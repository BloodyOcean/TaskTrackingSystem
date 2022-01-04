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
    public class HistoriesController : ControllerBase
    {
        private readonly IHistoryService _hs;
        public HistoriesController(IHistoryService historyService)
        {
            this._hs = historyService;
        }

        //GET: /api/histories
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<HistoryModel>> GetByEmployeeId(int id)
        {
            
            return Ok(_hs.GetAllByEmployeeId(id));
        }

        //GET: /api/histories
        [HttpGet]
        public ActionResult<IEnumerable<HistoryModel>> GetAll()
        {
            return Ok(_hs.GetAll());
        }

        //POST: /api/histories
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] HistoryModel historyModel)
        {
            await _hs.AddAsync(historyModel);
            return Ok();
        }
    }
}
