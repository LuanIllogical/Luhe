using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Input;

namespace Luhe.Content.UI
{
    public class UIElement
    {
        //public SpriteFont Font;
        public bool IsHovering;
        public Texture2D Texture;
        public Vector2 Scale;
        public Color Color;
        public int Width;
        public int Height;

        //public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }
        //public string Text { get; set; }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
