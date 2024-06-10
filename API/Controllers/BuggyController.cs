using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingProject.API.Controller;

namespace API.Controllers
{
    public class BuggyController : APIV1ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BuggyController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        //[Authorize]
        [HttpGet]
        public ActionResult<string> GetSecret()
        {
            return Ok("secret text");
        }

        [HttpGet]
        public ActionResult<string> GetNotFound()
        {
            return NotFound();
        }

        [HttpGet]
        public ActionResult<string> GetServerError()
        {
            throw new NullReferenceException();
        }

        [HttpGet]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest();
        }
    }
}