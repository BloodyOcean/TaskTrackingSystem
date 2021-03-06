using Administration.Account;
using BLL.Interfaces;
using BLL.Models;
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
    public class HistoriesController : ControllerBase
    {
        private readonly IHistoryService _hs;
        private readonly IUserService _userService;
        public HistoriesController(IHistoryService historyService, IUserService userService)
        {
            this._userService = userService;
            this._hs = historyService;
        }

        /// <summary>
        /// Returns all Histories by Employee with entered UserId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200</returns>
        /// <example>GET: /api/histories</example>
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<HistoryModel>> GetByEmployeeId(int id)
        {
            
            return Ok(_hs.GetAllByEmployeeId(id));
        }

        /// <summary>
        /// Returns all histories made by current loged in user
        /// </summary>
        /// <returns>Status 200</returns>
        /// <example>GET: /api/histories/employee</example>
        [HttpGet("employee")]
        public async Task<ActionResult<IEnumerable<HistoryModel>>> GetAllByCurrentUser()
        {
            var id = await _userService.GetCurrentUserIdAsync(User);
            return Ok(_hs.GetAllByEmployeeId(id));
        }

        /// <summary>
        /// Returns all histories of some project with entered Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200</returns>
        /// <example>GET: api/histories/project</example>
        [HttpGet("project/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<HistoryModel>> GetAllByProjectId(int id)
        {
            var res = _hs.GetAllByProjectId(id);
            return Ok(res);
        }

        /// <summary>
        /// Returns list of all histories on projects, where employee takes part
        /// </summary>
        /// <returns>Status 200</returns>
        /// <example>GET: api/histories/employee/project</example>
        [HttpGet("employee/project")]
        public async Task<ActionResult<IEnumerable<HistoryModel>>> GetAllByCurrentEmployeeProjects()
        {
            var id = await _userService.GetCurrentUserIdAsync(User);
            var res = _hs.GetAllByEmployeeProjects(id);
            return Ok(res);
        }

        /// <summary>
        /// Returns list of all histories of project made by current manager
        /// </summary>
        /// <returns>Status 200</returns>
        /// <example>GET: api/histories/manager/project</example>
        [HttpGet("manager/project")]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult<IEnumerable<HistoryModel>>> GetAllByCurrentManagerProjects()
        {
            var id = await _userService.GetCurrentUserIdAsync(User);
            var res = _hs.GetAllByManagerProjects(id);
            return Ok(res);
        }

        /// <summary>
        /// Returns all histories from db
        /// </summary>
        /// <returns>Status 200</returns>
        /// <example>GET: /api/histories</example>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<HistoryModel>> GetAll()
        {
            return Ok(_hs.GetAll());
        }

        /// <summary>
        /// Adds new history in db. 
        /// Id of employee who changed stores in assignment
        /// </summary>
        /// <param name="historyModel"></param>
        /// <returns></returns>
        /// <example>POST: /api/histories</example>
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] HistoryModel historyModel)
        {
            await _hs.AddAsync(historyModel);
            return Ok();
        }
    }
}
