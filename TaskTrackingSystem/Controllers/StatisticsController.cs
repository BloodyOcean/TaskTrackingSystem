using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _ss;
        public StatisticsController(IStatisticService ss)
        {
            this._ss = ss;
        }

        [HttpGet("{count}")]
        public ActionResult<IEnumerable<CompletionPercentage>> GetCompletionPercentage(int count)
        {
            return Ok(_ss.GetCompletionPercentages(count));
        }
    }
}
