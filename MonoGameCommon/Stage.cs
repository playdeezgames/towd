using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MonoGameCommon
{
    public class Stage : Game, IMessageHandler<CyColor>
    {
        private int _screenWidth;
        private int _screenHeight;
        private int _zoom;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _screenTexture;
        private KeyboardState _oldKeyboardState = new KeyboardState();
        private GamePadState _oldGamePadState = new GamePadState();
        private IMessageHandler<CyColor> _root;
        private ColorBuffer<CyColor> _colorBuffer;

        private int _backBufferWidth { get { return _screenWidth * _zoom; } }
        private int _backBufferHeight { get { return _screenHeight * _zoom; } }
        public IMessageHandler<CyColor> Parent => null;

        public int X { get => 0; set => throw new NotImplementedException(); }
        public int Y { get => 0; set => throw new NotImplementedException(); }
        public int Width { get => _screenWidth; set => throw new NotImplementedException(); }
        public int Height { get => _screenHeight; set => throw new NotImplementedException(); }

        public int GlobalX => X;

        public int GlobalY => Y;

        public bool Enabled { get => true; set => throw new NotImplementedException(); }

        public bool GlobalEnabled => Enabled;

        public int Right => X + Width;

        public int Bottom => Y + Height;

        public int GlobalRight => GlobalX + Width;

        public int GlobalBottom => GlobalY + Height;

        public CyRect GlobalBounds => CyRect.Create(X, Y, Width, Height);

        private Stage() { }

        public Stage(int screenWidth, int screenHeight, int zoom, Func<IMessageHandler<CyColor>, IMessageHandler<CyColor>> factory)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _zoom = zoom;
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = _backBufferWidth;
            _graphics.PreferredBackBufferHeight = _backBufferHeight;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            _root = factory(this);
            _colorBuffer = new ColorBuffer<CyColor>(_screenWidth, _screenHeight, CyColorExtensions.ToColor);
        }

        protected override void Initialize()
        {
            _screenTexture = new Texture2D(GraphicsDevice, _screenWidth, _screenHeight);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        private static readonly Dictionary<Command, Func<KeyboardState, bool>> keyProcessor = new Dictionary<Command, Func<KeyboardState, bool>>()
        {
            [Command.Back] = x => x.IsKeyDown(Keys.Back),
            [Command.Blue] = x => x.IsKeyDown(Keys.Enter),
            [Command.Down] = x => x.IsKeyDown(Keys.Down) || x.IsKeyDown(Keys.S),
            [Command.Green] = x => x.IsKeyDown(Keys.Space) || x.IsKeyDown(Keys.X),
            [Command.Left] = x => x.IsKeyDown(Keys.Left) || x.IsKeyDown(Keys.A),
            [Command.Next] = x => x.IsKeyDown(Keys.OemPeriod),
            [Command.Previous] = x => x.IsKeyDown(Keys.OemComma),
            [Command.Red] = x => x.IsKeyDown(Keys.Escape) || x.IsKeyDown(Keys.Z),
            [Command.Right] = x => x.IsKeyDown(Keys.Right) || x.IsKeyDown(Keys.D),
            [Command.Start] = x => x.IsKeyDown(Keys.F2),
            [Command.Up] = x => x.IsKeyDown(Keys.Up) || x.IsKeyDown(Keys.W),
            [Command.Yellow] = x => x.IsKeyDown(Keys.Tab)
        };

        private static readonly Dictionary<Command, Func<GamePadState, bool>> gamePadProcessor = new Dictionary<Command, Func<GamePadState, bool>>()
        {
            [Command.Back] = x => x.Buttons.Back == ButtonState.Pressed,
            [Command.Blue] = x => x.Buttons.X == ButtonState.Pressed,
            [Command.Down] = x => x.DPad.Down == ButtonState.Pressed,
            [Command.Green] = x => x.Buttons.A == ButtonState.Pressed,
            [Command.Left] = x => x.DPad.Left == ButtonState.Pressed,
            [Command.Next] = x => x.Buttons.RightShoulder == ButtonState.Pressed,
            [Command.Previous] = x => x.Buttons.LeftShoulder == ButtonState.Pressed,
            [Command.Red] = x => x.Buttons.B == ButtonState.Pressed,
            [Command.Right] = x => x.DPad.Right == ButtonState.Pressed,
            [Command.Start] = x => x.Buttons.Start == ButtonState.Pressed,
            [Command.Up] = x => x.DPad.Up == ButtonState.Pressed,
            [Command.Yellow] = x => x.Buttons.Y == ButtonState.Pressed
        };

        protected override void Update(GameTime gameTime)
        {
            _colorBuffer.Clear(CyColor.White);
            var newKeyboardState = Keyboard.GetState();
            HashSet<Command> commands = new HashSet<Command>();
            foreach (var entry in keyProcessor)
            {
                if (entry.Value(newKeyboardState) && !entry.Value(_oldKeyboardState))
                {
                    commands.Add(entry.Key);
                }
            }
            _oldKeyboardState = newKeyboardState;
            var newGamePadState = GamePad.GetState(PlayerIndex.One);
            System.Diagnostics.Debug.Print(newGamePadState.Buttons.Y.ToString());
            foreach (var entry in gamePadProcessor)
            {
                if (entry.Value(newGamePadState) && !entry.Value(_oldGamePadState))
                {
                    commands.Add(entry.Key);
                }
            }
            foreach (var command in commands)
            {
                HandleCommand(command);
            }
            _oldGamePadState = newGamePadState;
            base.Update(gameTime);
            Update(_colorBuffer);
            _colorBuffer.Apply(_screenTexture);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_screenTexture, new Rectangle(0, 0, _backBufferWidth, _backBufferHeight), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public IResult HandleMessage(IMessage message)
        {
            if (message.MessageId == QuitMessage.Id)
            {
                Exit();
                return AckResult<CyColor>.Create(message, this);
            }
            else
            {
                return null;
            }
        }

        public void Update(IPixelWriter<CyColor> pixelWriter)
        {
            _root.Update(pixelWriter);
        }

        public bool HandleCommand(Command command)
        {
            return _root.HandleCommand(command);
        }
    }
}
