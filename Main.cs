using Autofac.Core.Lifetime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Wiselike.Content.UI;

namespace Wiselike
{
    public class Main : Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public Color BackgroundColor = Color.Orange;
        private List<UIElement> UIElements;

        public MouseStateWrapper mouseWrapper;
        public static Vector2 MousePosition;

        public RenderTarget2D RenderTarget;
        Point GameResolution = new Point(1280, 720);

        public static Rectangle RenderTargetDestination;

        Color LetterboxingColor = new Color(0, 0, 0);
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
            /*RenderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);
            */
            RenderTarget = new RenderTarget2D(GraphicsDevice, GameResolution.X, GameResolution.Y);
            RenderTargetDestination = GetRenderTargetDestination(GameResolution, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            mouseWrapper = new MouseStateWrapper(true);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var randomButton = new ResolutionArrow(Content.Load<Texture2D>("UI/Arrow"))
            {
                Position = new Vector2(graphics.PreferredBackBufferWidth / 2f, graphics.PreferredBackBufferHeight / 2f),
                //Text = "Random"
            };

            UIElements = new List<UIElement>()
            {
                randomButton
            };

            // TODO: use this.Content to load your game content here
        }
        protected override void Update(GameTime gameTime)
        {
            //Exit the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Update UIElements
            foreach (var uiElement in UIElements)
            {
                uiElement.Update(gameTime);
            }
            //Set correct Mouse position;
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
            GraphicsDevice.Clear(BackgroundColor);
            foreach (var uiElement in UIElements)
            {
                uiElement.Draw(gameTime, spriteBatch);
            }

            spriteBatch.Draw(Content.Load<Texture2D>("UI/Arrow"), new Vector2(MousePosition.X, MousePosition.Y), Color.Blue);
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
                // Resolution and window/screen share aspect ratio
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