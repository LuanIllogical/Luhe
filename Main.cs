using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Wiselike.Content.UI;

namespace Wiselike
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Color BackgroundColor = Color.Orange;
        private List<UIElement> UIElements;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var randomButton = new UIButton(Content.Load<Texture2D>("UI/Arrow"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(250, 200),
                Text = "Random"
            };

            randomButton.Click += RandomButton_Click;

            UIElements = new List<UIElement>()
            {
                randomButton
            };

            // TODO: use this.Content to load your game content here
        }

        private void RandomButton_Click(object sender, System.EventArgs e)
        {
            var random = new Random();
            BackgroundColor = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach(var uiElement in UIElements)
            {
                uiElement.Update(gameTime);
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackgroundColor);

            spriteBatch.Begin();

            foreach (var uiElement in UIElements)
            {
                uiElement.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}