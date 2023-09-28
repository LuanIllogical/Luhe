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
    public class Jogo
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public MouseStateWrapper mouseWrapper;
        public List<UIElement> UIElements;

        public int WidthOriginal;
        public int HeightOriginal;
        public int A;

        public Color BackgroundColor;
        public int Tipo;

        public SpriteFont Font;
        public Jogo(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, SpriteFont font)
        {
            graphics = _graphics;
            spriteBatch = _spriteBatch;
            Font = font;
        }

        public virtual void Initialize()
        {
            UIElements = new List<UIElement>();

            WidthOriginal = Main.RenderTargetDestination.Width;
            HeightOriginal = Main.RenderTargetDestination.Height;
        }
        public virtual void Update(GameTime gameTime)
        {
            foreach (var uiElement in UIElements)
            {
                uiElement.Update(gameTime);
            }
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var uiElement in UIElements)
            {
                uiElement.Draw(gameTime, spriteBatch);
            }
        }
    }
}