using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_chat.Data.Repository;
using Online_chat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_chat.Models;

namespace Online_chat.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(IRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public IActionResult Index() 
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Chat", "Home");

            return View(); 
        }

        public async Task<IActionResult> Chat(string contactId)
        {
            ChatViewModel viewModel = new ChatViewModel();

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            viewModel.Contacts = currentUser.Contacts.
                Select(contact => new ContactViewModel
                {
                    UserName = contact.UserName,
                    Name = contact.FirstName,
                    LastName = contact.LastName,
                    ContactId = contact.Id
                }).
                ToList();

            if(viewModel.Contacts.Count > 0)
                viewModel.CurrentContact = viewModel.Contacts.First();
            else
                viewModel.CurrentContact = new ContactViewModel();

            viewModel.Messages = _repository.GetMessages(currentUser, _repository.GetUser(contactId)).
                Select(message => new MessageViewModel
                {
                    Content = message.Content,
                }).
                ToList();
            return View(viewModel);
        }
    }
}
