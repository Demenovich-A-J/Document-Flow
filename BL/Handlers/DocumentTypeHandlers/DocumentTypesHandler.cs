using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;

namespace BL.Handlers.DocumentTypeHandlers
{
    public class DocumentTypesHandler
    {
        protected IDataRepository<DocumentType> _typesRepository;

        public DocumentTypesHandler()
        {
            _typesRepository = new DocumentTypesRepository();
        }

        public List<SelectListItem> TypesSelectList()
        {
            var types = _typesRepository.GetAll();

            return types != null
                ? new List<SelectListItem>
                    (types.Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()}))
                : new List<SelectListItem>();
        }
    }
}