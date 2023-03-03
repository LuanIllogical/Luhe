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
            BackgroundColor = Color.Orange;
        }

        public override void Initialize()
        {
            var SnakeButton = new SnakeButton(Main.LoadedTextures["MathSnakeButton"], new MathSnake(graphics, spriteBatch, Font))
            {
                Position = new Vector2(Main.RenderTargetDestination.Width * 0.05f, Main.RenderTargetDestination.Height * 0.05f),
                Outline = Main.LoadedTextures["MathSnakeButtonOutline"]
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
        }
    }
    public class SnakeButton : GameChangerButton
    {
        public Texture2D Outline;
        public SnakeButton(Texture2D texture, Jogo jogo) : base (texture, jogo)
        {
            Jogo = jogo;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color);
            if (IsHovering)
            {
                spriteBatch.Draw(Outline, Rectangle, Color.White);
            }
        }
    }

}