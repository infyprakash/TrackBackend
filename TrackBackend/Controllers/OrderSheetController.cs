using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TrackBackend.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderSheetController : ControllerBase
    {
        private readonly TrackContext _context;
        static HttpClient httpClient = new HttpClient();

        public OrderSheetController(TrackContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<OrderSheet>> GetOrderSheet()
        {
            var url = "https://explorug.com/archanastools/Utilities/RugTrackingLog.txt";
            string responseMessage = await httpClient.GetStringAsync(url);
            string[] contents = responseMessage.Split("\n");
            List<OrderSheet> OrderSheets = new List<OrderSheet>(); 
            int recordCount = _context.orderSheets.Count();
            Console.WriteLine(recordCount);
            int count = 0;

            for (int i = 0; i < contents.Length; i++)
            {
                string[] data = contents[i].Split("\t");
                if (data.Length < 2)
                {
                    continue;
                }
                else
                {
                    count++;
                    if (count <= recordCount) continue;

                    Console.WriteLine("i=" + (contents[i - 1]));
                    for (int j = 0; j < data.Length; j++)
                    {
                        Console.WriteLine(data[j]);
                    }
                    var orderSheet = new OrderSheet
                    {
                        DesignName = data[0],
                        ImageUrl = data[1],
                        PdfUrl = data[2],
                        CtfUrl = data[3],
                        PhysicalWidth = data[4],
                        PhysicalHeight = data[5],
                        Unit = data[6],
                        PixelWidth = data[7],
                        PixelHeight = data[8],
                        CreatedAt = DateTime.Parse(contents[i - 1])
                    };
                    _context.orderSheets.Add(orderSheet);
                    await _context.SaveChangesAsync();
                    OrderSheets.Add(orderSheet);

                }

            }
            return Ok(OrderSheets);
        }

    }
}

