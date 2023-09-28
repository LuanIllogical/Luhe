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
    public class Menu : Jogo
    {
        public Menu(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SpriteFont font) : base(graphics, spriteBatch, font)
        {
            Tipo = 0;
            BackgroundColor = Color.Orange;
        }

        public override void Initialize()
        {
            base.Initialize();
            var SnakeButton = new GameChangerButton(Main.LoadedTextures["MathSnakeButton"], new MathSnake(graphics, spriteBatch, Font))
            {
                Position = new Vector2(Main.RenderTargetDestination.Width * 0.05f, Main.RenderTargetDestination.Height * 0.05f),
                Outline = Main.LoadedTextures["MathSnakeButtonOutline"]
            };

            var TriviaButton = new GameChangerButton(Main.LoadedTextures["MathSnakeButton"], new BatalhaTrivia(graphics, spriteBatch, Font))
            {
                Position = new Vector2(Main.RenderTargetDestination.Width * 0.25f, Main.RenderTargetDestination.Height * 0.05f),
                Outline = Main.LoadedTextures["MathSnakeButtonOutline"]
            };

            UIElements = new List<UIElement>()
            {
                SnakeButton,
                TriviaButton
            };
        }
    }

}