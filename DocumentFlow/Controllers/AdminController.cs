using System.Threading.Tasks;
using System.Web.Mvc;
using BL.AbstractClasses;
using BL.DocumentHandler;
using BL.DocumentHandlers;
using BL.DocumentTemplatesHandlers;
using BL.DocumentTypeHandlers;
using BL.PositionsHandler;
using BL.RolesHandlers;
using BL.UsersHandlers;
using EntityModels;

namespace DocumentFlow.Controllers
{
    public class AdminController : Controller
    {
        protected HtmlDocumentHandler DocumentHandler;

        public AdminController()
        {
            DocumentHandler = new HtmlDocumentHandler(AccountController.FullName);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ConvertView(DocumentTemplate template)
        {
            if (template.Text != null)
            {
                template = DocumentHandler.ConvertView(template);
            }

            return View("Preview/Preview",template);
        }

        #region User

        public ActionResult Users()
        {
            return View("Index/Users", _usersHandler.GetAll(x => true));
        }

        #endregion

        #region Handlers

        protected static RepositoryHandler<DocumentTemplate> _templatesHandler =
            new DocumentTemplatesRepositoryHandler();

        protected static RepositoryHandler<Position> _positionsHandler =
            new PositionsRepositoryHandler();

        protected static RepositoryHandler<DocumentType> _documentTypesHandler =
            new DocumentTypesRepositoryHandler();

        protected static RepositoryHandler<Document> _documentsHandler =
            new DocumentsRepositoryHandler();

        protected static RepositoryHandler<Role> _rolesHandler =
            new RolesRepositoryHandler();

        protected static RepositoryHandler<User> _usersHandler =
            new UsersRepositoryHandler();

        #endregion

        #region Template

        public ActionResult DocumentTemplates()
        {
            var templates = _templatesHandler.GetAll(x => true);
            return View("Index/DocumentTemplates", templates);
        }

        public ActionResult EditTemplate()
        {
            return View("Edit/EditTemplate");
        }

        [HttpGet]
        public ActionResult CreateTemplate()
        {
            var positions = _positionsHandler.GetAll(x => true);
            ViewBag.Positions = positions;

            return View("Create/CreateTemplate", new DocumentTemplate());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateTemplate(DocumentTemplate template)
        {
            template.Name = "Default1";
            template.TypeId = 1;

            _templatesHandler.Add(template);

            return RedirectToAction("DocumentTemplates", "Admin");
        }

        [HttpGet]
        public async Task<ActionResult> EditTemplate(int id)
        {
            var positions = _positionsHandler.GetAll(x => true);
            ViewBag.Positions = positions;

            var template = await _templatesHandler.FindById(id);
            return View("Edit/EditTemplate", template);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditTemplate(DocumentTemplate template)
        {
            _templatesHandler.Update(template);
            return RedirectToAction("DocumentTemplates", "Admin");
        }


        [HttpGet]
        public async Task<ActionResult> RemoveTemplate(int id)
        {
            var template = await _templatesHandler.FindById(id);
            _templatesHandler.Remove(template);
            return RedirectToAction("DocumentTemplates");
        }

        #endregion

        #region Role

        public ActionResult Roles()
        {
            return View("Index/Roles", _rolesHandler.GetAll(x => true));
        }

        public ActionResult CreateRole()
        {
            return View("Create/CreateRole");
        }

        [HttpPost]
        public ActionResult CreateRole(Role role)
        {
            if (ModelState.IsValid)
            {
                _rolesHandler.Add(role);
            }
            return RedirectToAction("Roles");
        }

        public async Task<ActionResult> EditRole(int id)
        {
            if (ModelState.IsValid)
            {
                var role = await _rolesHandler.FindById(id);

                if (role != null)
                {
                    return View("Edit/EditRole", role);
                }
            }
            return RedirectToAction("Roles");
        }

        [HttpPost]
        public ActionResult EditRole(Role role)
        {
            if (ModelState.IsValid)
            {
                _rolesHandler.Update(role);
            }
            return RedirectToAction("Roles");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var role = await _rolesHandler.FindById(id);

            if (role != null)
            {
                _rolesHandler.Remove(role);
            }
            return RedirectToAction("Roles");
        }

        #endregion

        #region DocumentType

        public ActionResult DocumentTypes()
        {
            return View("Index/DocumentTypes", _documentTypesHandler.GetAll(x => true));
        }

        public ActionResult CreateDocumentType()
        {
            return View("Create/CreateDocumentType");
        }

        public async Task<ActionResult> EditDocumentType(int id)
        {
            var type = await _documentTypesHandler.FindById(id);
            return View("Edit/EditDocumentType", type);
        }

        [HttpPost]
        public ActionResult EditDocumentType(DocumentType documentType)
        {
            _documentTypesHandler.Update(documentType);
            return RedirectToAction("DocumentTypes");
        }

        [HttpPost]
        public ActionResult CreateDocumentType(DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                _documentTypesHandler.Add(documentType);
                return RedirectToAction("DocumentTypes");
            }
            return View("Create/CreateDocumentType", documentType);
        }

        [HttpGet]
        public async Task<ActionResult> RemoveDocumentType(int id)
        {
            var type = await _documentTypesHandler.FindById(id);
            if (type != null)
            {
                _documentTypesHandler.Remove(type);
            }
            return RedirectToAction("DocumentTypes");
        }

        #endregion

        #region Position

        [HttpGet]
        public ActionResult Positions()
        {
            return View("Index/Positions", _positionsHandler.GetAll(x => true));
        }

        [HttpGet]
        public ActionResult CreatePosition()
        {
            return View("Create/CreatePosition");
        }

        [HttpPost]
        public ActionResult CreatePosition(Position position)
        {
            if (ModelState.IsValid)
            {
                _positionsHandler.Add(position);
            }

            return RedirectToAction("Positions");
        }

        [HttpGet]
        public async Task<ActionResult> EditPosition(int id)
        {
            var position = await _positionsHandler.FindById(id);
            return View("Edit/EditPosition", position);
        }

        [HttpPost]
        public ActionResult EditPosition(Position position)
        {
            _positionsHandler.Update(position);
            return RedirectToAction("Positions");
        }

        [HttpGet]
        public async Task<ActionResult> DeletePosition(int id)
        {
            var position = await _positionsHandler.FindById(id);
            if (position != null)
            {
                _positionsHandler.Remove(position);
            }
            return RedirectToAction("Positions");
        }

        #endregion
    }
}