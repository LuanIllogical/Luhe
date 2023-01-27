using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Input.InputListeners;
using Microsoft.Xna.Framework.Input;

namespace Wiselike.Content.UI
{
    public class UIButton : UIElement
    {
        private MouseState CurrentMouse;
        private SpriteFont Font;
        private bool IsHovering;
        private MouseState PreviousMouse;
        private Texture2D Texture;

        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }
        public string Text { get; set; }

        public UIButton(Texture2D texture, SpriteFont font)
        {
            Texture = texture;
            Font = font;
            PenColor = Color.Red;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = Color.White;
            if (IsHovering)
            {
                color = Color.Gray;
            }

            spriteBatch.Draw(Texture, Rectangle, color);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (Font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (Font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(Font, Text, new Vector2(x, y), PenColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            PreviousMouse = CurrentMouse;
            CurrentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(CurrentMouse.X, CurrentMouse.Y, 1, 1);

            IsHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                IsHovering = true;

                if (CurrentMouse.LeftButton == ButtonState.Released && PreviousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
