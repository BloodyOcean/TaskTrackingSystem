﻿using Administration.Account;
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

        public AccountsController(
            IUserService userService,
            IRoleService roleService,
            IOptionsSnapshot<JwtSettings> jwtSettings,
            IEmailService emailService)
        {
            _roleService = roleService;
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
            _mailService = emailService;
        }

        //POST: /api/accounts/register
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

        //POST: /api/accounts/logon
        [HttpPost("logon")]
        public async Task<IActionResult> Logon(LogonModel model)
        {
            var user = await _userService.Logon(new Logon
            {
                Email = model.Email,
                Password = model.Password
            });

            if (user is null) return BadRequest();

            var roles = await _roleService.GetRoles(user);

            return Ok(JwtHelper.GenerateJwt(user, roles, _jwtSettings));
        }

        //GET: /api/accounts/getRoles
        [HttpGet("getRoles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _roleService.GetRoles());
        }

        //POST: /api/accounts/createRole
        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            await _roleService.CreateRole(model.RoleName);
            return Ok();
        }

        //GET: /api/accounts
        [HttpGet]
        //[Authorize(Roles = "admin")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpDelete("roles")]
        //DELETE: /api/accounts/roles
        public async Task<IActionResult> RemoveRole(string name)
        {
            await _roleService.DeleteRole(name);
            return Ok();
        }


        //POST: /api/accounts/assignUserToRole
        [HttpPost("assignUserToRole")]
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
