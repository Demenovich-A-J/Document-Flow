using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BL.Handlers.PositionsHandlers;
using BL.Interfaces;
using BL.Models;
using DocumentFlow.Models.AuthorizeAttribute;

namespace DocumentFlow.Controllers
{
    public class PositionController : Controller
    {
        private readonly IRepositoryHandler<Position> _positionsHandler;

        public PositionController()
        {
            _positionsHandler = new PositionsRepositoryHandler();
        }

        public static Models.Position ConvertToModel(Position position)
        {
            Mapper.CreateMap<Position, Models.Position>();

            return Mapper.Map<Position, Models.Position>(position);
        }

        private static Position ConvertToBlModel(Models.Position position)
        {
            Mapper.CreateMap<Models.Position, Position>();

            return Mapper.Map<Models.Position, Position>(position);
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult Positions()
        {
            return View(_positionsHandler.GetAll().Select(ConvertToModel));
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult CreatePosition()
        {
            return View();
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult CreatePosition(Models.Position position)
        {
            if (ModelState.IsValid)
            {
                _positionsHandler.Add(ConvertToBlModel(position));
            }

            return RedirectToAction("Positions");
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult EditPosition(int id)
        {
            var position = _positionsHandler.FindById(id);
            return View(ConvertToModel(position));
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult EditPosition(Models.Position position)
        {
            _positionsHandler.Update(ConvertToBlModel(position));
            return RedirectToAction("Positions");
        }

        [HttpGet]
        [CustomAuthorize("Admin")]
        public ActionResult DeletePosition(int id)
        {
            var position = _positionsHandler.FindById(id);
            if (position != null)
            {
                _positionsHandler.Remove(position);
            }
            return RedirectToAction("Positions");
        }
    }
}