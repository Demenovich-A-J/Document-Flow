using System.Threading.Tasks;
using System.Web.Mvc;
using BL.DocumentHandlers;
using EntityModels;

namespace DocumentFlow.Controllers
{
    public class DocumentController : Controller
    {
        protected HtmlDocumentHandler DocumentHandler;

        public DocumentController()
        {
            DocumentHandler = new HtmlDocumentHandler(AccountController.FullName);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ConvertView(DocumentTemplate template)
        {
            template = await DocumentHandler.ConvertView(template);
            return View(template);
        }
    }
}