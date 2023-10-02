using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public int Rotatione = 30;
        public float Rotation = 0f;
        public bool Rotato = true;
        public GameChangerButton(Texture2D texture, Jogo jogo) : base(texture)
        {
            Jogo = jogo;
        }
        public override void Click()
        {
            //Unitialize
            Main.JogoAtual = Jogo;
            Main.JogoAtual.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            //Position = new Vector2(Main.ScreenWidths[Main.CurrentResolution] / 2f, Main.ScreenHeights[Main.CurrentResolution] / 2f);
            base.Update(gameTime);
            if (IsHovering)
            {
                if (!Rotato && Rotatione < 61)
                {
                    Rotatione++;
                }
                else
                {
                    Rotato = true;
                }
                if (Rotato)
                {
                    Rotatione--;
                }
                if (Rotatione == 0)
                {
                    Rotato = false;
                }
                Rotation = MathHelper.SmoothStep(-0.7f, 0.3f, Rotatione / 60f);
            }
            else
            {
                Rotatione = 30;
                Rotation = 0f;
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
            Rectangle newRect = Rectangle;
            newRect.X += (int)origin.X;
            newRect.Y += (int)origin.Y;
            spriteBatch.Draw(Texture, newRect, null, Color, Rotation, origin, SpriteEffects.None, 0f);
            if (IsHovering)
            {
                spriteBatch.Draw(Outline, newRect, null, Color, Rotation, origin, SpriteEffects.None, 0f);
            }
        }
    }
}
