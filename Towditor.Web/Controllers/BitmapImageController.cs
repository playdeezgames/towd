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
    public class BitmapImageController : Controller
    {
        private readonly TOWDContext _context;

        public BitmapImageController(TOWDContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Render(int? id, int? scale)
        {
            if (id == null)
            {
                return NotFound();
            }
            scale = scale ?? 1;

            var bitmap = await _context.Bitmaps.Include("BitmapSequence").Include("BitmapPixels").SingleAsync(x => x.BitmapId == id);

            Image img = new Bitmap(scale.Value * bitmap.BitmapWidth, scale.Value * bitmap.BitmapHeight);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, scale.Value * bitmap.BitmapWidth, scale.Value * bitmap.BitmapHeight));
            var brushes = new Brush[]
            {
                    new SolidBrush(Color.White),
                    new SolidBrush(Color.FromArgb(170,170,170)),
                    new SolidBrush(Color.FromArgb(85,85,85)),
                    new SolidBrush(Color.Black)
            };

            foreach (var bitmapPixel in bitmap.BitmapPixels)
            {
                g.FillRectangle(brushes[bitmapPixel.ColorId], new Rectangle(bitmapPixel.X * scale.Value, bitmapPixel.Y * scale.Value, scale.Value, scale.Value));
            }

            //TODO: dispose of things!
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Png);

            ms.Position = 0;

            return new FileStreamResult(ms, "image/png");

        }

        public async Task<IActionResult> Edit(int id, int color = 0)
        {
            var bitmap = await _context.Bitmaps.Include("BitmapSequence").Include("BitmapPixels").SingleAsync(x => x.BitmapId == id);

            return View(new Models.MetaModel<int, EFModel.Bitmaps>
            {
                Meta = color,
                Payload = bitmap
            });
        }
        public async Task<ActionResult> SetBitmapPixel(int id, int x, int y, int color)
        {
                var bitmapPixel = await _context.BitmapPixels.Where(bp => bp.BitmapId == id && bp.X == x && bp.Y == y).SingleOrDefaultAsync();
                if (bitmapPixel != null)
                {
                    bitmapPixel.ColorId = color;
                }
                else
                {
                    bitmapPixel = new EFModel.BitmapPixels()
                    {
                        BitmapId = id,
                        X = x,
                        Y = y,
                        ColorId = color
                    };
                    _context.BitmapPixels.Add(bitmapPixel);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", new { id, color });
        }
    }
}
