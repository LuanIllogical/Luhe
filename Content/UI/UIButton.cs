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
using MonoGame.Extended.Input;

namespace Luhe.Content.UI
{
    public class UIButton : UIElement
    {
        private MouseState CurrentMouse;
        //public string Text { get; set; }

        public UIButton(Texture2D texture)
        {
            Texture = texture;
            Color = Color.White;
            //Font = font;
            //PenColor = Color.Red;
        }
        /*
        public event EventHandler OnClick;
        */
        public virtual void Click()
        {

        }
        /*
        public event EventHandler OnRightClick;
        */
        public virtual void RightClick()
        {

        }
        public override void Update(GameTime gameTime)
        {
            var mouseRectangle = new Rectangle((int)Main.MousePosition.X, (int)Main.MousePosition.Y, 1, 1);

            IsHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                IsHovering = true;

                if (MouseExtended.GetState().WasButtonJustDown(MouseButton.Left))
                {
                 //   OnClick?.Invoke(this, new EventArgs());
                    Click();
                }
                else if (MouseExtended.GetState().WasButtonJustDown(MouseButton.Right))
                {
                   // OnRightClick?.Invoke(this, new EventArgs());
                    RightClick();
                }
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color);
            /*
            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (Font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (Font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(Font, Text, new Vector2(x, y), PenColor);
            }
            */
        }
    }
}
