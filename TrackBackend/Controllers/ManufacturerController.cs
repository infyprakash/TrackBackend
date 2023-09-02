using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackBackend.Models;

namespace TrackBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly TrackContext _context;
        private readonly IMapper _mapper;
        public ManufacturerController(TrackContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<OrderSheet>>> GetAllManufacturer()
        {
            var result = await _context.Manufacturers
                .Where(r => r.IsActive == true).ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<Manufacturer>> PostManufacturer(ManufacturerCreate manufacturerCreate)
        {
            var manufacturerExists = _context.Manufacturers.Any(m =>m.PhoneNumber==manufacturerCreate.PhoneNumber && m.IsActive==true);
            if (manufacturerExists)
            {
                return BadRequest();
            }
            var manufacturerMapped = _mapper.Map<Manufacturer>(manufacturerCreate);
            _context.Manufacturers.Add(manufacturerMapped);
            await _context.SaveChangesAsync();
            return Ok(manufacturerMapped);
        }
        [HttpPut]
        public async Task<ActionResult<Manufacturer>> UpdateManufacturer(ManufacturerUpdate manufacturerUpdate)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(manufacturerUpdate.Id);
            if(manufacturer==null)
            {
                return BadRequest();
            }
            var manufacturerMap = _mapper.Map(manufacturerUpdate,manufacturer);
            _context.Entry(manufacturerMap).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(manufacturerMap);
        }



    }
}

