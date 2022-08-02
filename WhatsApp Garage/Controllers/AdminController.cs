using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Models;

namespace Server.Controllers
{
    public class AdminController : Controller
    {
        FirebaseAuthProvider auth;
        public AdminController()
        {
            this.auth = new FirebaseAuthProvider(new FirebaseConfig(Constants.Configs.FirebaseApiKey));
        }

        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("_AdminToken");

            if (token != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                //log in an existing user
                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                string token = fbAuthLink.FirebaseToken;
                //save the token to a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_AdminToken", token);

                    return RedirectToAction("Index", "Admin");
                }

            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx!.error.message);
                return View(loginModel);
            }

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_AdminToken");
            return RedirectToAction("Login");
        }
    }
}
