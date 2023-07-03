using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrackBackend.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderManufacturerController : Controller
    {
        private readonly TrackContext _context;
        private readonly IMapper _mapper;
        public OrderManufacturerController(TrackContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Manufacturer>> PostManufacturer(OrderManufacturerCreate manufacturerCreate)
        {
            var manufacturerMapped = _mapper.Map<OrderManufacturer>(manufacturerCreate);
            _context.orderManufacturers.Add(manufacturerMapped);
            await _context.SaveChangesAsync();
            return Ok(manufacturerMapped);
        }
    }
}

