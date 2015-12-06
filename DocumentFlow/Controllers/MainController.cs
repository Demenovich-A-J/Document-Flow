using System.Web.Mvc;
using BL.AbstractClasses;
using BL.DocumentTypeHandlers;
using EntityModels;
using BL.DocumentTemplatesHandlers;
using System.Threading.Tasks;
using BL.DocumentFandler;


namespace DocumentFlow.Controllers
{
    public class MainController : Controller
    {
        protected static RepositoryHandler<DocumentTemplate> _templatesHandler =
            new DocumentTemplatesRepositoryHandler();

        protected static HtmlDocumentHandler _documentConverter = 
            new HtmlDocumentHandler(AccountController.FullName);

        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DocumentTemplates()
        {
            var templates = _templatesHandler.GetAll(x => true);

            return View(templates);
        }

        public async Task<ActionResult> FillDocument(int id)
        {
            var template = await _templatesHandler.FindById(id);
            template = await _documentConverter.ConvertView(template);
            return View(template);
        }

        [HttpPost]
        public ActionResult FillDocument(DocumentTemplate template)
        {
            return View();
        }
    }
}