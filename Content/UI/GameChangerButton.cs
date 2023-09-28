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
    public class GameChangerButton : UIButton
    {
        public Jogo Jogo;
        public Texture2D Outline;
        public GameChangerButton(Texture2D texture, Jogo jogo) : base(texture)
        {
            Jogo = jogo;
        }
        public override void Click()
        {
            //Unintialize
            Main.JogoAtual = Jogo;
            Main.JogoAtual.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            //Position = new Vector2(Main.ScreenWidths[Main.CurrentResolution] / 2f, Main.ScreenHeights[Main.CurrentResolution] / 2f);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(Texture, Rectangle, Color);
            if (IsHovering)
            {
                spriteBatch.Draw(Outline, Rectangle, Color.White);
            }
        }
    }
}
