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
                Position = new Vector2(75, 75),
                Outline = Main.LoadedTextures["MathSnakeButtonOutline"]
            };

            var TriviaButton = new GameChangerButton(Main.LoadedTextures["TriviaButton"], new BatalhaTrivia(graphics, spriteBatch, Font))
            {
                Position = new Vector2(75 + 250, 75),
                Outline = Main.LoadedTextures["TriviaButtonOutline"]
            };

            UIElements = new List<UIElement>()
            {
                SnakeButton,
                TriviaButton
            };
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle(0, 0, 250, 250), new Color(11, 13, 36));
            spriteBatch.DrawString(Font, "Cobrinhamática", new Vector2(125 - Font.MeasureString("Cobrinhamática").X / 2f, 230 - Font.MeasureString("Cobrinhamática").Y / 2f), Color.White);
            spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle(250, 0, 250, 250), Color.DarkGreen);
            spriteBatch.DrawString(Font, "Batalha Trivia", new Vector2(125 + 250 - Font.MeasureString("Batalha Trivia").X / 2f, 230 - Font.MeasureString("Batalha Trivia").Y / 2f), Color.White);

            base.Draw(gameTime, spriteBatch);
        }
    }

}