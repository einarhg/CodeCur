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
        EditorService service = new EditorService(null);

        // GET: Editor
        public ActionResult Index(int ID)
        {
            if(!service.AuthorizeFileAccess(User.Identity.GetUserId(), ID))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            EditorViewModel model = new EditorViewModel();
            model.File = service.GetFile(ID);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult Save(int ID, SaveViewModel model)
        {
            service.SaveFile(Server.HtmlEncode(model.Data), ID);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Editor()
        {
            //TODO
            return View();
        }
    }
}