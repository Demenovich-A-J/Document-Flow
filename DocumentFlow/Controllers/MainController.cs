using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFlow.Models;
using BL.AbstractClasses;
using BL.DocumentTypeHandlers;

namespace DocumentFlow.Controllers
{
    public class MainController : Controller
    {
        protected static RepositoryHandler<EntityModels.DocumentType> _documentTypesHandler =
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