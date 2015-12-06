using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.AbstractRepository;
using DAL.Repositories;
using EntityModels;

namespace BL.DocumentTypeHandlers
{
    public class DocumentTypesHandler
    {
        protected DataRepository<DocumentType> _typesRepository;

        public DocumentTypesHandler()
        {
            _typesRepository = new DocumentTypesRepository();
            ;
        }

        public List<SelectListItem> TypesSelectList()
        {
            var types = _typesRepository.GetAll(x => true);

            return types != null
                ? new List<SelectListItem>
                    (types.Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()}))
                : new List<SelectListItem>();
        }
    }
}