using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TiledSharp;

namespace TiledSharpSandbox
{
    public class Root : MessageHandlerBase<CyColor>
    {
        private TmxMap _map;
        private Manager<string, Sequence<Bitmap<CyColor>>> _bitmapSequenceManager = new Manager<string, Sequence<Bitmap<CyColor>>>(x=>null);
        private Dictionary<int, Bitmap<CyColor>> _bitmaps = new Dictionary<int, Bitmap<CyColor>>();
        private Dictionary<int, CyColor> _bitmapColorKeys = new Dictionary<int, CyColor>();
        private int _xOffset = 0;
        private int _yOffset = 0;

        public Root(IMessageHandler<CyColor> parent)
            : base(parent, true, null)
        {
            //TODO: eliminate hardcoded path
            _map = new TmxMap("E:/TheGrumpyGameDev/tombofwoefuldoom/Resources/Maps/home.tmx");

            foreach (var tileset in _map.Tilesets)
            {
                int gid = tileset.FirstGid;
                using (FileStream fileStream = new FileStream(tileset.Image.Source, FileMode.Open))
                {
                    using (var texture = Texture2D.FromStream(((Stage)parent).GraphicsDevice, fileStream))
                    {
                        Color[] colorBuffer = new Color[texture.Width * texture.Height];
                        texture.GetData(colorBuffer);
                        List<CyColor> cyColors = colorBuffer.Select(color =>
                        {
                            switch (color.R / 85)
                            {
                                case 0:
                                    return CyColor.Black;
                                case 1:
                                    return CyColor.DarkGray;
                                case 2:
                                    return CyColor.LightGray;
                                default:
                                    return CyColor.White;
                            }
                        }).ToList();

                        var sequence = new Sequence<Bitmap<CyColor>>();
                        int rows = texture.Height / tileset.TileHeight;
                        int columns = texture.Width / tileset.TileWidth;
                        for (int row = 0; row < rows; ++row)
                        {
                            for (int column = 0; column < columns; ++column)
                            {
                                if(tileset.Tiles.TryGetValue(column+row*columns, out var entry))
                                {
                                    if(entry.Properties.TryGetValue("ColorKeyed", out var isColorKeyed))
                                    {
                                        if(isColorKeyed.ToLower()=="true")
                                        {
                                            switch(entry.Properties["KeyColor"].ToLower())
                                            {
                                                case "0":
                                                    _bitmapColorKeys[gid] = CyColor.White;
                                                    break;
                                                case "1":
                                                    _bitmapColorKeys[gid] = CyColor.LightGray;
                                                    break;
                                                case "2":
                                                    _bitmapColorKeys[gid] = CyColor.DarkGray;
                                                    break;
                                                default:
                                                    _bitmapColorKeys[gid] = CyColor.Black;
                                                    break;
                                            }
                                        }
                                    }
                                }
                                Bitmap<CyColor> bitmap = new Bitmap<CyColor>(tileset.TileWidth, tileset.TileHeight);
                                for (int x = 0; x < tileset.TileWidth; ++x)
                                {
                                    for (int y = 0; y < tileset.TileHeight; ++y)
                                    {
                                        var cyColor = cyColors[x + y * texture.Width + column*tileset.TileWidth + row*texture.Width *tileset.TileHeight ];
                                        bitmap.Set(x, y, cyColor);
                                    }
                                }
                                sequence.Append(bitmap);
                                _bitmaps[gid] = bitmap;
                                ++gid;
                            }
                        }
                        _bitmapSequenceManager[tileset.Name] = sequence;
                    }
                }
            }

        }

        public static IMessageHandler<CyColor> Create(IMessageHandler<CyColor> parent)
        {
            return new Root(parent);
        }

        protected override bool OnCommand(Command command)
        {
            switch(command)
            {
                case Command.Up:
                    _yOffset++;
                    break;
                case Command.Down:
                    _yOffset--;
                    break;
                case Command.Left:
                    _xOffset++;
                    break;
                case Command.Right:
                    _xOffset--;
                    break;
            }
            return false;
        }

        protected override IResult OnMessage(IMessage message)
        {
            return null;
        }

        protected override void OnUpdate(IPixelWriter<CyColor> pixelWriter, CyRect? clipRect)
        {
            foreach (var layer in _map.Layers)
            {
                for (int column = 0; column < _map.Width; ++column)
                {
                    for (int row = 0; row < _map.Height; ++row)
                    {
                        var tile = layer.Tiles[column + row * _map.Width];

                        if (tile.Gid != 0)
                        {
                            if(_bitmapColorKeys.TryGetValue(tile.Gid, out CyColor colorKey))
                            {
                                _bitmaps[tile.Gid].Draw(pixelWriter, CyPoint.Create(column * _map.TileWidth + _xOffset, row * _map.TileHeight + _yOffset), x => x!=colorKey);
                            }
                            else
                            {
                                _bitmaps[tile.Gid].Draw(pixelWriter, CyPoint.Create(column * _map.TileWidth + _xOffset, row * _map.TileHeight + _yOffset), x => true);
                            }
                        }
                    }
                }
            }
        }
    }
}
