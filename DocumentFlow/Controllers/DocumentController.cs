using BL.DocumentFandler;
using DocumentFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

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