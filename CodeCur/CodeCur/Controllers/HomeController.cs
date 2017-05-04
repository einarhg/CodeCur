using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;
using CodeCur.Models.ViewModels;
using CodeCur.Models.Entities;
using Microsoft.AspNet.Identity;
using CodeCur.Services;

namespace CodeCur.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Projects = NavService.GetUserProjects(User.Identity.GetUserId());
            return View(model);
        }
        public ActionResult Project()
        {
            //TODO
            return View();
        }

        [AllowAnonymous]
        public ActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject(CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    Name = model.Name,
                    Type = model.ProjectType,
                    DateCreated =  DateTime.Now,
                    UserID = User.Identity.GetUserId()
                };
              
                NavService.AddProjectToDb(project);
                return RedirectToAction("Index", "Home");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}