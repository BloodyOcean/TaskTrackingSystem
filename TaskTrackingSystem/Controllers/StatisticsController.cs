using Administration.Account;
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
        private readonly IUserService _userService;
        public StatisticsController(IStatisticService ss, IUserService userService)
        {
            this._userService = userService;
            this._ss = ss;
        }

        [HttpGet("{count}")]
        public ActionResult<IEnumerable<CompletionPercentage>> GetAllCompletionPercentage(int count)
        {
            return Ok(_ss.GetCompletionPercentages(count));
        }

        [HttpGet("manager")]
        public async Task<ActionResult<IEnumerable<CompletionPercentage>>> GetAllCompletionPercentageByManager()
        {
            var id = await _userService.GetCurrentUserIdAsync(User);
            return Ok(_ss.GetCompletionPercentagesByManager(id));
        }
    }
}
