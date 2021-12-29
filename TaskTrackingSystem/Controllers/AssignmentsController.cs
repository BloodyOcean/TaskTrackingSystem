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
    //[Authorize]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _as;
        public AssignmentsController(IAssignmentService assignmentService)
        {
            this._as = assignmentService;
        }

        //GET: /api/assignments
        [HttpGet]
        public ActionResult<IEnumerable<AssignmentModel>> GetAll()
        {
            return Ok(_as.GetAll());
        }

        //GET: /api/assignments/id
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<AssignmentModel>> GetAssignmentsByEmployeeId(int id)
        {
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
