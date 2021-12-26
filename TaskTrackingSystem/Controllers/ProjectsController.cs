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
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _ps;
        public ProjectsController(IProjectService ps)
        {
            this._ps = ps;

        }

        //GET: /api/projects
        [HttpGet]
        public ActionResult<IEnumerable<ProjectModel>> GetAll()
        {
            return Ok(_ps.GetAll());
        }

        //POST: /api/projects
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProjectModel projectModel)
        {
            if (projectModel == null)
            {
                return BadRequest();
            }

            try
            {
                await _ps.AddAsync(projectModel);
                return CreatedAtAction(nameof(Add), projectModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
