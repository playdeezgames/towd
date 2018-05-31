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
    public class CommonGame : Game, IMessageHandler
    {
        private int _screenWidth;
        private int _screenHeight;
        private int _zoom;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _screenTexture;
        private KeyboardState _oldKeyboardState = new KeyboardState();
        private GamePadState _oldGamePadState = new GamePadState();
        private IMessageHandler _root;
        private ColorBuffer<CyColor> _colorBuffer;

        private int _backBufferWidth { get { return _screenWidth * _zoom; } }
        private int _backBufferHeight { get { return _screenHeight * _zoom; } }
        public IMessageHandler Parent => null;


        private CommonGame() { }

        public CommonGame(int screenWidth, int screenHeight, int zoom, Func<IMessageHandler,IMessageHandler> factory)
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
            [Command.Down] = x => x.IsKeyDown(Keys.Down),
            [Command.Green] = x => x.IsKeyDown(Keys.Space),
            [Command.Left] = x => x.IsKeyDown(Keys.Left),
            [Command.Next] = x => x.IsKeyDown(Keys.OemPeriod),
            [Command.Previous] = x => x.IsKeyDown(Keys.OemComma),
            [Command.Red] = x => x.IsKeyDown(Keys.Escape),
            [Command.Right] = x => x.IsKeyDown(Keys.Right),
            [Command.Start] = x => x.IsKeyDown(Keys.F2),
            [Command.Up] = x => x.IsKeyDown(Keys.Up),
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
            [Command.Yellow] = x => x.Buttons.Y == ButtonState.Pressed,
        };

        protected override void Update(GameTime gameTime)
        {
            _colorBuffer.Clear(CyColor.White);
            var newKeyboardState = Keyboard.GetState();
            HashSet<Command> commands = new HashSet<Command>();
            foreach(var entry in keyProcessor)
            {
                if(entry.Value(newKeyboardState) && !entry.Value(_oldKeyboardState))
                {
                    commands.Add(entry.Key);
                }
            }
            _oldKeyboardState = newKeyboardState;
            var newGamePadState = GamePad.GetState(PlayerIndex.One);
            foreach (var entry in gamePadProcessor)
            {
                if (entry.Value(newGamePadState) && !entry.Value(_oldGamePadState))
                {
                    commands.Add(entry.Key);
                }
            }
            foreach(var command in commands)
            {
                _root.HandleBroadcast(new CommandMessage(command), true);
            }
            _oldGamePadState = newGamePadState;
            base.Update(gameTime);
            _colorBuffer.Offset = CyPoint.Create(0, 0);
            _colorBuffer.ClipRect = null;
            _root.Broadcast(new DrawMessage<CyColor>(_colorBuffer));
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
            if(message.MessageId == QuitMessage.Id)
            {
                Exit();
                return new AckResult(message, this);
            }
            else
            {
                return null;
            }
        }

        public void Broadcast(IMessage message, bool reverseOrder = false)
        {
            _root.Broadcast(message, reverseOrder);
        }

        public IResult HandleBroadcast(IMessage message, bool reverseOrder = false)
        {
            return _root.HandleBroadcast(message, reverseOrder);
        }
    }
}
