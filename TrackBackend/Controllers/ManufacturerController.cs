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
    [ApiController]
    [Route("[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly TrackContext _context;
        private readonly IMapper _mapper;
        public ManufacturerController(TrackContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

