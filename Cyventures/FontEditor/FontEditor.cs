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

namespace FontEditor
{
    public class FontEditor : EditorBase<EditorState>
    {
        private CyFont _font;

        public FontEditor():base(2,EditorState.Quit)
        {
        }

        protected override void OnInitialize()
        {
            Data.Font = Utility.LoadEmbedded<CyFont>(Assembly.GetExecutingAssembly(), "FontEditor.Font3x5.json");
        }

        protected override void OnLoadContent()
        {
            _font = Utility.LoadEmbedded<CyFont>(Assembly.GetExecutingAssembly(), "FontEditor.Font5x7.json");
            _stateManager[EditorState.MainMenu] = new MainMenuState(_stateManager, _colorBuffer, _font);
            _stateManager[EditorState.ConfirmQuit] = new ConfirmQuitState(_stateManager, _colorBuffer, _font);
            _stateManager[EditorState.Create] = new CreateState(_stateManager, _colorBuffer, _font);
            _stateManager[EditorState.Browse] = new BrowseState(_stateManager, _colorBuffer, _font);
            _stateManager[EditorState.Edit] = new EditState(_stateManager, _colorBuffer, _font);
        }
    }
}
