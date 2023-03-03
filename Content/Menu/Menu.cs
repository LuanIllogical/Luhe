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

namespace Luhe
{
    public class Menu : Jogo
    {
        public Menu(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SpriteFont font) : base(graphics, spriteBatch, font)
        {
            Tipo = 0;
            BackgroundColor = Color.Blue;
        }

        public override void Initialize()
        {
            var SnakeButton = new GameChangerButton(Main.LoadedTextures["Flecha"], new MathSnake(graphics, spriteBatch, Font))
            {
                Position = new Vector2(graphics.PreferredBackBufferWidth / 2f, graphics.PreferredBackBufferHeight / 2f)
            };
            UIElements = new List<UIElement>()
            {
                SnakeButton
            };
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var uiElement in UIElements)
            {
                uiElement.Update(gameTime);
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var uiElement in UIElements)
            {
                uiElement.Draw(gameTime, spriteBatch);
            }

            spriteBatch.Draw(Main.LoadedTextures["Flecha"], new Vector2(Main.MousePosition.X, Main.MousePosition.Y), Color.Blue);
        }
    }
}