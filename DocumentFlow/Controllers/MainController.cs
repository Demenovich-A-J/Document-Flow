using System.Web.Mvc;
using BL.AbstractClasses;
using BL.DocumentTypeHandlers;
using EntityModels;
using BL.DocumentTemplatesHandlers;
using System.Threading.Tasks;
using BL.DocumentHandlers;


namespace DocumentFlow.Controllers
{
    public class MainController : Controller
    {
        protected static RepositoryHandler<DocumentTemplate> TemplatesHandler =
            new DocumentTemplatesRepositoryHandler();

        protected static HtmlDocumentHandler DocumentConverter = 
            new HtmlDocumentHandler(AccountController.FullName);

        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DocumentTemplates()
        {
            var templates = TemplatesHandler.GetAll(x => true);

            return View(templates);
        }

        public ActionResult FillDocument(DocumentTemplate template)
        {
            template = DocumentConverter.ConvertView(template);
            return View(template);
        }
    }
}