using System.Web.Mvc;
using EntityModels;
using BL.DocumentFandler;
using System.Threading.Tasks;

namespace DocumentFlow.Controllers
{
    public class DocumentController : Controller
    {
        protected HtmlDocumentHandler _documentHandler;

        public DocumentController()
        {
            _documentHandler = new HtmlDocumentHandler(AccountController.FullName);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ConvertView(DocumentTemplate template)
        {
            template = await _documentHandler.ConvertView(template);
            return View(template);
        }
    }
}