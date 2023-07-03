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
    public class OrderPriorityController : ControllerBase
    {
        private readonly TrackContext _context;
        private readonly IMapper _mapper;
        public OrderPriorityController(TrackContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<OrderPriority>> PostOrderPriority(OrderPriorityCreate orderPriority)
        {
            var manufacturerExists = await _context.Manufacturers
                .Where(m => m.Id == orderPriority.ManufacturerId && m.IsActive == true).FirstOrDefaultAsync();
            if (manufacturerExists == null)
            {
                return NotFound();
            }
            var mapPriority = _mapper.Map<OrderPriority>(orderPriority);
            _context.orderPriorities.Add(mapPriority);
            await _context.SaveChangesAsync();
            return Ok(mapPriority);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderPriority>>> getOrderPriorities()
        {
            var priorities = await _context.orderPriorities
                .Where(r => r.IsActive == true)
                .GroupBy(r => r.ManufacturerId)
                .ToListAsync();

            return Ok(priorities);
        }
        [HttpGet]
        [Route("uuid")]
        public async Task<ActionResult<OrderPriority>> getOrderPriorityByUuid(int id)
        {
            var priority = await _context.orderPriorities
                .Where(r => r.Id == id && r.IsActive == true).FirstAsync();
            if (priority == null)
            {
                return BadRequest();
            }
            return Ok(priority);
        }

        [HttpGet]
        [Route("manufacturerId")]
        public async Task<ActionResult<OrderPriority>> getOrderPriorityByManufacturerUuid(int id)
        {
            var priority = await _context.orderPriorities
                .Where(r => r.ManufacturerId == id && r.IsActive == true).ToListAsync();
            if (priority == null)
            {
                return BadRequest();
            }
            return Ok(priority);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<OrderPriority>> updateOrderPriority(int id, OrderPriorityUpdate orderPriorityUpdate)
        {
            var priorityExists = await _context.orderPriorities.FindAsync(id);
            if (priorityExists == null)
            {
                return NotFound();
            }
            var priorityMap = _mapper.Map(orderPriorityUpdate, priorityExists);
            _context.Entry(priorityMap).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(priorityMap);
        }
    }
}

