using Administration.Account;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _as;
        private readonly IUserService _userService;
        public AssignmentsController(IAssignmentService assignmentService, IUserService userService)
        {
            this._userService = userService;
            this._as = assignmentService;
        }

        /// <summary>
        /// Returns all tasks from db
        /// </summary>
        /// <returns>Status 200</returns>
        /// <example>GET: /api/assignments</example>
        [HttpGet]
        [Authorize(Roles = "admin, manager")]
        public ActionResult<IEnumerable<AssignmentModel>> GetAll()
        {
            return Ok(_as.GetAll());
        }

        /// <summary>
        /// Returns assignment info from Db by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200</returns>
        /// <example>GET: /api/assignments</example>
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult<AssignmentModel>> GetById(int id)
        {
            return Ok(await _as.GetByIdAsync(id));
        }

        /// <summary>
        /// Removes record of task by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <example>DELETE: /api/assignments</example>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _as.DeleteByIdAsync(id);
            return Ok();
        }

        /// <summary>
        /// Update the record by their Id
        /// </summary>
        /// <param name="assignmentModel"></param>
        /// <returns>Status 200 if updates Ok</returns>
        /// <example>PUT: /api/assignments</example>
        [HttpPut]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Update(AssignmentModel assignmentModel)
        {
            if (assignmentModel == null)
            {
                return BadRequest();
            }
            try
            {
                await _as.UpdateAsync(assignmentModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all assignment of current user
        /// </summary>
        /// <returns>List of assignments only for current authorized user</returns>
        /// <example>GET: /api/assignments/employee </example>
        [HttpGet("Employee")]
        public async Task<ActionResult<IEnumerable<AssignmentModel>>> GetAssignmentsByEmployeeId()
        {
            var id = await _userService.GetCurrentUserIdAsync(User);
            return Ok(_as.GetAssignmentsByEmployee(id));
        }

        /// <summary>
        /// Writes new record of task in db
        /// </summary>
        /// <param name="assignmentModel"></param>
        /// <returns>Status 201 if creates ok</returns>
        /// <example>POST: /api/assignments</example>
        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult> Add([FromBody] AssignmentModel assignmentModel)
        {
            if (assignmentModel == null)
            {
                return BadRequest();
            }

            try
            {
                await _as.AddAsync(assignmentModel);
                return CreatedAtAction(nameof(Add), assignmentModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
