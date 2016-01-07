using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BL.Handlers.DocumentTypeHandlers;
using BL.Interfaces;
using BL.Models;
using DocumentFlow.Models.AuthorizeAttribute;

namespace DocumentFlow.Controllers
{
    public class DocumentTypeController : Controller
    {
        private readonly IRepositoryHandler<DocumentType> _typesHandler;

        public DocumentTypeController()
        {
            _typesHandler = new DocumentTypesRepositoryHandler();
        }

        public static Models.DocumentType ConvertToModel(DocumentType type)
        {
            Mapper.CreateMap<DocumentType, Models.DocumentType>();

            return Mapper.Map<DocumentType, Models.DocumentType>(type);
        }

        private static DocumentType ConvertToBlModel(Models.DocumentType type)
        {
            Mapper.CreateMap<Models.DocumentType, DocumentType>();

            return Mapper.Map<Models.DocumentType, DocumentType>(type);
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult DocumentTypes()
        {
            return View(_typesHandler.GetAll().Select(ConvertToModel));
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult CreateDocumentType()
        {
            return View();
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult EditDocumentType(int id)
        {
            var type = _typesHandler.FindById(id);
            return View(ConvertToModel(type));
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult EditDocumentType(Models.DocumentType documentType)
        {
            _typesHandler.Update(ConvertToBlModel(documentType));
            return RedirectToAction("DocumentTypes");
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult CreateDocumentType(Models.DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                _typesHandler.Add(ConvertToBlModel(documentType));
                return RedirectToAction("DocumentTypes");
            }
            return View(documentType);
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult RemoveDocumentType(int id)
        {
            var type = _typesHandler.FindById(id);
            if (type != null)
            {
                _typesHandler.Remove(type);
            }
            return RedirectToAction("DocumentTypes");
        }
    }
}