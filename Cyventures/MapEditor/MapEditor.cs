using Common;
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
    public class MapEditor : Game
    {
        const int ScreenWidth = 160;
        const int ScreenHeight = 100;
        const int Zoom = 6;
        const int BackBufferWidth = ScreenWidth * Zoom;
        const int BackBufferHeight = ScreenHeight * Zoom;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D screenTexture;
        KeyboardState oldKeyboardState = new KeyboardState();

        ColorBuffer<CyColor> colorBuffer;
        CyFont font;
        StateManager<EditorState, Command> stateManager;

        public MapEditor()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBufferHeight;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            screenTexture = new Texture2D(GraphicsDevice, ScreenWidth, ScreenHeight);
            Data.Map = new TileMap<int>(12, 12, 0);
            Data.TileSet = Utility.LoadEmbedded<CyBitmapSequence>(Assembly.GetExecutingAssembly(), "MapEditor.DungeonTiles.json");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            colorBuffer = new ColorBuffer<CyColor>(ScreenWidth, ScreenHeight, CyColorExtensions.ToColor);
            colorBuffer.Clear(CyColor.White);
            font = Utility.LoadEmbedded<CyFont>(Assembly.GetExecutingAssembly(), "MapEditor.Font5x7.json");
            stateManager = new StateManager<EditorState, Command>();
            stateManager[EditorState.MainMenu] = new MainMenuState(stateManager, colorBuffer, font);
            stateManager[EditorState.ConfirmQuit] = new ConfirmQuitState(stateManager, colorBuffer, font);
            stateManager[EditorState.Create] = new CreateState(stateManager, colorBuffer, font);
            stateManager[EditorState.SelectTile] = new SelectTileState(stateManager, colorBuffer, font);
            stateManager[EditorState.Edit] = new EditState(stateManager, colorBuffer, font);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var newKeyboardState = Keyboard.GetState();
            if(newKeyboardState.IsKeyDown(Keys.Up) && !oldKeyboardState.IsKeyDown(Keys.Up))
            {
                stateManager.DoCommand(Command.Up);
            }
            if (newKeyboardState.IsKeyDown(Keys.Down) && !oldKeyboardState.IsKeyDown(Keys.Down))
            {
                stateManager.DoCommand(Command.Down);
            }
            if (newKeyboardState.IsKeyDown(Keys.Left) && !oldKeyboardState.IsKeyDown(Keys.Left))
            {
                stateManager.DoCommand(Command.Left);
            }
            if (newKeyboardState.IsKeyDown(Keys.Right) && !oldKeyboardState.IsKeyDown(Keys.Right))
            {
                stateManager.DoCommand(Command.Right);
            }
            if (newKeyboardState.IsKeyDown(Keys.Escape) && !oldKeyboardState.IsKeyDown(Keys.Escape))
            {
                stateManager.DoCommand(Command.Esc);
            }
            if (newKeyboardState.IsKeyDown(Keys.Enter) && !oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                stateManager.DoCommand(Command.Enter);
            }
            if (newKeyboardState.IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space))
            {
                stateManager.DoCommand(Command.Select);
            }
            if (newKeyboardState.IsKeyDown(Keys.Tab) && !oldKeyboardState.IsKeyDown(Keys.Tab))
            {
                stateManager.DoCommand(Command.Tab);
            }
            if (newKeyboardState.IsKeyDown(Keys.OemPeriod) && !oldKeyboardState.IsKeyDown(Keys.OemPeriod))
            {
                stateManager.DoCommand(Command.Next);
            }
            if (newKeyboardState.IsKeyDown(Keys.OemComma) && !oldKeyboardState.IsKeyDown(Keys.OemComma))
            {
                stateManager.DoCommand(Command.Previous);
            }

            if (stateManager.Current == EditorState.Quit)
            {
                Exit();
                return;
            }

            oldKeyboardState = newKeyboardState;
            stateManager.Update(gameTime.ElapsedGameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            colorBuffer.Apply(screenTexture);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(screenTexture, new Rectangle(0, 0, BackBufferWidth, BackBufferHeight), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
