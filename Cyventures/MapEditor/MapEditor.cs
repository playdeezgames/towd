using Common;
using EditorCommon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MapEditor
{
    public class MapEditor:EditorBase<EditorState>
    {
        private CyFontOld _font;

        public MapEditor() : base(2, EditorState.Quit ) { }

        protected override void OnInitialize()
        {
            Data.Map = new TileMap<int>(12, 12, 0);
            Data.TileSet = Utility.LoadEmbedded<CyBitmapSequenceOld>(Assembly.GetExecutingAssembly(), "MapEditor.DungeonTiles.json");
        }

        protected override void OnLoadContent()
        {
            _font = Utility.LoadEmbedded<CyFontOld>(Assembly.GetExecutingAssembly(), "MapEditor.Font5x7.json");
            _stateManager[EditorState.MainMenu] = new MainMenuState(_stateManager, _colorBuffer, _font);
            _stateManager[EditorState.ConfirmQuit] = new ConfirmQuitState(_stateManager, _colorBuffer, _font);
            _stateManager[EditorState.Create] = new CreateState(_stateManager, _colorBuffer, _font);
            _stateManager[EditorState.SelectTile] = new SelectTileState(_stateManager, _colorBuffer, _font);
            _stateManager[EditorState.Edit] = new EditState(_stateManager, _colorBuffer, _font);
        }
    }
}
