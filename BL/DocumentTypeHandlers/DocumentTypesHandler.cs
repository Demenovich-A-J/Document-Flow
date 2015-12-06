using DAL.AbstractRepository;
using DAL.Repositories;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BL.DocumentTypeHandlers
{
    public class DocumentTypesHandler
    {
        protected DataRepository<DocumentType> _typesRepository;

        public DocumentTypesHandler()
        {
            _typesRepository = new DocumentTypesRepository(); ;
        }

        public List<SelectListItem> TypesSelectList()
        {
            IEnumerable<DocumentType> types = _typesRepository.GetAll(x => true);

            return types != null ?
                new List<SelectListItem>
                    (types.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() })) :
                new List<SelectListItem>();
        }
    }
}
