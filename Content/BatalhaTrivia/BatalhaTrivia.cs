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
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using MonoGame.Extended.Collisions;
using static Luhe.MathSnake;
using MonoGame.Extended.BitmapFonts;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Audio;
using System.Text;

namespace Luhe
{
    public class BatalhaTrivia : Jogo
    {
        public string PerguntaAtual = "O Brasil está localizado em 3 hemisférios, qual é o único em que ele não está?";
        public string[] Respostas;
        public int TempoRestante;
        public BatalhaTrivia(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SpriteFont font) : base(graphics, spriteBatch, font)
        {
            Tipo = 2;
            BackgroundColor = Color.Green;
        }

        public override void Initialize()
        {
            base.Initialize();
            Respostas = new string[4] { "", "", "", "" };
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Respostas[0] = "Hemisfério Norte.";
            Respostas[1] = "Hemisfério Sul.";
            Respostas[2] = "Hemisfério Ocidental.";
            Respostas[3] = "Hemisfério Oriental.";
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(Font, PerguntaAtual, new Vector2(WidthOriginal / 2f - Font.MeasureString(PerguntaAtual).X / 2f, HeightOriginal * 0.05f), Color.White);
            spriteBatch.DrawString(Font, Respostas[0], new Vector2(WidthOriginal / 2f - Font.MeasureString(Respostas[0]).X / 2f, HeightOriginal * 0.35f), Color.White);
            spriteBatch.DrawString(Font, Respostas[1], new Vector2(WidthOriginal / 2f - Font.MeasureString(Respostas[1]).X / 2f, HeightOriginal * 0.45f), Color.White);
            spriteBatch.DrawString(Font, Respostas[2], new Vector2(WidthOriginal / 2f - Font.MeasureString(Respostas[2]).X / 2f, HeightOriginal * 0.55f), Color.White);
            spriteBatch.DrawString(Font, Respostas[3], new Vector2(WidthOriginal / 2f - Font.MeasureString(Respostas[3]).X / 2f, HeightOriginal * 0.65f), Color.White);
            spriteBatch.DrawString(Font, TempoRestante.ToString(), new Vector2(WidthOriginal / 2f - Font.MeasureString("7 Segundos.").X / 2f, HeightOriginal * 0.90f), Color.White);
        }
    }
}