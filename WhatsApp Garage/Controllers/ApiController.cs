using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Send(MesageRequest request)
        {
            return Json(request);
        }
    }
}
