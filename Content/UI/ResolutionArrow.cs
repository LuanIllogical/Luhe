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
        public ResolutionArrow(Texture2D texture) : base(texture)
        {

        }
        public override void Click()
        {

            if (Main.CurrentResolution >= 0 && Main.CurrentResolution < Main.ScreenWidths.Length - 1)
            {
                Main.CurrentResolution++;
                Main.graphics.PreferredBackBufferWidth = Main.ScreenWidths[Main.CurrentResolution];
                Main.graphics.PreferredBackBufferHeight = Main.ScreenHeights[Main.CurrentResolution];
                Main.graphics.ApplyChanges();
            }
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
