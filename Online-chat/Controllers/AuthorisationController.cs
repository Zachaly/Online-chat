using Blog.Data.FileManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_chat.Models;
using Online_chat.ViewModels;
using System;
using System.Threading.Tasks;

namespace Online_chat.Controllers
{
    public class AuthorisationController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileManager _fileManager;
        public AuthorisationController(SignInManager<ApplicationUser> signInManager, IFileManager fileManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _fileManager = fileManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, false, false);

                if (result.Succeeded)
                    return RedirectToAction("Chat", "Home");
            }
            catch (Exception)
            {
                return View(viewModel);
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if(viewModel.Password != viewModel.ConfirmPassword)
                return RedirectToAction("Register", "Authorisation");

            string profileImg;

            if (viewModel.ProfilePicture != null)
                profileImg = await _fileManager.SaveImage(viewModel.ProfilePicture, true);
            else
                profileImg = "default.jpg";
            

            var user = new ApplicationUser
            {
                UserName = viewModel.UserName,
                FirstName = viewModel.Name,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                ProfilePicture = profileImg
            };

            try
            {
                var result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                    return RedirectToAction("Login", "Authorisation");

                return RedirectToAction("Register", "Authorisation");
            }
            catch (Exception)
            {
                return RedirectToAction("Register", "Authorisation");
            }
        }
    }
}
