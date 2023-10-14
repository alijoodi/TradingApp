using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradingProject.API.Controller;

namespace API.Controllers
{
    [Route("[controller]")]
    public class AdminController : APIV1ControllerBase
    {
        [HttpGet]
        [Authorize(Policy ="RequireAdminRole")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            return Ok("Only admins can see this");
        }
        

    }
}