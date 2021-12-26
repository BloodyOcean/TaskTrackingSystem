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
