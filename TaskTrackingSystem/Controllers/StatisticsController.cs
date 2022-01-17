using Administration.Account;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Made list of percentages, sort it and take first N elements
        /// </summary>
        /// <param name="count"></param>
        /// <returns>List of sorted percentages in descending order</returns>
        /// <example>GET: api/statistics</example>
        [HttpGet("{count}")]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<CompletionPercentage>> GetAllCompletionPercentage(int count)
        {
            return Ok(_ss.GetCompletionPercentages(count));
        }

        /// <summary>
        /// Made list of percentages in descending order of current manager projects
        /// </summary>
        /// <returns>All list of percentages by current manager loged in</returns>
        /// <example>GET api/statistics/manager</example>
        [HttpGet("manager")]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult<IEnumerable<CompletionPercentage>>> GetAllCompletionPercentageByManager()
        {
            var id = await _userService.GetCurrentUserIdAsync(User);
            return Ok(_ss.GetCompletionPercentagesByManager(id));
        }
    }
}
