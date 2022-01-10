using Administration.Account;
using BLL.Interfaces;
using BLL.Models;
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

        //GET: /api/histories
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<HistoryModel>> GetByEmployeeId(int id)
        {
            
            return Ok(_hs.GetAllByEmployeeId(id));
        }

        //GET: /api/histories/employee
        [HttpGet("employee")]
        public async Task<ActionResult<IEnumerable<HistoryModel>>> GetAllByCurrentUser()
        {
            var id = await _userService.GetCurrentUserIdAsync(User);
            return Ok(_hs.GetAllByEmployeeId(id));
        }

        [HttpGet("project/{id}")]
        public ActionResult<IEnumerable<HistoryModel>> GetAllByProjectId(int id)
        {
            var res = _hs.GetAllByProjectId(id);
            return Ok(res);
        }

        [HttpGet("employee/project")]
        public async Task<ActionResult<IEnumerable<HistoryModel>>> GetAllByCurrentEmployeeProjects()
        {
            var id = await _userService.GetCurrentUserIdAsync(User);
            var res = _hs.GetAllByEmployeeProjects(id);
            return Ok(res);
        }

        [HttpGet("manager/project")]
        public async Task<ActionResult<IEnumerable<HistoryModel>>> GetAllByCurrentManagerProjects()
        {
            var id = await _userService.GetCurrentUserIdAsync(User);
            var res = _hs.GetAllByManagerProjects(id);
            return Ok(res);
        }

        //GET: /api/histories
        [HttpGet]
        public ActionResult<IEnumerable<HistoryModel>> GetAll()
        {
            return Ok(_hs.GetAll());
        }

        //POST: /api/histories
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] HistoryModel historyModel)
        {
            await _hs.AddAsync(historyModel);
            return Ok();
        }
    }
}
