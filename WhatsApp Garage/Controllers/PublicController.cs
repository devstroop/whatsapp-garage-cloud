using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Server.Models;

namespace Server.Controllers
{
    public class PublicController : Controller
    {

        public PublicController() { }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Documentation()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }



    }
}