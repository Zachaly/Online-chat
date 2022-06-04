using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_chat.Data.Repository;
using Online_chat.Models;
using Online_chat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_chat.Controllers
{
    public class AuthorisationController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private IRepository _repository;
        private UserManager<ApplicationUser> _userManager;
        public AuthorisationController(SignInManager<ApplicationUser> signInManager, IRepository repository, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _repository = repository;
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
            var user = new ApplicationUser
            {
                UserName = viewModel.UserName,
                FirstName = viewModel.Name,
                LastName = viewModel.Name,
                Email = viewModel.Email,
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
