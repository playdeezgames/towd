using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Towditor.Web.EFModel;

namespace Towditor.Web.Controllers
{
    [Authorize]
    public class GlyphImageController : Controller
    {
        private readonly TOWDContext _context;

        public GlyphImageController(TOWDContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Render(int? id, int? scale)
        {
            var glyph = await _context.Glyphs.Include("Font").Include("GlyphPixels").SingleAsync(x => x.GlyphId == id);
            scale = scale ?? 1;

            Image img = new Bitmap(scale.Value * glyph.GlyphWidth, scale.Value * glyph.Font.FontHeight);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, scale.Value * glyph.GlyphWidth, scale.Value * glyph.Font.FontHeight));
            var brush = new SolidBrush(Color.Black);

            foreach (var glyphPixel in glyph.GlyphPixels)
            {
                g.FillRectangle(brush, new Rectangle(glyphPixel.X * scale.Value, glyphPixel.Y * scale.Value, scale.Value, scale.Value));
            }

            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Png);

            ms.Position = 0;

            return new FileStreamResult(ms, "image/png");
        }

        public async Task<IActionResult> Edit(int id, int color = 0)
        {
            var glyph = await _context.Glyphs.Include("Font").Include("GlyphPixels").SingleAsync(x => x.GlyphId == id);

            return View(glyph);
        }
        public ActionResult RemoveGlyphPixel(int id)
        {
            var glyphPixel = _context.GlyphPixels.Single(x => x.GlyphPixelId == id);
            var glyphId = glyphPixel.GlyphId;
            _context.GlyphPixels.Remove(glyphPixel);
            _context.SaveChanges();
            return RedirectToAction("Edit", new { id = glyphId });
        }
        public ActionResult AddGlyphPixel(int glyphid, int x, int y)
        {
            var glyphPixel = new EFModel.GlyphPixels()
            {
                GlyphId = glyphid,
                X = x,
                Y = y
            };
            _context.GlyphPixels.Add(glyphPixel);
            _context.SaveChanges();
            return RedirectToAction("Edit", new { id = glyphid });
        }
    }
}
