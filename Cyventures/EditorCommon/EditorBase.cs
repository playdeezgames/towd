using Common;
using EditorCommon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EditorCommon
{
    public abstract class EditorBase<T> : Game
    {
        const int ScreenWidth = 160;
        const int ScreenHeight = 100;
        private int _zoom=1;
        private int BackBufferWidth { get { return ScreenWidth * _zoom; } }
        private int BackBufferHeight { get { return ScreenHeight * _zoom; } }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D screenTexture;
        KeyboardState oldKeyboardState = new KeyboardState();

        protected ColorBuffer<CyColor> _colorBuffer;
        protected StateManager<T, Command> _stateManager;
        T _finalState;

        private EditorBase() { }

        public EditorBase(int zoom, T finalState)
        {
            _zoom = zoom;
            _finalState = finalState;
            _stateManager = new StateManager<T, Command>();
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBufferHeight;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected abstract void OnInitialize();

        protected override void Initialize()
        {
            screenTexture = new Texture2D(GraphicsDevice, ScreenWidth, ScreenHeight);
            OnInitialize();
            base.Initialize();
        }
        protected abstract void OnLoadContent();
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _colorBuffer = new ColorBuffer<CyColor>(ScreenWidth, ScreenHeight, CyColorExtensions.ToColor);
            _colorBuffer.Clear(CyColor.White);
            OnLoadContent();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var newKeyboardState = Keyboard.GetState();
            if(newKeyboardState.IsKeyDown(Keys.Up) && !oldKeyboardState.IsKeyDown(Keys.Up))
            {
                _stateManager.DoCommand(Command.Up);
            }
            if (newKeyboardState.IsKeyDown(Keys.Down) && !oldKeyboardState.IsKeyDown(Keys.Down))
            {
                _stateManager.DoCommand(Command.Down);
            }
            if (newKeyboardState.IsKeyDown(Keys.Left) && !oldKeyboardState.IsKeyDown(Keys.Left))
            {
                _stateManager.DoCommand(Command.Left);
            }
            if (newKeyboardState.IsKeyDown(Keys.Right) && !oldKeyboardState.IsKeyDown(Keys.Right))
            {
                _stateManager.DoCommand(Command.Right);
            }
            if (newKeyboardState.IsKeyDown(Keys.Escape) && !oldKeyboardState.IsKeyDown(Keys.Escape))
            {
                _stateManager.DoCommand(Command.Esc);
            }
            if (newKeyboardState.IsKeyDown(Keys.Enter) && !oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                _stateManager.DoCommand(Command.Enter);
            }
            if (newKeyboardState.IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space))
            {
                _stateManager.DoCommand(Command.Select);
            }
            if (newKeyboardState.IsKeyDown(Keys.Tab) && !oldKeyboardState.IsKeyDown(Keys.Tab))
            {
                _stateManager.DoCommand(Command.Tab);
            }
            if (newKeyboardState.IsKeyDown(Keys.OemPeriod) && !oldKeyboardState.IsKeyDown(Keys.OemPeriod))
            {
                _stateManager.DoCommand(Command.Next);
            }
            if (newKeyboardState.IsKeyDown(Keys.OemComma) && !oldKeyboardState.IsKeyDown(Keys.OemComma))
            {
                _stateManager.DoCommand(Command.Previous);
            }

            if (_stateManager.Current.Equals(_finalState))
            {
                Exit();
                return;
            }

            oldKeyboardState = newKeyboardState;
            _stateManager.Update(gameTime.ElapsedGameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _colorBuffer.Apply(screenTexture);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(screenTexture, new Rectangle(0, 0, BackBufferWidth, BackBufferHeight), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
