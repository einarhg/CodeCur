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
            ProjectViewModel model = new ProjectViewModel();
            model.Projects = NavService.GetUserProjects(User.Identity.GetUserId());
            model.Owners = new List<string>();
            foreach (var project in model.Projects)
            {
                model.Owners.Add(NavService.GetUserName(project.UserID));
            }
            return View(model);
        }

        public ActionResult Project(int ID)
        {
            ProjectDetailsViewModel model = new ProjectDetailsViewModel();
            model.Files = NavService.GetProjectFiles(ID);
            model.ProjectID = ID;
            model.ProjectName = NavService.GetProjectName(ID);
            return View(model);
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

                //Creating default file
                File defaultFile = new File();
                defaultFile.ProjectID = project.ID;
                defaultFile.DateCreated = DateTime.Now;
                if (project.Type == "Website")
                {
                    defaultFile.Name += "index.html";
                    defaultFile.Type = "HTML";
                }
                else if (project.Type == "Mobile app")
                {
                    defaultFile.Name += "index.js";
                    defaultFile.Type = "JavaScript";
                }
                else
                {
                    defaultFile.Name += "index.txt";
                    defaultFile.Type = "TXT";
                }
                NavService.AddFileToDb(defaultFile);
                return RedirectToAction("Index", "Home");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult CreateFile(int ID)
        {
            CreateFileViewModel model = new CreateFileViewModel();
            model.ProjectID = ID;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFile(CreateFileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = new File
                {
                    Name = model.Name,
                    Type = model.FileType,
                    DateCreated = DateTime.Now,
                    ProjectID = model.ProjectID
                };
                if (file.Type == "JavaScript")
                {

                    file.Name = file.Name + ".js";
                }
                else if (file.Type == "HTML")
                {
                    file.Name = file.Name + ".html";
                }
                else if (file.Type == "CSS")
                {
                    file.Name = file.Name + ".css";
                }
                else
                {
                    file.Name = file.Name + ".txt";
                }

                NavService.AddFileToDb(file);
                return RedirectToAction("Project", "Home", new { id = model.ProjectID });
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ShareProject(int ID)
        {
            ShareProjectViewModel model = new ShareProjectViewModel();
            model.ProjectID = ID;
            model.ProjectName = NavService.GetProjectName(ID);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ShareProject(ShareProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                NavService.AddUserProjectRelationByName(model.UserName, model.ProjectID);
                return RedirectToAction("Project", "Home", new { id = model.ProjectID });
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}