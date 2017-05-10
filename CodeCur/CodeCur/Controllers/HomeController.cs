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
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CodeCur.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        EditorService editorService = new EditorService(null);
        NavService service = new NavService(null);

        public ActionResult Index()
        {
            ProjectViewModel model = new ProjectViewModel();
            model.Projects = service.GetUserProjects(User.Identity.GetUserId());
            model.Owners = new List<string>();
            foreach (var project in model.Projects)
            {
                model.Owners.Add(service.GetUserName(project.UserID));
            }
            return View(model);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult Project(int ID)
        {
            if (!service.AuthorizeProjectAccess(User.Identity.GetUserId(), ID))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            ProjectDetailsViewModel model = new ProjectDetailsViewModel();
            model.Files = service.GetProjectFiles(ID);
            model.ProjectID = ID;
            model.ProjectName = service.GetProjectName(ID);
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
                    DateCreated = DateTime.Now,
                    UserID = User.Identity.GetUserId()
                };

                service.AddProjectToDb(project);

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
                service.AddFileToDb(defaultFile);
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
                if (!service.ValidFileName(file.Name, file.Type, model.ProjectID))
                {
                    ModelState.AddModelError("duplicateFileError", "That filename already excists in this project!");
                }
                else
                {
                    service.AddFileToDb(file);
                    return RedirectToAction("Project", "Home", new { id = model.ProjectID });
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ShareProject(int ID)
        {
            if (!service.AuthorizeProjectAccess(User.Identity.GetUserId(), ID))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            ShareProjectViewModel model = new ShareProjectViewModel();
            model.ProjectID = ID;
            model.ProjectName = service.GetProjectName(ID);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ShareProject(ShareProjectViewModel model)
        {
            if (!service.DoesUserExist(model.UserName))
            {
                ModelState.AddModelError("shareError", "There is no user by that username!");
            }
            else if (service.AlreadyHasAccesss(model.UserName, model.ProjectID))
            {
                ModelState.AddModelError("shareError", "This user has already been added!");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    service.AddUserProjectRelationByName(model.UserName, model.ProjectID);
                    return RedirectToAction("Project", "Home", new { id = model.ProjectID });
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteProject(DeleteProjectViewModel model)
        {
            if (!service.AuthorizeProjectAccess(User.Identity.GetUserId(), model.ID))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            service.DeleteAllFiles(model.ID);
            service.DeleteProject(model.ID);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RemoveFromProject(RemoveFromProjectViewModel model)
        {
            if (!service.AuthorizeProjectAccess(User.Identity.GetUserId(), model.ID))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            service.RemoveUserFromProject(model.ID, User.Identity.GetUserId());
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteFile(DeleteFileViewModel model)
        {
            if (!editorService.AuthorizeFileAccess(User.Identity.GetUserId(), model.ID))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            service.DeleteFile(model.ID);
            return RedirectToAction("Project", "Home", new { id = model.ProjectID });
        }
    }
}