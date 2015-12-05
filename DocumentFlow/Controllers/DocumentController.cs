using System.Web.Mvc;
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

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult ConvertView(int id)
        {
            var template = _documentHandler.ConvertView(id);
            return View(template);
        }
    }
}