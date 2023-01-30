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

namespace Wiselike.Content.UI
{
    public class ResolutionArrow : UIButton
    {
        bool balls;
        public ResolutionArrow(Texture2D texture) : base(texture)
        {

        }
        public override void Click()
        {
        }
        public override void Update(GameTime gameTime)
        {
            //Position = new Vector2(Main.ScreenWidths[Main.CurrentResolution] / 2f, Main.ScreenHeights[Main.CurrentResolution] / 2f);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsHovering)
            {
                Color = Color.Red;
            }
            else
            {
                Color = Color.White;
            }
            base.Draw(gameTime, spriteBatch);
        }
    }
}
