using System.Threading.Tasks;
using System.Web.Mvc;
using BL.AbstractClasses;
using BL.DocumentHandler;
using BL.DocumentTemplatesHandlers;
using BL.DocumentTypeHandlers;
using BL.PositionsHandler;
using BL.RolesHandlers;
using BL.UsersHandlers;
using EntityModels;
using BL.DocumentHandlers;
using BL.PositionsHandlers;

namespace DocumentFlow.Controllers
{
    public class AdminController : Controller
    {

        #region Handlers

        protected HtmlDocumentHandler DocumentHandler;

        protected static RepositoryHandler<DocumentTemplate> TemplatesHandler =
            new DocumentTemplatesRepositoryHandler();

        protected static RepositoryHandler<Position> _positionsHandler =
            new PositionsRepositoryHandler();

        protected static PositionsHandler PositionsHandler = new PositionsHandler();

        protected static RepositoryHandler<DocumentType> DocumentTypesHandler =
            new DocumentTypesRepositoryHandler();

        protected static RepositoryHandler<Document> DocumentsHandler =
            new DocumentsRepositoryHandler();

        protected static RepositoryHandler<Role> RolesHandler =
            new RolesRepositoryHandler();

        protected static RolesHandler rolesHandler = new RolesHandler();

        protected static RepositoryHandler<User> UsersHandler =
            new UsersRepositoryHandler();

        protected static DocumentTypesHandler TypesHtmlHandler =
            new DocumentTypesHandler();

        #endregion

        public AdminController()
        {
            DocumentHandler = new HtmlDocumentHandler(AccountController.FullName);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ConvertView(DocumentTemplate template)
        {
            if (template.Text != null)
            {
                template = await DocumentHandler.ConvertView(template);
            }

            return View("Preview/Preview", template);
        }

        #region User

        public ActionResult Users()
        {
            @ViewBag.Positions = _positionsHandler.GetAll(x => true);
            @ViewBag.Roles = RolesHandler.GetAll(x => true);
            return View("Index/Users", UsersHandler.GetAll(x => true));
        }

        public async Task<ActionResult> EditUser(int id)
        {
            ViewBag.Positions = PositionsHandler.PositionsSelectList();
            ViewBag.Roles = rolesHandler.RolesSelectList();

            if (ModelState.IsValid)
            {
                var user = await UsersHandler.FindById(id);

                if (user != null)
                {
                    return View("Edit/EditUser", user);
                }
            }
            return RedirectToAction("Users");
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            ViewBag.Positions = PositionsHandler.PositionsSelectList();
            ViewBag.Roles = rolesHandler.RolesSelectList();

            if (ModelState.IsValid)
            {
                UsersHandler.Update(user);
            }
            return RedirectToAction("Users");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var role = await UsersHandler.FindById(id);

            if (role != null)
            {
                UsersHandler.Remove(role);
            }
            return RedirectToAction("Users");
        }
        #endregion

        #region Template

        public ActionResult DocumentTemplates()
        {
            var templates = TemplatesHandler.GetAll(x => true);
            return View("Index/DocumentTemplates", templates);
        }

        [HttpGet]
        public ActionResult CreateTemplate()
        {
            var positions = _positionsHandler.GetAll(x => true);
            ViewBag.Positions = positions;

            var t = TypesHtmlHandler.TypesSelectList();
            ViewBag.Types = TypesHtmlHandler.TypesSelectList();

            return View("Create/CreateTemplate", new DocumentTemplate());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateTemplate(DocumentTemplate template)
        {
            TemplatesHandler.Add(template);

            return RedirectToAction("DocumentTemplates", "Admin");
        }

        [HttpGet]
        public async Task<ActionResult> EditTemplate(int id)
        {
            var positions = _positionsHandler.GetAll(x => true);
            ViewBag.Positions = positions;

            var template = await TemplatesHandler.FindById(id);
            return View("Edit/EditTemplate", template);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditTemplate(DocumentTemplate template)
        {
            TemplatesHandler.Update(template);
            return RedirectToAction("DocumentTemplates", "Admin");
        }


        [HttpGet]
        public async Task<ActionResult> RemoveTemplate(int id)
        {
            var template = await TemplatesHandler.FindById(id);
            TemplatesHandler.Remove(template);
            return RedirectToAction("DocumentTemplates");
        }

        #endregion

        #region Role

        public ActionResult Roles()
        {
            return View("Index/Roles", RolesHandler.GetAll(x => true));
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
                RolesHandler.Add(role);
            }
            return RedirectToAction("Roles");
        }

        public async Task<ActionResult> EditRole(int id)
        {
            if (ModelState.IsValid)
            {
                var role = await RolesHandler.FindById(id);

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
                RolesHandler.Update(role);
            }
            return RedirectToAction("Roles");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var role = await RolesHandler.FindById(id);

            if (role != null)
            {
                RolesHandler.Remove(role);
            }
            return RedirectToAction("Roles");
        }

        #endregion

        #region DocumentType

        public ActionResult DocumentTypes()
        {
            return View("Index/DocumentTypes", DocumentTypesHandler.GetAll(x => true));
        }

        public ActionResult CreateDocumentType()
        {
            return View("Create/CreateDocumentType");
        }

        public async Task<ActionResult> EditDocumentType(int id)
        {
            var type = await DocumentTypesHandler.FindById(id);
            return View("Edit/EditDocumentType", type);
        }

        [HttpPost]
        public ActionResult EditDocumentType(DocumentType documentType)
        {
            DocumentTypesHandler.Update(documentType);
            return RedirectToAction("DocumentTypes");
        }

        [HttpPost]
        public ActionResult CreateDocumentType(DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                DocumentTypesHandler.Add(documentType);
                return RedirectToAction("DocumentTypes");
            }
            return View("Create/CreateDocumentType", documentType);
        }

        [HttpGet]
        public async Task<ActionResult> RemoveDocumentType(int id)
        {
            var type = await DocumentTypesHandler.FindById(id);
            if (type != null)
            {
                DocumentTypesHandler.Remove(type);
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