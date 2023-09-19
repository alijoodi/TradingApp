using Microsoft.AspNetCore.Mvc;

namespace TradingProject.API.Controller
{
    // Specifies the route for accessing this controller and its actions.
    // The [controller] placeholder refers to the name of the controller class,
    // which in this case is APIV1ControllerBase.
    [Route("api/v1/[controller]/[action]")]
    // Indicates that this class is an API controller.
    [ApiController]
    public class APIV1ControllerBase : ControllerBase
    {

    }
}
