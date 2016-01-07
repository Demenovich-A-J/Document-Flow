using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using BL.Handlers.DocumentHandlers;
using BL.Handlers.DocumentTemplatesHandlers;
using BL.Handlers.DocumentTypeHandlers;
using BL.Handlers.PositionsHandlers;
using BL.Handlers.UsersHandlers;
using BL.Interfaces;
using DocumentFlow.Models;
using DocumentFlow.Models.AuthorizeAttribute;
using DocumentType = BL.Models.DocumentType;
using Position = BL.Models.Position;
using User = BL.Models.User;

namespace DocumentFlow.Controllers
{
    public class DocumentTemplateController : Controller
    {
        public DocumentTemplateController()
        {
            _templatesHandler =
                new DocumentTemplatesRepositoryHandler();
            _positionsHandler =
                new PositionsRepositoryHandler();
            _usersRepositoryHandler =
                new UsersRepositoryHandler();
            _typeRepositoryHandler =
                new DocumentTypesRepositoryHandler();
        }

        public static DocumentTemplate ConvertToModel(BL.Models.DocumentTemplate template)
        {
            Mapper.CreateMap<BL.Models.DocumentTemplate, DocumentTemplate>();
            Mapper.CreateMap<DocumentType, Models.DocumentType>();

            return Mapper.Map<BL.Models.DocumentTemplate, DocumentTemplate>(template);
        }

        public static BL.Models.DocumentTemplate ConvertToBlModel(DocumentTemplate template)
        {
            Mapper.CreateMap<DocumentTemplate, BL.Models.DocumentTemplate>();
            Mapper.CreateMap<Models.DocumentType, DocumentType>();

            return Mapper.Map<DocumentTemplate, BL.Models.DocumentTemplate>(template);
        }

        public string GetUserName()
        {
            var user = GetUser();

            var fullName = "";
            if (user != null)
            {
                fullName = user.FirstName + " " + user.LastName + " " + user.Patronymic;
            }
            return fullName;
        }

        private User GetUser()
        {
            var userName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var user = _usersRepositoryHandler.GetUserByEmail(userName);
            return user;
        }

        [HttpPost]
        [ValidateInput(false)]
        [CustomAuthorize("Admin")]
        public ActionResult ConvertView(DocumentTemplate template)
        {
            _documentRepositoryHandler =
                new HtmlDocumentHandler(GetUserName());
            DocumentTemplate newTemplate = null;
            if (template.Text != null)
            {
                newTemplate = ConvertToModel
                    (_documentRepositoryHandler.ConvertView(ConvertToBlModel(template)));
            }

            return View("Preview", newTemplate);
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult DocumentTemplates()
        {
            var templates = _templatesHandler.GetAll();
            return View(templates.Select(ConvertToModel));
        }

        [HttpGet]
        public ActionResult CreateTemplate()
        {
            var positions = _positionsHandler.GetAll().Select(PositionController.ConvertToModel);
            ViewBag.Positions = positions;

            ViewBag.Types = new DocumentTypesHandler().TypesSelectList();

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [CustomAuthorize("Admin")]
        public ActionResult CreateTemplate(DocumentTemplate template)
        {
            _templatesHandler.Add(ConvertToBlModel(template));
            return RedirectToAction("DocumentTemplates");
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult EditTemplate(int id)
        {
            ViewBag.Positions = _positionsHandler.GetAll().Select(PositionController.ConvertToModel);
            ViewBag.Types =
                _typeRepositoryHandler.GetAll()
                    .Select(DocumentTypeController.ConvertToModel)
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });

            var template = _templatesHandler.FindById(id);

            return View(ConvertToModel(template));
        }

        [HttpPost]
        [ValidateInput(false)]
        [CustomAuthorize("Admin")]
        public ActionResult EditTemplate(DocumentTemplate template)
        {
            _templatesHandler.Update(ConvertToBlModel(template));
            return RedirectToAction("DocumentTemplates");
        }


        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult RemoveTemplate(int id)
        {
            var template = _templatesHandler.FindById(id);
            _templatesHandler.Remove(template);
            return RedirectToAction("DocumentTemplates");
        }

        #region handlers

        private HtmlDocumentHandler _documentRepositoryHandler;

        private readonly IRepositoryHandler<BL.Models.DocumentTemplate> _templatesHandler;
        private readonly IRepositoryHandler<Position> _positionsHandler;
        private readonly IRepositoryHandler<DocumentType> _typeRepositoryHandler;
        private readonly UsersRepositoryHandler _usersRepositoryHandler;

        #endregion
    }
}