using System.Threading.Tasks;
using System.Web.Mvc;
using BL.AbstractClasses;
using BL.DocumentHandlers;
using BL.DocumentTemplatesHandlers;
using EntityModels;

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

        public async Task<ActionResult> FillDocument(DocumentTemplate template)
        {
            template = await DocumentConverter.ConvertView(template);
            return View(template);
        }
    }
}