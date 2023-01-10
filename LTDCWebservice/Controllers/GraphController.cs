//using Graphing.RadarChart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Color = System.Drawing.Color;

namespace LTDCWebservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphController : ControllerBase
    {
        private readonly ILogger<GraphController> _logger;

        public GraphController(ILogger<GraphController> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        public string Get()
        {
            string filePath = Path.GetTempFileName();
            

            return filePath;
        }

        //[HttpPost]
        //public ActionResult Post(List<DataPoint> dataPoints)
        //{
        //    List<AxisInfo> AxisInfos = new List<AxisInfo>();
        //    AxisInfos.Add(new AxisInfo("PriceLow", "c", 90000, 30000));
        //    AxisInfos.Add(new AxisInfo("PriceHigh", "c", 90000, 30000));
        //    AxisInfos.Add(new AxisInfo("Rating", "0.0", 0, 10));
        //    AxisInfos.Add(new AxisInfo("Range", "0", 0, 300));
        //    AxisInfos.Add(new AxisInfo("Miles/kWh", "0.00", 0, 5));

        //    string filePath = Path.GetTempFileName();
        //    RadarChart chart = new RadarChart(filePath, new System.Drawing.Size(800, 800), AxisInfos, dataPoints);
        //    chart.Draw();

        //    return Ok();
        //}


    }
}
