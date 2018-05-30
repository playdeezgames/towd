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

namespace TombsPlayer
{
    public class TombsPlayer : EditorBase<GameState>
    {
        private CyFont _font;

        public TombsPlayer():base(2,GameState.Quit)
        {
        }

        protected override void OnInitialize()
        {
            Data.World = Utility.LoadEmbedded<World>(Assembly.GetExecutingAssembly(), "TombsPlayer.World.json");
        }

        protected override void OnLoadContent()
        {
            _font = Utility.LoadEmbedded<CyFont>(Assembly.GetExecutingAssembly(), "Tombs.Font4x6.json");
            _stateManager[GameState.MainMenu] = new MainMenuState(_stateManager, _colorBuffer, _font);
            _stateManager[GameState.ConfirmQuit] = new ConfirmQuitState(_stateManager, _colorBuffer, _font);
        }
    }
}
