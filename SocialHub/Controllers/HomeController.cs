using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialHub.Models;
using SocialHub.ViewModels;

namespace SocialHub.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment hostingEnvironment;

        // Constructor Injection 
        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository,
            IWebHostEnvironment hostingEnvironment)
        {
            _userRepository = userRepository;
            _logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }


        public ViewResult Index()
        {
            var model = _userRepository.GetAllUsers();
            return View(model);
        }

        public ViewResult Details(int? id)
        {
            User user = _userRepository.GetUser(id.Value);

            if (user == null)
            {
                Response.StatusCode = 404;

                return View("UserNotFound", id.Value);
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                User = user,
                PageTitle = "User Details"
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            User user = _userRepository.GetUser(id);

            UserEditViewModel userEdit = new UserEditViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ExistingPhotoPath = user.PhotoPath
            };

            return View(userEdit);
        }

        [HttpPost]
        public IActionResult Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _userRepository.GetUser(model.Id);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        // Get full file name 
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    user.PhotoPath = ProcessUploadedFile(model);
                }
                 
                User updatedUser = _userRepository.Update(user);
                return RedirectToAction("Index");
            }
            return View(); 
        }

        private string ProcessUploadedFile(UserCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath + "\\images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(fileStream);
                    }
                    
            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                string uniqueFileName = ProcessUploadedFile(model);
                User newUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhotoPath = uniqueFileName
                };

                _userRepository.Add(newUser);
                return RedirectToAction("Details", new { id = newUser.Id });
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
