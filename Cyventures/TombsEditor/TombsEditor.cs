using Common;
using EditorCommon;
using EngineCommon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TombsCommon;

namespace TombsEditor
{
    public class TombsEditor : EditorBase<EditorState>
    {
        private CyFont _font;

        public TombsEditor():base(2,EditorState.Quit)
        {
        }

        protected override void OnInitialize()
        {
            Data.World = new World();
        }

        protected override void OnLoadContent()
        {
            _font = Utility.LoadEmbedded<CyFont>(Assembly.GetExecutingAssembly(), "TombsEditor.Font4x6.json");
            _stateManager[EditorState.MainMenu] = new MainMenuState(_stateManager, _colorBuffer, _font);
            _stateManager[EditorState.ConfirmQuit] = new ConfirmQuitState(_stateManager, _colorBuffer, _font);
            _stateManager[EditorState.EditMenu] = new EditMenuState(_stateManager, _colorBuffer, _font);
            _stateManager[EditorState.GeneratorsMenu] = new GeneratorsMenuState(_stateManager, _colorBuffer, _font);
            _stateManager[EditorState.GeneratorMenu] = new GeneratorMenuState(_stateManager, _colorBuffer, _font);
        }
    }
}
