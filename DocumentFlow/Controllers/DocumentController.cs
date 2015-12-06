using System.Web.Mvc;
using EntityModels;
using BL.DocumentFandler;

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
        public ActionResult ConvertView(DocumentTemplate template)
        {
            template = _documentHandler.ConvertView(template);
            return View(template);
        }
    }
}