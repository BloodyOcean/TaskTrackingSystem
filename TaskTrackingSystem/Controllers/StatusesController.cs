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
    public class StatusesController : ControllerBase
    {
        private readonly IAssignmentStatusService _statusService;
        public StatusesController(IAssignmentStatusService assignmentStatusService)
        {
            this._statusService = assignmentStatusService;
        }

        /// <summary>
        /// Gets all statuses of tasks drom db
        /// </summary>
        /// <returns>Status 200 if ok</returns>
        /// <example>GET: /api/statuses</example>
        [HttpGet]
        public ActionResult<IEnumerable<AssignmentStatusModel>> GetAll()
        {
            return Ok(_statusService.GetAll());
        }

        /// <summary>
        /// Adds new status of task in db
        /// </summary>
        /// <param name="statusModel"></param>
        /// <returns></returns>
        /// <example>POST: /api/statuses</example>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Add([FromBody] AssignmentStatusModel statusModel)
        {
            if (statusModel == null)
            {
                return BadRequest();
            }

            try
            {
                await _statusService.AddAsync(statusModel);
                return CreatedAtAction(nameof(Add), statusModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
