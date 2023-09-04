using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackBackend.Models;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackBackend.Controllers
{
    public class ScheduleCreate
    {
        public int? orderId { get; set; }
        public int? manufacturerId { get; set; }
        public int? priorityId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly TrackContext _context;
        private readonly IMapper _mapper;
        public ScheduleController(TrackContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<OrderSheet>> GetOrderNotScheduled()
        {
            var orders = await _context.orderSheets
                .Where(r => r.IsScheduled==false && r.IsActive == true).ToListAsync();
            return Ok(orders);
        }
        [HttpPost]
        [Route("order")]
        public async Task<ActionResult> scheduleOrder(ScheduleCreate schedule)
        {
            var order = await _context.orderSheets.FindAsync(schedule.orderId);
            if (order == null)
            {
                return NotFound();
            }
            var manuExists = await _context.orderManufacturers.AnyAsync(o => o.OrderSheetId == schedule.orderId);
            if(manuExists)
            {
                return BadRequest("Already Assigned");
            }
            var orderManu = new OrderManufacturer
            {
                OrderSheetId = schedule.orderId,
                ManufacturerId = schedule.manufacturerId
            };
            _context.orderManufacturers.Add(orderManu);
            await _context.SaveChangesAsync();
            var stageExists = await _context.orderStages.AnyAsync(o => o.OrderSheetId == schedule.orderId);
            if (stageExists)
            {
                return BadRequest("Already Assigned");
            }
            var orderStage = new OrderStage
            {
                OrderSheetId = schedule.orderId,
                stage = Stage.Preparation,
                IsCompleted = false
            };
            _context.orderStages.Add(orderStage);
            await _context.SaveChangesAsync();
            var statusExists = await _context.orderStatuses.AnyAsync(o => o.OrderSheetId == schedule.orderId);
            if (statusExists)
            {
                return BadRequest("Already Assigned");
            }
            var priority = await _context.orderPriorities
                .Where(o => o.IsActive == true && o.Id == schedule.priorityId).FirstOrDefaultAsync();
            if (priority == null)
            {
                return NotFound();
            }
            var DaysRequiredForWeaving = 0; 
            if (order.Unit=="ft")
            {
                DaysRequiredForWeaving = Convert.ToInt32(Convert.ToDouble(order.PhysicalHeight) / (10 * 0.0328084));
            }
            else
            {
                DaysRequiredForWeaving = Convert.ToInt32(Convert.ToDouble(order.PhysicalHeight) / (10));
            }
            var totalDays = Convert.ToInt32(priority.DaysRequiredForFinalTriming
                + DaysRequiredForWeaving
                + priority.DaysRequiredForFinishing
                + priority.DaysRequiredForPackaging
                + priority.DaysRequiredForProcessing
                + priority.DaysRequiredForShipping
                + priority.DaysRequiredForTrimming
                + priority.DaysRequiredForWashing);


            var orderStatus = new OrderStatus
            {
                OrderSheetId = schedule.orderId,
                OrderPriorityId = schedule.priorityId,
                DeliveryDate = order.CreatedAt.AddDays(totalDays)
                
            };
            _context.orderStatuses.Add(orderStatus);
            await _context.SaveChangesAsync();

            order.IsScheduled = true;
            await _context.SaveChangesAsync();



            return Ok("success");
        }
    }

}

