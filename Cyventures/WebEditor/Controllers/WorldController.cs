using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEditor.Controllers
{
    public class WorldController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var worlds = db.Worlds.OrderBy(x => x.WorldName).ToList();
                return View(worlds);
            }
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View(new EFModel.World());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(EFModel.World model)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                db.Worlds.Add(model);
                db.SaveChanges();
                return RedirectToAction("Detail", new { id = model.WorldId });
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                return View(db.Worlds.Single(x=>x.WorldId==id));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EFModel.World model)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var world = db.Worlds.Single(x => x.WorldId == model.WorldId);
                world.WorldName = model.WorldName;
                db.SaveChanges();
                return RedirectToAction("Detail", new { id = model.WorldId });
            }
        }
    }
}