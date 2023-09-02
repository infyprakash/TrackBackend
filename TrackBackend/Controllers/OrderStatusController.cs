using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackBackend.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly TrackContext _context;
        private readonly IMapper _mapper;
        public OrderStatusController(TrackContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public static IDictionary<string, object> ConvertModelToDictionary(object model)
        {
            var dictionary = new Dictionary<string, object>();
            var properties = model.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(model);
                if(value!=null)
                    dictionary.Add(property.Name, value);
            }

            return dictionary;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> OrderStatuses()
        {
            var orderStatus = await _context.orderStatuses.ToListAsync();
            return Ok(orderStatus);
        }
        [HttpGet("statusId")]
        public async Task<ActionResult<OrderStatus>> GetOrderStatusById(int statusId)
        {
            var orderStatus = await _context.orderStatuses.FindAsync(statusId);
            if (orderStatus == null)
            {
                return NotFound();
            }
            return Ok(orderStatus);
        }
        [HttpGet]
        [Route("orderId")]
        public async Task<ActionResult<OrderStatus>> GetOrderStatusByOrderId(int orderId)
        {
            
            var order = await _context.orderSheets.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            var orderStatus = await _context.orderStatuses
                .Include(os => os.OrderPriority)
                .Where(os => os.OrderSheetId == order.Id).ToListAsync();

            return Ok(orderStatus);
        }
        [HttpGet]
        [Route("detail")]
        public async Task<ActionResult> GetOrderStatusDetailByOrderId(int orderId)
        {
            var order = await _context.orderSheets.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            var orderDictionary = ConvertModelToDictionary(order);
            var orderStatus = await _context.orderStatuses
                .Where(s => s.IsActive == true && s.OrderSheetId == orderId).FirstOrDefaultAsync();
            if(orderStatus==null)
            {
                return NotFound();
            }
            var orderPriority = await _context.orderPriorities
                .Where(p => p.IsActive == true && p.Id == orderStatus.OrderPriorityId).FirstOrDefaultAsync();
            if(orderPriority==null)
            {
                return NotFound();
            }
            // 10 cm per day
            var orderPriorityDict = ConvertModelToDictionary(orderPriority);
            double linesPerSquareInch = 2;
            double totalLines = 0;
            if (order.Unit == "ft")
            {
                totalLines = (linesPerSquareInch) * ((12 * Convert.ToDouble(order.PhysicalWidth)) * (12 * Convert.ToDouble(order.PhysicalHeight)));

            }
            else
            {
                totalLines = (linesPerSquareInch) * ((Convert.ToDouble(order.PhysicalWidth) / 2.54) * (Convert.ToDouble(order.PhysicalHeight) / 2.54));

            }
            var DaysRequiredForWeaving = totalLines / orderPriority.LinesPerDay;
            var totalDays = Convert.ToInt32(orderPriority.DaysRequiredForFinalTriming
                + DaysRequiredForWeaving
                + orderPriority.DaysRequiredForFinishing
                + orderPriority.DaysRequiredForPackaging
                + orderPriority.DaysRequiredForProcessing
                + orderPriority.DaysRequiredForShipping
                + orderPriority.DaysRequiredForTrimming
                + orderPriority.DaysRequiredForWashing);
            orderPriorityDict.Add("TotalDaysRequired", totalDays);
            if (DaysRequiredForWeaving != null)
            {
                orderPriorityDict.Add("DaysReqiredForWeaving", DaysRequiredForWeaving);
            }
            orderDictionary.Add("OrderStatusEstimate", orderPriorityDict);
            orderDictionary.Add("ExpectedDeliveryDate", orderStatus.DeliveryDate!);

            var orderStages = await _context.orderStages
                .Where(os => os.OrderSheetId == order.Id && os.IsActive == true)
                .GroupBy(os => os.stage)
                .ToListAsync();
            var listOfStage = new List<Dictionary<string,object>>();
            for(int i=0;i<orderStages.Count();i++)
            {
                var d = new Dictionary<string, object>();
                    if(orderStages[i].ElementAt(0) != null)
                    {
                        d.Add("createdAt", orderStages[i].ElementAt(0).CreatedAt!);
                        d.Add("stage", orderStages[i].ElementAt(0).stage);
                        d.Add("filePath", orderStages[i].ElementAt(orderStages[i].Count() - 1).FilePath!);
                        TimeSpan? duration =  orderStages[i].ElementAt(orderStages[i].Count() - 1).CreatedAt! - orderStages[i].ElementAt(0).CreatedAt!;
                        int numberOfDays = (int)duration.Value.TotalDays;
                        d.Add("daysSpent", numberOfDays);
                        d.Add("isCompleted", orderStages[i].ElementAt(orderStages[i].Count() - 1).IsCompleted!);
                        d.Add("lengthCompletd", orderStages[i].ElementAt(orderStages[i].Count() - 1).LengthCompleted!);
                        d.Add("lengthUnit", orderStages[i].ElementAt(orderStages[i].Count() - 1).LengthUnit!);
                    }

                    
                listOfStage.Add(d);
            }
            orderDictionary.Add("OrderStage", listOfStage);

            return Ok(orderDictionary);
        }


        [HttpPost]
        public async Task<ActionResult<OrderStatus>> PostOrderStatus(OrderStatusCreate order)
        {
            var orderOb = await _context.orderSheets.Where(o => o.IsActive == true && o.Id==order.OrderSheetId).FirstOrDefaultAsync(); ;
            if(orderOb==null)
            {
                return NotFound();
            }


            var orderExists = await _context.orderStatuses.AnyAsync(o => o.OrderSheetId == order.OrderSheetId);
            if (orderExists)
            {
                return BadRequest("status already exists for this order");
            }


            var priority = await _context.orderPriorities
                .Where(o => o.IsActive == true && o.Id == order.OrderPriorityId).FirstOrDefaultAsync();
            if (priority == null)
            {
                return NotFound();
            }
            double linesPerSquareInch = 2;
            double totalLines = 0;
            if (orderOb.Unit == "ft")
            {
                totalLines = (linesPerSquareInch) * ((12* Convert.ToDouble(orderOb.PhysicalWidth)) * (12 *Convert.ToDouble(orderOb.PhysicalHeight)));

            }
            else
            {
                totalLines = (linesPerSquareInch) * ((Convert.ToDouble(orderOb.PhysicalWidth) / 2.54) * (Convert.ToDouble(orderOb.PhysicalHeight) / 2.54));

            }
            var DaysRequiredForWeaving = totalLines / priority.LinesPerDay;
            var totalDays = Convert.ToInt32(priority.DaysRequiredForFinalTriming
                +DaysRequiredForWeaving
                + priority.DaysRequiredForFinishing
                + priority.DaysRequiredForPackaging
                + priority.DaysRequiredForProcessing
                + priority.DaysRequiredForShipping
                + priority.DaysRequiredForTrimming
                + priority.DaysRequiredForWashing);

            var orderObj = await _context.orderSheets
                .Where(o => o.IsActive == true && o.Id == order.OrderSheetId).FirstOrDefaultAsync();
            if (orderObj == null)
            {
                return NotFound();
            }
            var OrderMapped = _mapper.Map<OrderStatus>(order);
            OrderMapped.DeliveryDate = orderObj.CreatedAt.AddDays(totalDays);
            _context.orderStatuses.Add(OrderMapped);
            await _context.SaveChangesAsync();
            return Ok(OrderMapped);
            //return CreatedAtAction(nameof(GetOrderStatusById), new { Uuid = OrderMapped.Id }, order);
        }
        [HttpPut("Id")]
        public async Task<ActionResult> PutOrderStatus(int id, OrderStatusUpdate order)
        {
            var OrderObj = await _context.orderStatuses.FindAsync(id);
            if (OrderObj == null)
            {
                return NotFound();
            }
            var OrderM = _mapper.Map(order, OrderObj);
            _context.Entry(OrderM).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(OrderM);

        }

    }
}

