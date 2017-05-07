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
using System.Net;

namespace CodeCur.Controllers
{
    [Authorize]
    public class EditorController : Controller
    {
        // GET: Editor
        public ActionResult Index(int ID)
        {
            EditorViewModel model = new EditorViewModel();
            model.File = EditorService.GetFile(ID);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Save(int ID, SaveViewModel model)
        {
            EditorService.SaveFile(model.Data, ID);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Editor()
        {
            //TODO
            return View();
        }
    }
}