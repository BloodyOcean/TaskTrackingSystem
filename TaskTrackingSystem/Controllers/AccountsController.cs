using Administration;
using Administration.Account;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackingSystem.Helpers;
using TaskTrackingSystem.Models.Account;

namespace TaskTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly JwtSettings _jwtSettings;
        private readonly IEmailService _mailService;
        private readonly IAssignmentService _assignmentService;

        public AccountsController(
            IAssignmentService assignmentService,
            IUserService userService,
            IRoleService roleService,
            IOptionsSnapshot<JwtSettings> jwtSettings,
            IEmailService emailService)
        {
            _assignmentService = assignmentService;
            _roleService = roleService;
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
            _mailService = emailService;
        }

        /// <summary>
        /// Gets register credentiasl and made new user record in db
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 201 if account created</returns>
        /// <example>POST: /api/accounts/register</example>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            await _userService.Register(new Register
            {
                Email = model.Email,
                Name = model.Name,
                Password = model.Password
            });

            return Created(string.Empty, string.Empty);
        }

        /// <summary>
        /// Gets login credentials and generate JWT for this person
        /// </summary>
        /// <param name="model"></param>
        /// <returns>JWT token of person and status 200</returns>
        /// <example>POST: /api/accounts/logon</example>
        [HttpPost("logon")]
        public async Task<IActionResult> Logon(LogonModel model)
        {
            var user = await _userService.Logon(new Logon
            {
                Email = model.Email,
                Password = model.Password
            });
        

            if (user is null) return BadRequest();

            // Save UserId to session.
            HttpContext.Session.SetInt32("id", 228);

            Console.WriteLine(HttpContext.Session.GetInt32("id"));

            var roles = await _roleService.GetRoles(user);

            return Ok(JwtHelper.GenerateJwt(user, roles, _jwtSettings));
        }

        /// <summary>
        /// takes all role from db and returns them
        /// </summary>
        /// <returns></returns>
        /// <example>GET: /api/accounts/getRoles</example>
        [HttpGet("getRoles")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _roleService.GetRoles());
        }

        /// <summary>
        /// Finds user by his taskID in assignmentRepository and returns it 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <example>GET: /api/assignment</example>
        [HttpGet("assignment/{id}")]
        [Authorize(Roles = "admin, manager")]
        public ActionResult<ApplicationUser> GetUserByAssignmentId(int id)
        {
            var employeeId = _assignmentService.GetEmployeeId(id);
            var res = _userService.GetAll().First(p => p.UserId == employeeId);
            return Ok(res);            
        }

        /// <summary>
        /// get UserId property from context of user, find him and remove
        /// also delete all assignments of this user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <example>DELETE: /api/accounts</example>
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            await _userService.DeleteAccountByUserId(id);
            await _assignmentService.RemoveAssignmentsByEmployeeId(id);            
            return Ok();
        }

        /// <summary>
        /// Takes string from 5 to 20 chars and add new entity in db
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 if role created</returns>
        /// <example>POST: /api/accounts/createRole</example>
        [HttpPost("createRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            await _roleService.CreateRole(model.RoleName);
            return Ok();
        }

        /// <summary>
        /// Take all user info from db from db
        /// </summary>
        /// <returns></returns>
        /// <example>GET: /api/accounts</example>
        [HttpGet]
        [Authorize(Roles = "admin, manager")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        /// <summary>
        /// gets the string with role name and delete this role from db
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Status 200 if role deleted</returns>
        /// <example>DELETE: /api/accounts/roles</example>
        [HttpDelete("roles")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveRole(string name)
        {
            await _roleService.DeleteRole(name);
            return Ok();
        }

        /// <summary>
        /// Gets roles in string format (ex. "admin, manager") and email of user
        /// after assigning the roles, method send email for this user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 if roles assigned</returns>
        /// <example>POST: /api/accounts/assignUserToRole</example>
        [HttpPost("assignUserToRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AssignUserToRole(AssignUserToRoleModel model)
        {
            await _roleService.AssignUserToRoles(new AssignUserToRoles
            {
                Email = model.Email,
                Roles = model.Roles
            });

            await _mailService.SendEmailAsync(model.Email, "You have new role(s)!", "Congratulations! You have new roles(s)!");

            return Ok();
        }
    }
}
