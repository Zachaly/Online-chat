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
using Blog.Data.FileManager;

namespace Online_chat.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;
        private UserManager<ApplicationUser> _userManager;
        private IFileManager _fileManager;

        private static ApplicationUser _currentContact;

        public HomeController(IRepository repository, UserManager<ApplicationUser> userManager, IFileManager fileManager)
        {
            _repository = repository;
            _userManager = userManager;
            _fileManager = fileManager;
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

            currentUser.Contacts = _repository.GetContacts(currentUser);

            viewModel.Contacts = currentUser.Contacts.
                Select(contact => _repository.GetUser(contact.ContactUserId)).
                Select(contact => new ContactViewModel
                {
                    UserName = contact.UserName,
                    Name = contact.FirstName,
                    LastName = contact.LastName,
                    ContactId = contact.Id,
                    ProfilePicture = contact.ProfilePicture,
                }).
                ToList();

            if(viewModel.Contacts.Count > 0 && !string.IsNullOrEmpty(contactId))
                viewModel.CurrentContact = viewModel.Contacts.First(contact => contact.ContactId == contactId);
            else
                viewModel.CurrentContact = new ContactViewModel();

            _currentContact = _repository.GetUser(viewModel.CurrentContact.ContactId);

            var sender = _repository.GetUser(contactId);

            if (sender != null)
                viewModel.Messages = _repository.GetMessages(sender, currentUser).
                    Select(message => new MessageViewModel
                    {
                        Content = message.Content,
                        Sender = message.SenderId,
                        Created = message.Created,
                        ImgString = message.Image
                    }).
                    ToList();
            else
                viewModel.Messages = new List<MessageViewModel>();

            viewModel.UserId = currentUser.Id;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Message(MessageViewModel viewModel)
        {
            if (_currentContact is null)
                return RedirectToAction("Chat");

            var message = new Message
            {
                Content = viewModel.Content,
                SenderId = (await _userManager.GetUserAsync(HttpContext.User)).Id,
                ReceiverId = _currentContact?.Id,
            };

            if (viewModel.Image != null)
                message.Image = await _fileManager.SaveImage(viewModel.Image);

            _repository.AddMessage(message);

            await _repository.SaveChanges();

            return RedirectToAction("Chat", "Home", new { contactId = _currentContact.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel contactViewModel)
        {
            if(!_repository.GetUsers().
                Any(user => user.UserName == contactViewModel.UserName) ||
                (await _userManager.GetUserAsync(HttpContext.User)).Contacts.
                Select(contact => _repository.GetUser(contact.ContactUserId)).
                Any(user => user.UserName == contactViewModel.UserName))
                return RedirectToAction("Chat");

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var contactId = _repository.GetUsers().First(user => user.UserName == contactViewModel.UserName).Id;

            if(user.Id == contactId)
                return RedirectToAction("Chat");

            var contact = _repository.GetUser(contactId);

            _repository.AddContact(user, contact);

            await _repository.SaveChanges();

            return RedirectToAction("Chat", "Home", new { contactId = contactId });
        }

        [HttpGet("/Images/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }

        [HttpGet("/ProfilePicture/{image}")]
        public IActionResult ProfilePicture(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ProfilePictureStream(image), $"image/{mime}");
        }
    }
}
