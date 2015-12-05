using DAL.AbstractRepository;
using DAL.Repositories;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BL.PositionsHandler
{
    public class PositionsHandler
    {
        protected DataRepository<Position> _positionsRepository;

        public PositionsHandler()
        {
            _positionsRepository = new PositionsRepository();
        }

        public List<SelectListItem> PositionsSelectList()
        {
            IEnumerable<Position> positions = _positionsRepository.GetAll(x => true);

            return positions != null ?
                new List<SelectListItem>
                    (positions.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() })) :
                new List<SelectListItem>();
        }
    }
}
