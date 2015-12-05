using System.Web.Mvc;
using BL.AbstractClasses;
using BL.DocumentTypeHandlers;
using EntityModels;

namespace DocumentFlow.Controllers
{
    public class MainController : Controller
    {
        protected static RepositoryHandler<DocumentType> _documentTypesHandler =
            new DocumentTypesRepositoryHandler();

        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DocumentTemplates()
        {
            var templates = _documentTypesHandler.GetAll(x => true);

            return View(templates);
        }
    }
}