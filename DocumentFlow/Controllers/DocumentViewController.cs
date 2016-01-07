using System.Web.Mvc;
using AutoMapper;
using DocumentFlow.Models;

namespace DocumentFlow.Controllers
{
    public class DocumentViewController : Controller
    {
        public static DocumentView ConvertToModel(BL.Models.DocumentView view)
        {
            Mapper.CreateMap<BL.Models.DocumentView, DocumentView>();
            return Mapper.Map<BL.Models.DocumentView, DocumentView>(view);
        }

        public static BL.Models.DocumentView ConvertToBlModel(DocumentView view)
        {
            Mapper.CreateMap<DocumentView, BL.Models.DocumentView>();
            return Mapper.Map<DocumentView, BL.Models.DocumentView>(view);
        }
    }
}