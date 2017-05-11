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
    /// <summary>
    /// Provides controllers that are connected to the editor
    /// </summary>
    [Authorize]
    public class EditorController : Controller
    {
        EditorService _service = new EditorService(null);

        /// <summary>
        /// Gets the idex of a file and returns the view of the editor
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Index(int ID)
        {
            if(!_service.AuthorizeFileAccess(User.Identity.GetUserId(), ID))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            EditorViewModel model = new EditorViewModel();
            model.File = _service.GetFile(ID);
            return View(model);
        }

        /// <summary>
        /// Posts the save for any edited code
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult Save(int ID, SaveViewModel model)
        {
            _service.SaveFile(Server.HtmlEncode(model.Data), ID);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}