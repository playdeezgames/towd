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
        private int BackBufferWidth { get { return _screenWidth * _zoom; } }
        private int BackBufferHeight { get { return _screenHeight * _zoom; } }

        public IMessageHandler Parent => null;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D screenTexture;
        private KeyboardState oldKeyboardState = new KeyboardState();
        private GamePadState oldGamePadState = new GamePadState();
        private IMessageHandler _root;

        private CommonGame() { }

        public CommonGame(int screenWidth, int screenHeight, int zoom, Func<IMessageHandler,IMessageHandler> factory)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _zoom = zoom;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBufferHeight;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            _root = factory(this);
        }

        protected override void Initialize()
        {
            screenTexture = new Texture2D(GraphicsDevice, _screenWidth, _screenHeight);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
            var newKeyboardState = Keyboard.GetState();
            HashSet<Command> commands = new HashSet<Command>();
            foreach(var entry in keyProcessor)
            {
                if(entry.Value(newKeyboardState) && !entry.Value(oldKeyboardState))
                {
                    commands.Add(entry.Key);
                }
            }
            oldKeyboardState = newKeyboardState;
            var newGamePadState = GamePad.GetState(PlayerIndex.One);
            foreach (var entry in gamePadProcessor)
            {
                if (entry.Value(newGamePadState) && !entry.Value(oldGamePadState))
                {
                    commands.Add(entry.Key);
                }
            }
            foreach(var command in commands)
            {
                _root.HandleBroadcast(new CommandMessage(command), true);
            }
            oldGamePadState = newGamePadState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(screenTexture, new Rectangle(0, 0, BackBufferWidth, BackBufferHeight), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public IResult HandleMessage(IMessage message)
        {
            if(message.MessageId == QuitMessage.Id)
            {
                Exit();
                return new AckResult(message);
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
