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

        public  Color BackgroundColor = Color.Orange;
        private List<UIElement> UIElements;

        public static Vector2 MousePosition;

        public static int CurrentResolution;
        public static int[] ScreenWidths;
        public static int[] ScreenHeights;

        public RenderTarget2D RenderTarget;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.SynchronizeWithVerticalRetrace= true;
            //graphics.HardwareModeSwitch = false;
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            ScreenWidths = new int[] { 1920, 960, 1366, 1280, 1280, 1366 };
            ScreenHeights = new int[] { 1080, 540, 768, 1024, 720, 680 };

            RenderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var randomButton = new ResolutionArrow(Content.Load<Texture2D>("UI/Arrow"))
            {
                Position = new Vector2(ScreenWidths[CurrentResolution] / 2f, ScreenHeights[CurrentResolution] / 2f),
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach(var uiElement in UIElements)
            {
                uiElement.Update(gameTime);
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.SetRenderTarget(RenderTarget);

            var scaleX = (float)ScreenWidths[CurrentResolution] / 1920;
            var scaleY = (float)ScreenHeights[CurrentResolution] / 1080;
            var matrix = Matrix.CreateScale(scaleX, scaleX, 1.0f);

            spriteBatch.Begin(transformMatrix: matrix);

            GraphicsDevice.Clear(BackgroundColor);            

            foreach (var uiElement in UIElements)
            {
                uiElement.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                        SamplerState.LinearClamp, DepthStencilState.Default,
                        RasterizerState.CullNone);

            var offsetX = ScreenWidths[CurrentResolution] / 2 - 1920 / 2 * scaleX;
            var offsetY = ScreenHeights[CurrentResolution] / 2 - 1080 / 2 * scaleY;
            spriteBatch.Draw(RenderTarget, new Rectangle((int)offsetX, (int)offsetY, (int)(1920), (int)(1080)), Color.White);

            spriteBatch.End();

            var mouseState = Mouse.GetState();
            MousePosition = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), Matrix.Invert(matrix));

            base.Draw(gameTime);
        }
    }
}