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
    //[Authorize]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _as;
        private readonly IUserService _userService;
        public AssignmentsController(IAssignmentService assignmentService, IUserService userService)
        {
            this._userService = userService;
            this._as = assignmentService;
        }

        //GET: /api/assignments
        [HttpGet]
        public ActionResult<IEnumerable<AssignmentModel>> GetAll()
        {
            return Ok(_as.GetAll());
        }

        //GET: /api/assignments
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentModel>> GetById(int id)
        {
            return Ok(await _as.GetByIdAsync(id));
        }

        //DELETE: /api/assignments
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _as.DeleteByIdAsync(id);
            return Ok();
        }

        //PUT: /api/assignments
        [HttpPut]
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

        //GET: /api/assignments/employee
        [HttpGet("Employee")]
        public async Task<ActionResult<IEnumerable<AssignmentModel>>> GetAssignmentsByEmployeeId()
        {
            var id = await _userService.GetCurrentUserIdAsync(User);
            return Ok(_as.GetAssignmentsByEmployee(id));
        }

        //POST: /api/assignments
        [HttpPost]
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
