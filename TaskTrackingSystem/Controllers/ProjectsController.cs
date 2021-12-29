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

        //DELETE: /api/projects
        [HttpDelete]
        public async Task<ActionResult> RemoveProject(int? id)
        {
            if (id == null)
            {
                return Ok();
            }

            await _ps.DeleteByIdAsync((int)id);
            return Ok();
        }

        //POST: /api/projects/assignManagerToProject
        [HttpPost("assignManagerToProject")]
        public async Task<ActionResult<ProjectModel>> AssignManagerToProject([FromBody] AssignManagerToProjectModel model)
        {
            return Ok(await _ps.AssignManagerToProject(model));
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
