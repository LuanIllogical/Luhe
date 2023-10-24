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
using Luhe.Content;

namespace Luhe
{
    public class Menu : Jogo
    {
        public List<Color> MenuRandomColors;
        public Menu(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SpriteFont font) : base(graphics, spriteBatch, font)
        {
            Tipo = 0;
            BackgroundColor = Color.MediumPurple;
            Font = font;
        }

        public override void Initialize()
        {
            base.Initialize();
            var SnakeButton = new GameChangerButton(Main.LoadedTextures["MathSnakeButton"], new MathSnake(graphics, spriteBatch, Font))
            {
                Position = new Vector2(110, 70), //320 / 2 - 50
                Outline = Main.LoadedTextures["MathSnakeButtonOutline"]
            };
             
            var TriviaButton = new GameChangerButton(Main.LoadedTextures["TriviaButton"], new BatalhaTrivia(graphics, spriteBatch, Font))
            {
                Position = new Vector2(110 + 320, 70),
                Outline = Main.LoadedTextures["TriviaButtonOutline"]
            };

            UIElements = new List<UIElement>()
            {
                SnakeButton,
                TriviaButton
            };
            MenuRandomColors = new List<Color>()
            { 
                Color.White,
                Color.White,
                Color.CornflowerBlue,
                Color.Red,
                Color.Purple,
                Color.Orange,
                Color.IndianRed,
                Color.Teal,
                Color.Gray,
                Color.Pink,
                Color.Green,
                Color.MediumVioletRed,
                Color.DarkOrchid,
                Color.Gold,
                Color.White
            };

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int color = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle(320 * i, 240 * j, 320, 240), MenuRandomColors[color]);
                    if (j > 0 || j == 0 && i > 1)
                    {
                        spriteBatch.DrawString(Font, "Em Breve", new Vector2(320 * i + 160 - Font.MeasureString("Em Breve").X / 2f, 240 * j + 220 - Font.MeasureString("Em Breve").Y / 2f), Color.White);
                    }
                    color++;
                }
            }
            spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle(0, 0, 320, 240), new Color(11, 13, 36));
            spriteBatch.DrawString(Font, "Cobrinhamática", new Vector2(160 - Font.MeasureString("Cobrinhamática").X / 2f, 220 - Font.MeasureString("Cobrinhamática").Y / 2f), Color.White);
            spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle(320, 0, 320, 240), Color.DarkGreen);
            spriteBatch.DrawString(Font, "Batalha Trivia", new Vector2(160 + 320 - Font.MeasureString("Batalha Trivia").X / 2f, 220 - Font.MeasureString("Batalha Trivia").Y / 2f), Color.White);

            base.Draw(gameTime, spriteBatch);
        }
    }

}