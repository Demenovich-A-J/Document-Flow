using System.Web.Mvc;
using AutoMapper;
using BL.Handlers.DocumentHandlers;
using DocumentFlow.Models;
using DocumentFlow.Models.AuthorizeAttribute;
using DocumentType = BL.Models.DocumentType;
using Position = BL.Models.Position;
using Role = BL.Models.Role;
using User = BL.Models.User;

namespace DocumentFlow.Controllers
{
    public class DocumentController : Controller
    {
        public static Document ConvertToModel(BL.Models.Document document)
        {
            Mapper.CreateMap<BL.Models.Document, Document>();
            Mapper.CreateMap<User, Models.User>();
            Mapper.CreateMap<DocumentType, Models.DocumentType>();
            Mapper.CreateMap<Position, Models.Position>();
            Mapper.CreateMap<Role, Models.Role>();

            return Mapper.Map<BL.Models.Document, Document>(document);
        }

        public static BL.Models.Document ConvertToBlModel(Document document)
        {
            Mapper.CreateMap<Document, BL.Models.Document>();
            Mapper.CreateMap<Models.User, User>();
            Mapper.CreateMap<Models.DocumentType, DocumentType>();
            Mapper.CreateMap<Models.Position, Position>();
            Mapper.CreateMap<Models.Role, Role>();

            return Mapper.Map<Document, BL.Models.Document>(document);
        }

        [HttpPost]
        [ValidateInput(false)]
        [CustomAuthorize("Admin")]
        public ActionResult ConvertView(DocumentTemplate template)
        {
            var userName = new MainController().GetUserName();

            var newTemplate =
                new HtmlDocumentHandler(userName)
                    .ConvertView(DocumentTemplateController.ConvertToBlModel(template));

            return View(DocumentTemplateController.ConvertToModel(newTemplate));
        }
    }
}