using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using BL.Handlers.DocumentHandlers;
using BL.Handlers.DocumentTemplatesHandlers;
using BL.Handlers.StepsHandlers;
using BL.Handlers.UsersDocumentsHandler;
using BL.Handlers.UsersHandlers;
using BL.Interfaces;
using DocumentFlow.Models.AuthorizeAttribute;
using BlModels = BL.Models;

namespace DocumentFlow.Controllers
{
    public class MainController : Controller
    {
        private readonly IRepositoryHandler<BlModels.DocumentTemplate> _templatesRepositoryHandler =
            new DocumentTemplatesRepositoryHandler();

        private readonly UsersRepositoryHandler _usersRepositoryHandler =
            new UsersRepositoryHandler();

        private DocumentsRepositoryHandler _documentsRepositoryHandler =
            new DocumentsRepositoryHandler();

        // GET: Main
        public ActionResult Index()
        {
            ViewBag.FullName = GetUserName();
            return RedirectToAction("Inbox");
        }

        [HttpGet]
        [CustomAuthorize("User","Admin")]
        public ActionResult DocumentTemplates()
        {
            var templates = _templatesRepositoryHandler.GetAll();
            var viewTemplates = templates.Select(DocumentTemplateController.ConvertToModel);

            ViewBag.FullName = GetUserName();
            return View(viewTemplates);
        }

        public string GetUserName()
        {
            var user = GetUser();

            var fullName = "";
            if (user != null)
            {
                fullName = user.LastName + " " + user.FirstName + " " + user.Patronymic;
            }
            return fullName;
        }

        [CustomAuthorize("User", "Admin")]
        protected BlModels.User GetUser()
        {
            var userName = FormsAuthentication.Decrypt
                (Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var user = _usersRepositoryHandler.GetUserByEmail(userName);
            return user;
        }

        [HttpGet]
        [CustomAuthorize("User", "Admin")]
        public ActionResult FillDocument(int id)
        {
            var template = _templatesRepositoryHandler.FindById(id);
            template = new HtmlDocumentHandler(GetUserName()).ConvertView(template);

            ViewBag.FullName = GetUserName();
            return View(DocumentTemplateController.ConvertToModel(template));
        }


        [HttpGet]
        [CustomAuthorize("User", "Admin")]
        public ActionResult SendDocument(string json)
        {
            object[] jsonArray = new JavaScriptSerializer()
                .Deserialize<dynamic>(json);

            var user = GetUser();
            if (user == null) return RedirectToAction("Login", "Account");
            new DocumentCreationHandler(user.Id).ParseInputToDocument(jsonArray);
            ViewBag.FullName = GetUserName();
            return RedirectToAction("Outbox");
        }

        [HttpGet]
        [CustomAuthorize("User", "Admin")]
        public ActionResult Outbox()
        {
            var user = GetUser();
            if (user == null) return RedirectToAction("Login", "Account");

            var documents = new UsersDocumentsHandler().Outbox(user.Id);
            ViewBag.FullName = GetUserName();
            return View(documents.Select(DocumentController.ConvertToModel).ToList());
        }

        [HttpGet]
        [CustomAuthorize("User", "Admin")]
        public ActionResult Inbox()
        {
            var user = GetUser();
            if (user == null) return RedirectToAction("Login", "Account");
            var documents = new UsersDocumentsHandler().Inbox(user.Id);
            ViewBag.FullName = GetUserName();
            return View(documents.Select(DocumentController.ConvertToModel).ToList());
        }

        [HttpGet]
        [CustomAuthorize("User", "Admin")]
        public ActionResult OpenDocument(int id)
        {
            var view = new DocumentViewCreator().GetDocumentView(id);
            ViewBag.FullName = GetUserName();
            return View(DocumentViewController.ConvertToModel(view));
        }

        [HttpGet]
        [CustomAuthorize("User", "Admin")]
        public ActionResult SigningDocument(int id)
        {
            var view = new DocumentViewCreator().GetDocumentView(id);
            ViewBag.FullName = GetUserName();
            return View(DocumentViewController.ConvertToModel(view));
        }

        [HttpGet]
        [CustomAuthorize("User", "Admin")]
        public ActionResult SignDocument(int id)
        {
            new SigningHandler().Sign(id);
            return RedirectToAction("Outbox");
        }

        [HttpGet]
        [CustomAuthorize("User", "Admin")]
        public ActionResult RejectDocument(int id)
        {
            new SigningHandler().Reject(id);
            return RedirectToAction("Outbox");
        }

        [HttpGet]
        [CustomAuthorize("User", "Admin")]
        public ActionResult DeleteDocument(int id)
        {
            var document = _documentsRepositoryHandler.FindById(id);
            if (document != null)
            {
                _documentsRepositoryHandler.Remove(document);
            }
            return RedirectToAction("Outbox");
        }
    }
}