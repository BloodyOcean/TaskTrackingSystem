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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _es;
        public EmployeesController(IEmployeeService es)
        {
            this._es = es;

        }

        //GET: /api/employees
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeModel>> GetAll()
        {
            return Ok(_es.GetAll());
        }

        //POST: /api/employees
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] EmployeeModel employeeModel)
        {
            if (employeeModel == null)
            {
                return BadRequest();
            }

            try
            {
                await _es.AddAsync(employeeModel);
                return Ok(employeeModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
