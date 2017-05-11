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
    /// <summary>
    /// Provides all the fundemental functions in the home view
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        EditorService _editorService = new EditorService(null);
        NavService _service = new NavService(null);

        /// <summary>
        /// Displays all projest this user created and/or is a part of
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ProjectViewModel model = new ProjectViewModel();
            model.Projects = _service.GetUserProjects(User.Identity.GetUserId());
            model.Owners = new List<string>();
            foreach (var project in model.Projects)
            {
                model.Owners.Add(_service.GetUserName(project.UserID));
            }
            return View(model);
        }

        /// <summary>
        /// If the user is trying to access something he should not be accessing
        /// </summary>
        /// <returns></returns>
        public ActionResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// If project is valid it is created with an automatic index file
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Project(int ID)
        {
            if (!_service.AuthorizeProjectAccess(User.Identity.GetUserId(), ID))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            ProjectDetailsViewModel model = new ProjectDetailsViewModel();
            model.Files = _service.GetProjectFiles(ID);
            model.ProjectID = ID;
            model.ProjectName = _service.GetProjectName(ID);
            return View(model);
        }

        /// <summary>
        /// Gets the view for create project
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CreateProject()
        {
            return View();
        }

        /// <summary>
        /// Posts the view if model is valid for create project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

                _service.AddProjectToDb(project);

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
                _service.AddFileToDb(defaultFile);
                return RedirectToAction("Index", "Home");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Gets the crate view model
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CreateFile(int ID)
        {
            CreateFileViewModel model = new CreateFileViewModel();
            model.ProjectID = ID;
            return View(model);
        }

        /// <summary>
        /// Post the view if model state is valid for create file
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                // Checks if 2 files have the same name and type
                if (!_service.ValidFileName(file.Name, file.Type, model.ProjectID))
                {
                    ModelState.AddModelError("duplicateFileError", "That filename already excists in this project!");
                }
                else
                {
                    _service.AddFileToDb(file);
                    return RedirectToAction("Project", "Home", new { id = model.ProjectID });
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Gets the model for share share project and returns the view
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ShareProject(int ID)
        {
            if (!_service.AuthorizeProjectAccess(User.Identity.GetUserId(), ID))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            ShareProjectViewModel model = new ShareProjectViewModel();
            model.ProjectID = ID;
            model.ProjectName = _service.GetProjectName(ID);
            return View(model);
        }

        /// <summary>
        /// Posts the view for share project if model state is valid
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ShareProject(ShareProjectViewModel model)
        {
            if (!_service.DoesUserExist(model.UserName))
            {
                ModelState.AddModelError("shareError", "There is no user by that username!");
            }
            else if (_service.AlreadyHasAccesss(model.UserName, model.ProjectID))
            {
                ModelState.AddModelError("shareError", "This user has already been added!");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _service.AddUserProjectRelationByName(model.UserName, model.ProjectID);
                    return RedirectToAction("Project", "Home", new { id = model.ProjectID });
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Posts the view without the removed project if model is valid, then the project is deleted from the database. Redirects to access denied otherwise
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteProject(DeleteProjectViewModel model)
        {
            if (!_service.AuthorizeProjectAccess(User.Identity.GetUserId(), model.ID))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            _service.DeleteAllFiles(model.ID);
            _service.DeleteProject(model.ID);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Posts the view without the removed project if model state is valid, redirects to access denied
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult RemoveFromProject(RemoveFromProjectViewModel model)
        {
            if (!_service.AuthorizeProjectAccess(User.Identity.GetUserId(), model.ID))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            _service.RemoveUserFromProject(model.ID, User.Identity.GetUserId());
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Posts the view without the deleted project if model state is valid, redirect to access denied
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteFile(DeleteFileViewModel model)
        {
            if (!_editorService.AuthorizeFileAccess(User.Identity.GetUserId(), model.ID))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            _service.DeleteFile(model.ID);
            return RedirectToAction("Project", "Home", new { id = model.ProjectID });
        }
    }
}