using System.Collections.Generic;
using System.Linq;
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
            var res = TemplatesHandler.GetAll(x => true).FirstOrDefault(x => x.Id == template.Id);

            res = await DocumentConverter.ConvertView(res);
            return View(res);
        }


        [HttpPost]
        public void GetResult(string json)
        {
            var pairs = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string,string>>(json);
        }

    }
}