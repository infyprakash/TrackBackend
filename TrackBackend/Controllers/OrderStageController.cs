using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackBackend.Models;
using System.Diagnostics;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStageController : ControllerBase
    {

        private readonly TrackContext _context;
        private readonly IMapper _mapper;
        public OrderStageController(TrackContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("orderId")]
        public async Task<ActionResult<OrderStage>> GetOrderStagesByOrderUuId(int id)
        {
            var order = await _context.orderSheets.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderStages = await _context.orderStages
                .Where(os => os.OrderSheetId == order.Id && os.IsActive == true )
                .GroupBy(os => os.stage)
                .ToListAsync();
            return Ok(orderStages);
        }
        [HttpGet]
        [Route("order/by/stage")]
        public async Task<ActionResult<OrderStage>> GetOrdersByStage(int stageId)
        {
            //os.stage == (Stage)stageId
            var orders = await _context.orderStages
                .Include(os => os.OrderSheet)
                .Where(os => os.IsActive==true && os.stage== (Stage)stageId)
                .GroupBy(os => os.OrderSheetId )
                .ToListAsync();
            var filteredOrders = orders
                .Select(group => group.ToList())
                .Where(group => group.LastOrDefault()?.stage == (Stage)stageId && group.LastOrDefault()?.IsCompleted == false).ToList();


            return Ok(filteredOrders);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Post([FromForm] OrderStageCreate rugStage)
        {
            //var latestEntry = await _context.orderStages
            //    .OrderByDescending(entry => entry.CreatedAt)
            //    .FirstOrDefaultAsync();
            //Console.WriteLine("++",latestEntry!.stage);
            //if (latestEntry!=null &&  latestEntry!.IsCompleted == true && latestEntry.stage==Stage.Finalised)
            //{
            //    return NotFound("Not Allowed");
            //}
            if (rugStage.Image == null || rugStage.Image.Length == 0)
            {
                return BadRequest("Image file is required.");
            }

            var uploadsDirectory = "MyStaticFiles/images/";
            var fileName = $"{Guid.NewGuid()}-{rugStage.Image.FileName}";

            var filePath = Path.Combine(uploadsDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                rugStage.Image.CopyTo(stream);
            }
            var StageMapped = _mapper.Map<OrderStage>(rugStage);
            StageMapped.FilePath = filePath;
            // Save the design object to the database.
            _context.orderStages.Add(StageMapped);
            await _context.SaveChangesAsync();

            var order = await _context.orderSheets.FindAsync(StageMapped.OrderSheetId);
            if (order == null)
            {
                return NotFound();
            }
            Console.WriteLine(StageMapped.stage == Stage.Dispatched && StageMapped.IsCompleted == true);
            if (StageMapped.stage == Stage.Dispatched && StageMapped.IsCompleted == true)
            {
                order.IsActive = false;
                await _context.SaveChangesAsync();
            }

            return Ok();
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Put([FromForm] OrderStageUpdate rugStage)
        {

            var OrderStageObj = await _context.orderStages.FindAsync(rugStage.Id);
            if (OrderStageObj == null)
            {
                return NotFound();
            }

            if (rugStage.Image == null || rugStage.Image.Length == 0)
            {
                return BadRequest("Image file is required.");
            }

            var uploadsDirectory = "MyStaticFiles/images/";
            var fileName = $"{Guid.NewGuid()}-{rugStage.Image.FileName}";

            var filePath = Path.Combine(uploadsDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                rugStage.Image.CopyTo(stream);
            }

            var OrderStageM = _mapper.Map(rugStage, OrderStageObj);
            OrderStageM.FilePath = filePath;


            _context.Entry(OrderStageM).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(OrderStageM);
        }

    }
}

