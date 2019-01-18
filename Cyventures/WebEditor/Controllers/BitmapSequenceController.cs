using Common;
using Newtonsoft.Json;
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
    public class BitmapSequenceController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new EFModel.TOWDEntities())
            {
                return View(db.BitmapSequences.Include("Bitmaps").OrderBy(x=>x.BitmapSequenceName).ToList());
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                return View(db.BitmapSequences.Single(x=>x.BitmapSequenceId==id));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EFModel.BitmapSequence model)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var bitmapSequence = db.BitmapSequences.Single(x => x.BitmapSequenceId == model.BitmapSequenceId);
                bitmapSequence.BitmapSequenceName = model.BitmapSequenceName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Detail(int id)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var bitmapSequence = db.BitmapSequences.Include("Bitmaps").Single(x => x.BitmapSequenceId == id);
                return View(bitmapSequence);
            }
        }
        public FileResult BitmapImage(int id, int scale = 1)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var bitmap = db.Bitmaps.Include("BitmapSequence").Include("BitmapPixels").Single(x => x.BitmapId == id);

                Image img = new Bitmap(scale * bitmap.BitmapWidth, scale * bitmap.BitmapHeight);
                Graphics g = Graphics.FromImage(img);
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, scale * bitmap.BitmapWidth, scale * bitmap.BitmapHeight));
                var brushes = new Brush[] 
                {
                    new SolidBrush(Color.White),
                    new SolidBrush(Color.FromArgb(170,170,170)),
                    new SolidBrush(Color.FromArgb(85,85,85)),
                    new SolidBrush(Color.Black)
                };

                foreach (var bitmapPixel in bitmap.BitmapPixels)
                {
                    g.FillRectangle(brushes[bitmapPixel.ColorId], new Rectangle(bitmapPixel.X * scale, bitmapPixel.Y * scale, scale, scale));
                }

                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Png);

                ms.Position = 0;

                return new FileStreamResult(ms, "image/png");
            }
        }


        [HttpGet]
        public ActionResult EditBitmap(int id)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var bitmap = db.Bitmaps.Include("BitmapSequence").Single(x => x.BitmapId == id);

                return View(bitmap);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBitmap(EFModel.Bitmap model)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var bitmap = db.Bitmaps.Include("BitmapSequence").Single(x => x.BitmapId == model.BitmapId);
                bitmap.BitmapWidth = model.BitmapWidth;
                bitmap.BitmapHeight = model.BitmapHeight;
                db.SaveChanges();
                return RedirectToAction("Detail", new { id = bitmap.BitmapSequenceId });
            }
        }
        public ActionResult EditBitmapImage(int id, int color=0)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var bitmap = db.Bitmaps.Include("BitmapSequence").Include("BitmapPixels").Single(x => x.BitmapId == id);

                return View(new Models.MetaModel<int, EFModel.Bitmap>
                {
                    Meta=color,
                    Payload=bitmap
                });
            }
        }
        public ActionResult SetBitmapPixel(int id, int x, int y, int color)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var bitmapPixel = db.BitmapPixels.Where(bp => bp.BitmapId == id && bp.X == x && bp.Y == y).SingleOrDefault();
                if(bitmapPixel!=null)
                {
                    bitmapPixel.ColorId = color;
                }
                else
                {
                    bitmapPixel = new EFModel.BitmapPixel()
                    {
                        BitmapId=id,
                        X=x,
                        Y=y,
                        ColorId=color
                    };
                    db.BitmapPixels.Add(bitmapPixel);
                }
                db.SaveChanges();
                return RedirectToAction("EditBitmapImage", new { id, color });
            }
        }
        public ActionResult Export(int id)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var bitmapSequence = db.BitmapSequences.Include("Bitmaps.BitmapPixels").Single(x => x.BitmapSequenceId == id);
                Sequence<Bitmap<CyColor>> export = new Sequence<Bitmap<CyColor>>();
                foreach(var bitmap in bitmapSequence.Bitmaps.OrderBy(x=>x.BitmapIndex))
                {
                    Bitmap<CyColor> exportBitmap = new Bitmap<CyColor>(bitmap.BitmapWidth, bitmap.BitmapHeight);
                    foreach(var bitmapPixel in bitmap.BitmapPixels)
                    {
                        exportBitmap.Set(bitmapPixel.X, bitmapPixel.Y, (CyColor)bitmapPixel.ColorId);
                    }
                    export.Items.Add(exportBitmap);
                }
                MemoryStream ms = new MemoryStream();
                var writer = new StreamWriter(ms);
                writer.Write(JsonConvert.SerializeObject(export));
                writer.Flush();
                ms.Position = 0;
                return new FileStreamResult(ms, "application/json");
            }
        }
        [HttpGet]
        public ActionResult AppendBitmap(int id)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                var bitmap = new EFModel.Bitmap()
                {
                    BitmapSequence = db.BitmapSequences.SingleOrDefault(x => x.BitmapSequenceId == id),
                    BitmapSequenceId = id,
                    BitmapIndex = db.Bitmaps.Where(x => x.BitmapSequenceId == id).Max(x => x.BitmapIndex) + 1,
                    BitmapHeight = 1,
                    BitmapWidth = 1
                };
                return View(bitmap);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppendBitmap(EFModel.Bitmap model)
        {
            using (var db = new EFModel.TOWDEntities())
            {
                db.Bitmaps.Add(model);
                db.SaveChanges();
                return RedirectToAction("Detail", new { id = model.BitmapSequenceId });
            }
        }

    }
}