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

            //var bitmapSequences = await _context.BitmapSequences.Include("Bitmaps")
            //    .FirstOrDefaultAsync(m => m.BitmapSequenceId == id);
            //if (bitmapSequences == null)
            //{
            //    return NotFound();
            //}

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

    }
}
