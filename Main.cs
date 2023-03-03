using Autofac.Core.Lifetime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Luhe.Content.UI;
using Microsoft.Xna.Framework.Audio;

namespace Luhe
{
    public class Main : Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public Color BackgroundColor = new Color(14, 120, 196);

        public MouseStateWrapper mouseWrapper;
        public static Vector2 MousePosition;

        public RenderTarget2D RenderTarget;
        Point GameResolution = new Point(1280, 720);

        public static Rectangle RenderTargetDestination;

        Color LetterboxingColor = new Color(0, 0, 0);

        public static Jogo JogoAtual;

        public static Dictionary<string, Texture2D> LoadedTextures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SoundEffect> LoadedSounds = new Dictionary<string, SoundEffect>();

        public static Random Random;

        public SpriteFont Font;
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = GameResolution.X;
            graphics.PreferredBackBufferHeight = GameResolution.Y;
            graphics.SynchronizeWithVerticalRetrace = true;
            //graphics.HardwareModeSwitch = false;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        private void OnResize(object sender, EventArgs e)
        {
            Window.ClientSizeChanged -= OnResize;
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width < 640 ? 640 : Window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height < 360 ? 360 : Window.ClientBounds.Height;
            graphics.ApplyChanges();
            RenderTargetDestination = GetRenderTargetDestination(GameResolution, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Window.ClientSizeChanged += OnResize;
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            RenderTarget = new RenderTarget2D(GraphicsDevice, GameResolution.X, GameResolution.Y);
            RenderTargetDestination = GetRenderTargetDestination(GameResolution, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            mouseWrapper = new MouseStateWrapper(true);
            Random = new Random();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadedTextures["Flecha"] = Content.Load<Texture2D>("UI/Arrow");
            LoadedTextures["MagicRectangle"] = Content.Load<Texture2D>("UI/MagicRectangle");
            LoadedTextures["MathSnakeButton"] = Content.Load<Texture2D>("UI/MathSnakeButton");
            LoadedTextures["MathSnakeButtonOutline"] = Content.Load<Texture2D>("UI/MathSnakeButtonOutline");

            LoadedSounds["MathSnakeMove"] = Content.Load<SoundEffect>("Sounds/MathSnakeMove");
            LoadedSounds["MathSnakeCollect"] = Content.Load<SoundEffect>("Sounds/MathSnakeCollect");
            LoadedSounds["MathSnakeDie"] = Content.Load<SoundEffect>("Sounds/MathSnakeDie");

            Font = Content.Load<SpriteFont>("Fonts/Font");

            JogoAtual = new Menu(graphics, spriteBatch, Font);
            JogoAtual.Initialize();
        }
        protected override void Update(GameTime gameTime)
        {
            //Exit the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            JogoAtual.Update(gameTime);

            float resolutionRatio = (float)GameResolution.X / GameResolution.Y;
            float screenRatio;
            Point bounds = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            screenRatio = (float)bounds.X / bounds.Y;
            float scale;

            if (resolutionRatio < screenRatio)
                scale = (float)bounds.Y / GameResolution.Y;
            else
                scale = (float)bounds.X / GameResolution.X;

            var mouseState = Mouse.GetState();
            mouseWrapper.SetRenderTargetDestination(RenderTargetDestination);
            mouseWrapper.SetScreenScale(scale);
            mouseWrapper.SetMouseState(mouseState);
            MousePosition = mouseWrapper.Position;

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(RenderTarget);
            GraphicsDevice.Clear(LetterboxingColor);

            spriteBatch.Begin();
            GraphicsDevice.Clear(JogoAtual.BackgroundColor);

            JogoAtual.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(LetterboxingColor);

            spriteBatch.Begin();

            spriteBatch.Draw(RenderTarget, RenderTargetDestination, Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }
        Rectangle GetRenderTargetDestination(Point resolution, int preferredBackBufferWidth, int preferredBackBufferHeight)
        {
            float resolutionRatio = (float)resolution.X / resolution.Y;
            float screenRatio;
            Point bounds = new Point(preferredBackBufferWidth, preferredBackBufferHeight);
            screenRatio = (float)bounds.X / bounds.Y;
            float scale;
            Rectangle rectangle = new Rectangle();

            if (resolutionRatio < screenRatio)
                scale = (float)bounds.Y / resolution.Y;
            else if (resolutionRatio > screenRatio)
                scale = (float)bounds.X / resolution.X;
            else
            {
                rectangle.Size = bounds;
                return rectangle;
            }
            rectangle.Width = (int)(resolution.X * scale);
            rectangle.Height = (int)(resolution.Y * scale);
            return CenterRectangle(new Rectangle(Point.Zero, bounds), rectangle);
        }

        static Rectangle CenterRectangle(Rectangle outerRectangle, Rectangle innerRectangle)
        {
            Point delta = outerRectangle.Center - innerRectangle.Center;
            innerRectangle.Offset(delta);
            return innerRectangle;
        }
    }
}