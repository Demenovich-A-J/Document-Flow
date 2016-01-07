using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;

namespace BL.Handlers.PositionsHandlers
{
    public class PositionsHandler
    {
        protected IDataRepository<Position> _positionsRepository;

        public PositionsHandler()
        {
            _positionsRepository = new PositionsRepository();
        }

        public List<SelectListItem> PositionsSelectList()
        {
            var positions = _positionsRepository.GetAll();

            return positions != null
                ? new List<SelectListItem>
                    (positions.Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()}))
                : new List<SelectListItem>();
        }
    }
}