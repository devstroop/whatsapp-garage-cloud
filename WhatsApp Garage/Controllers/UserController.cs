using Microsoft.AspNetCore.Mvc;
using Firebase.Auth;
using Server.Models;
using Newtonsoft.Json;

namespace Server.Controllers
{
    public class UserController : Controller
    {
        FirebaseAuthProvider auth;
        public UserController()
        {
            this.auth = new FirebaseAuthProvider(new FirebaseConfig(Constants.Configs.FirebaseApiKey));
        }

        // Index
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("_UserToken");

            if (token != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                //create the user
                var firebaseAuthLink = await auth.CreateUserWithEmailAndPasswordAsync(registerModel.Email, registerModel.Password, registerModel.Name);

                //log in the new user
                firebaseAuthLink = await auth.SignInWithEmailAndPasswordAsync(registerModel.Email, registerModel.Password);
                string token = firebaseAuthLink.FirebaseToken;

                //saving the token in a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);

                    return RedirectToAction("Index", "User");
                }
            }
            catch (FirebaseAuthException ex) 
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx!.error.message);
                return View(registerModel);
            }

            return View();

        }

        // Login
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
                    HttpContext.Session.SetString("_UserToken", token);

                    return RedirectToAction("Index", "User");
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

        // Forgot Password
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                Console.WriteLine(forgotPasswordModel.Email);
                await auth.SendPasswordResetEmailAsync(forgotPasswordModel.Email);
                ViewBag.Message = $"A password reset link has been sent on {forgotPasswordModel.Email}";
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx!.error.message);
                return View(forgotPasswordModel);
            }
            catch(HttpRequestException ex)
            {
                ModelState.AddModelError(String.Empty, "INVALID_EMAIL");
                return View(forgotPasswordModel);
            }
            return View();
        }


        /*// Verify Email
        public async Task<IActionResult> VerifyEmail()
        {
            var token = HttpContext.Session.GetString("_UserToken");
            if (token != null)
            {
                try
                {
                    await auth.SendEmailVerificationAsync(token);
                    ViewBag.Message = $"A verification email has been sent on registered email, Please verify";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }*/

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_UserToken");
            return RedirectToAction("Login");
        }



        // Login
        public IActionResult Authentication()
        {
            var token = HttpContext.Session.GetString("_UserToken");

            if (token != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }


    }
}
