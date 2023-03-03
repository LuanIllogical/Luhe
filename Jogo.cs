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
        }
        public virtual void Update(GameTime gameTime)
        {
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}