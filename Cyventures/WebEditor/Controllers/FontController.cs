using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEditor.Controllers
{
    public class FontController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new EFModel.TOWDEntities())
            {
                return View(db.Fonts.OrderBy(x=>x.FontName).ToList());
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                return View(db.Fonts.Single(x=>x.FontId==id));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EFModel.Font model)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var font = db.Fonts.Single(x => x.FontId == model.FontId);
                font.FontName = model.FontName;
                font.FontHeight = model.FontHeight;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Detail(int id)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var font = db.Fonts.Include("Glyphs").Single(x => x.FontId == id);
                return View(font);
            }
        }
        [HttpGet]
        public ActionResult EditGlyph(int id)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var glyph = db.Glyphs.Include("Font").Single(x => x.GlyphId == id);

                return View(glyph);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGlyph(EFModel.Glyph model)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var glyph = db.Glyphs.Include("Font").Single(x => x.GlyphId == model.GlyphId);
                glyph.GlyphWidth = model.GlyphWidth;
                db.SaveChanges();
                return RedirectToAction("Detail", new { id = glyph.FontId });
            }
        }
        public ActionResult EditGlyphImage(int id)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var glyph = db.Glyphs.Include("Font").Include("GlyphPixels").Single(x => x.GlyphId == id);

                return View(glyph);
            }
        }
        public FileResult GlyphImage(int id, int scale=1)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var glyph = db.Glyphs.Include("Font").Include("GlyphPixels").Single(x => x.GlyphId == id);

                Image img = new Bitmap(scale * glyph.GlyphWidth, scale*glyph.Font.FontHeight);
                Graphics g = Graphics.FromImage(img);
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, scale * glyph.GlyphWidth, scale * glyph.Font.FontHeight));
                var brush = new SolidBrush(Color.Black);

                foreach(var glyphPixel in glyph.GlyphPixels)
                {
                    g.FillRectangle(brush, new Rectangle(glyphPixel.X * scale, glyphPixel.Y * scale, scale, scale));
                }

                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Png);

                ms.Position = 0;

                return new FileStreamResult(ms, "image/png");
            }
        }
        public ActionResult RemoveGlyphPixel(int id)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var glyphPixel = db.GlyphPixels.Single(x => x.GlyphPixelId == id);
                var glyphId = glyphPixel.GlyphId;
                db.GlyphPixels.Remove(glyphPixel);
                db.SaveChanges();
                return RedirectToAction("EditGlyphImage", new { id = glyphId });
            }
        }
        public ActionResult AddGlyphPixel(int glyphid, int x, int y)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var glyphPixel = new EFModel.GlyphPixel()
                {
                    GlyphId = glyphid,
                    X = x,
                    Y = y
                };
                db.GlyphPixels.Add(glyphPixel);
                db.SaveChanges();
                return RedirectToAction("EditGlyphImage", new { id = glyphid });
            }
        }
    }
}