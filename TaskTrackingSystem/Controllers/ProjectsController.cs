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
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _ps;
        private readonly IUserService _userService;
        public ProjectsController(IProjectService ps, IUserService userService)
        {
            this._userService = userService;
            this._ps = ps;
        }

        /// <summary>
        /// Returns all projects from db
        /// </summary>
        /// <returns>Status 200</returns>
        /// <example>GET: /api/projects</example>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<ProjectModel>> GetAll()
        {
            return Ok(_ps.GetAll());
        }

        /// <summary>
        /// Returns project that has assignment with entered id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200</returns>
        /// <example>GET: /api/projects/assignment</example>
        [HttpGet("assignment/{id}")]
        [Authorize(Roles = "admin, manager")]
        public ActionResult<ProjectModel> GetByAssignmentId(int id)
        {
            return Ok(_ps.GetByAssignmentId(id));
        }

        /// <summary>
        /// Returns all projects where current employee works on
        /// </summary>
        /// <returns>Status 200</returns>
        /// <example>GET: /api/projects/employee</example>
        [HttpGet("employee")]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjectsOfCurrentUser()
        {
            var id = await _userService.GetCurrentUserIdAsync(User);
            return Ok(_ps.GetProjectsByEmployee(id));
        }

        /// <summary>
        /// Change the managerId in some project
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200</returns>
        /// <example>POST: /api/projects/manager</example>
        [HttpPost("manager")]
        [Authorize(Roles = "admin, manager")]
        public async Task<ActionResult<ProjectModel>> AssignManagerToProject([FromBody] AssignManagerToProjectModel model)
        {
            return Ok(await _ps.AssignManagerToProject(model));
        }

        /// <summary>
        /// Updates project by id
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 20</returns>
        /// <example>PUT: /api/projects</example>
        [HttpPut]
        [Authorize(Roles = "admin, manager")]
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

        /// <summary>
        /// Add new project in db
        /// </summary>
        /// <param name="projectModel"></param>
        /// <returns>Status 201</returns>
        /// <example>POST: /api/projects</example>
        [HttpPost]
        [Authorize(Roles = "admin, manager")]
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
