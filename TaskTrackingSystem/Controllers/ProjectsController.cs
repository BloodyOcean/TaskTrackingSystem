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
    //[Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _ps;
        private readonly IUserService _userService;
        public ProjectsController(IProjectService ps, IUserService userService)
        {
            this._userService = userService;
            this._ps = ps;
        }

        //GET: /api/projects
        [HttpGet]
        public ActionResult<IEnumerable<ProjectModel>> GetAll()
        {
            return Ok(_ps.GetAll());
        }

        //GET: /api/projects/assignment
        [HttpGet("assignment/{id}")]
        public ActionResult<ProjectModel> GetByAssignmentId(int id)
        {
            return Ok(_ps.GetByAssignmentId(id));
        }

        //GET: /api/projects/employee
        [HttpGet("employee")]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjectsOfCurrentUser()
        {
            var id = await _userService.GetCurrentUserIdAsync(User);
            return Ok(_ps.GetProjectsByEmployee(id));
        }

        //POST: /api/projects/manager
        [HttpPost("manager")]
        public async Task<ActionResult<ProjectModel>> AssignManagerToProject([FromBody] AssignManagerToProjectModel model)
        {
            return Ok(await _ps.AssignManagerToProject(model));
        }

        //PUT: /api/projects
        [HttpPut]
        public async Task<ActionResult> Update(ProjectModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            try
            {
                var id = await _userService.GetCurrentUserIdAsync(User);
                model.ManagerId = id;

                await _ps.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST: /api/projects
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProjectModel projectModel)
        {
            if (projectModel == null)
            {
                return BadRequest();
            }

            var id = await _userService.GetCurrentUserIdAsync(User);
            projectModel.ManagerId = id;

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
