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
using MonoGame.Extended.BitmapFonts;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Audio;

namespace Luhe.Content
{
    public class MathSnake : Jogo
    {
        public string FirstNumber = "□";
        public string Operator = "□";
        public string SecondNumber = "□";
        public string Result = "?";
        public int Points;
        public int Highscore;

        public int Grid = 32;
        public int Count = 0;

        public bool SnakeDead = false;

        public int Wait = 0;

        public bool Auto;

        //Sons

        public List<string> EquationsFinished1 = new List<string>();
        public List<string> EquationsFinished2 = new List<string>();
        public List<NumberObject> Numbers = new List<NumberObject>();
        public string Operation;
        public int Phase;
        public int CorrectAnswer;

        public List<Keys> DirectionsToTurn = new List<Keys>();
        public Snake OurSnake;

        public int BackgroundOffsetX;
        public int BackgroundOffsetY;

        public SoundEffectInstance Move;
        public SoundEffectInstance Collect;
        public SoundEffectInstance Die;
        public MathSnake(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SpriteFont font) : base(graphics, spriteBatch, font)
        {
            Tipo = 1;
            BackgroundColor = new Color(11, 13, 36);
        }

        public override void Initialize()
        {
            base.Initialize();
            OurSnake = new Snake();
            BackgroundOffsetX = (int)(WidthOriginal / 2f - 288);
            BackgroundOffsetY = (int)(HeightOriginal / 2f) - 288;
            OurSnake.Cells.Add(new Vector2(5 * 32, 5 * 32));
            Move = Main.LoadedSounds["MathSnakeMove"].CreateInstance();
            Collect = Main.LoadedSounds["MathSnakeCollect"].CreateInstance();
            Die = Main.LoadedSounds["MathSnakeDie"].CreateInstance();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            /*
            foreach (var uiElement in UIElements)
            {
                uiElement.Update(gameTime);
            }
            */
            KeyboardState state = Keyboard.GetState();
            float pitch = (float)Main.Random.NextDouble() * 0.5f;
            if (state.IsKeyDown(Keys.Left) && OurSnake.DX == 0)
            {
                DirectionsToTurn.Add(Keys.Left);
                OurSnake.DX = -Grid;
                OurSnake.DY = 0;
                Move.Stop();
                Move.Pitch = pitch;
                Move.Play();
            }
            else if (state.IsKeyDown(Keys.Up) && OurSnake.DY == 0)
            {
                OurSnake.DY = -Grid;
                OurSnake.DX = 0;
                Move.Stop();
                Move.Pitch = pitch;
                Move.Play();
            }
            else if (state.IsKeyDown(Keys.Right) && OurSnake.DX == 0)
            {
                OurSnake.DX = Grid;
                OurSnake.DY = 0; ;
                Move.Stop();
                Move.Pitch = pitch;
                Move.Play();
            }
            else if (state.IsKeyDown(Keys.Down) && OurSnake.DY == 0)
            {
                OurSnake.DY = Grid;
                OurSnake.DX = 0;
                Move.Stop();
                Move.Pitch = pitch;
                Move.Play();
            }
            if (++Count < 8)
            {
                return;
            }
            Count = 0;
            if (Phase == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    NumberObject thisNumber = new NumberObject();
                    thisNumber.Number = Main.Random.Next(1, 10).ToString();
                    var numberSpawn = GetNumberSpawn();
                    thisNumber.Y = (int)numberSpawn.Y;
                    thisNumber.X = (int)numberSpawn.X;
                    Numbers.Add(thisNumber);
                }
                Phase = 1;
            }
            //Sons
            if (Wait == 0)
            {
                
                
                if (state.IsKeyDown(Keys.Back))
                {
                    Main.JogoAtual = new Menu(graphics, spriteBatch, Font);
                    Main.JogoAtual.Initialize();
                }
                if (state.IsKeyDown(Keys.K))
                {
                    Auto = !Auto;
                }
                if (SnakeDead)
                {
                    if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.Down))
                    {
                        SnakeDead = false;
                        OurSnake.X = 160;
                        OurSnake.Y = 160;
                        OurSnake.Cells = new List<Vector2>();
                        OurSnake.MaxCells = 4;
                        OurSnake.DX = Grid;
                        OurSnake.DY = 0;
                        Wait = 0;
                        Phase = 1;
                        FirstNumber = "□";
                        Operator = "□";
                        SecondNumber = "□";
                        EquationsFinished1.Clear();
                        EquationsFinished2.Clear();
                        if (Points > Highscore)
                        {
                            Highscore = Points;
                        }

                        Points = 0;

                        for (int i5 = 0; i5 < 6; i5++)
                        {
                            Numbers[i5].Number = Main.Random.Next(1, 10).ToString();
                            var numberSpawn = GetNumberSpawn();
                            Numbers[i5].X = (int)numberSpawn.X;
                            Numbers[i5].Y = (int)numberSpawn.Y;
                        }
                    }
                }
                if (Auto)
                {
                }

                if (SnakeDead == false)
                {
                    OurSnake.X += OurSnake.DX;
                    OurSnake.Y += OurSnake.DY;
                }

            }
            else
            {
                Wait -= 1;
            }
            for (int i = 0; i < OurSnake.Cells.Count; i++)
                {
                    for (int i2 = 0; i2 < 6; i2++)
                    {
                        if (OurSnake.Cells[i].X == Numbers[i2].X && OurSnake.Cells[i].Y == Numbers[i2].Y)
                        {
                            if (SnakeDead == false)
                            {
                                OurSnake.MaxCells++;
                                /*
                                soundCollect.stop();
                                soundCollect.currentTime = 0;
                                soundCollect.play();
                                */
                            }
                            if (Phase == 1)
                            {
                                FirstNumber = Numbers[i2].Number.ToString();

                                Numbers[0].Number = "+";
                                var numberSpawn = GetNumberSpawn();
                                Numbers[0].X = (int)numberSpawn.X;
                                Numbers[0].Y = (int)numberSpawn.Y;
                                while (Numbers[0].X == OurSnake.Cells[i].X && Numbers[0].Y == OurSnake.Cells[i].Y)
                                {
                                    numberSpawn = GetNumberSpawn();
                                    Numbers[0].X = (int)numberSpawn.X;
                                    Numbers[0].Y = (int)numberSpawn.Y;
                                }

                                Numbers[1].Number = "-";
                                numberSpawn = GetNumberSpawn();
                                Numbers[1].X = (int)numberSpawn.X;
                                Numbers[1].Y = (int)numberSpawn.Y;
                                while (Numbers[1].X == OurSnake.Cells[i].X && Numbers[1].Y == OurSnake.Cells[i].Y)
                                {
                                    numberSpawn = GetNumberSpawn();
                                    Numbers[1].X = (int)numberSpawn.X;
                                    Numbers[1].Y = (int)numberSpawn.Y;
                                }

                                Numbers[2].Number = "×";
                                numberSpawn = GetNumberSpawn();
                                Numbers[2].X = (int)numberSpawn.X;
                                Numbers[2].Y = (int)numberSpawn.Y;
                                while (Numbers[2].X == OurSnake.Cells[i].X && Numbers[2].Y == OurSnake.Cells[i].Y)
                                {
                                    numberSpawn = GetNumberSpawn();
                                    Numbers[2].X = (int)numberSpawn.X;
                                    Numbers[2].Y = (int)numberSpawn.Y;
                                }

                                Numbers[3].Number = "";
                                Numbers[3].X = -1;
                                Numbers[3].Y = -1;

                                Numbers[4].Number = "";
                                Numbers[4].X = -1;
                                Numbers[4].Y = -1;

                                Numbers[5].Number = "";
                                Numbers[5].X = -1;
                                Numbers[5].Y = -1;

                                Phase = 2;
                                Points += 15;
                                Collect.Play();
                            }
                            else if (Phase == 2)
                            {
                                Operator = Numbers[i2].Number.ToString();

                                for (int i3 = 0; i3 < 6; i3++)
                                {
                                    Numbers[i3].Number = Main.Random.Next(1, 10).ToString();
                                    var numberSpawn = GetNumberSpawn();
                                    Numbers[i3].X = (int)numberSpawn.X;
                                    Numbers[i3].Y = (int)numberSpawn.Y;
                                    while (Numbers[i3].X == OurSnake.Cells[i].X && Numbers[i3].Y == OurSnake.Cells[i].Y)
                                    {
                                        numberSpawn = GetNumberSpawn();
                                        Numbers[i3].X = (int)numberSpawn.X;
                                        Numbers[i3].Y = (int)numberSpawn.Y;
                                    }
                                }

                                Phase = 3;
                                Points += 40;
                                Collect.Play();
                            }
                            else if (Phase == 3)
                            {
                                SecondNumber = Numbers[i2].Number.ToString();

                                if (Operator == "+")
                                {
                                    CorrectAnswer = int.Parse(FirstNumber) + int.Parse(SecondNumber);
                                }
                                else if (Operator == "-")
                                {
                                    CorrectAnswer = int.Parse(FirstNumber) - int.Parse(SecondNumber);
                                }
                                else if (Operator == "×")
                                {
                                    CorrectAnswer = int.Parse(FirstNumber) * int.Parse(SecondNumber);
                                }

                                Numbers[0].Number = CorrectAnswer.ToString();
                                var numberSpawn = GetNumberSpawn();
                                Numbers[0].X = (int)numberSpawn.X;
                                Numbers[0].Y = (int)numberSpawn.Y;
                                while (Numbers[0].X == OurSnake.Cells[i].X && Numbers[0].Y == OurSnake.Cells[i].Y)
                                {
                                    numberSpawn = GetNumberSpawn();
                                    Numbers[0].X = (int)numberSpawn.X;
                                    Numbers[0].Y = (int)numberSpawn.Y;
                                }

                                for (int i4 = 1; i4 < 6; i4++)
                                {
                                    Numbers[i4].Number = (CorrectAnswer + Main.Random.Next(-10, 10)).ToString();
                                    numberSpawn = GetNumberSpawn();
                                    Numbers[i4].X = (int)numberSpawn.X;
                                    Numbers[i4].Y = (int)numberSpawn.Y;
                                    while (Numbers[i4].X == OurSnake.Cells[i].X && Numbers[i4].Y == OurSnake.Cells[i].Y)
                                    {
                                        numberSpawn = GetNumberSpawn();
                                        Numbers[i4].X = (int)numberSpawn.X;
                                        Numbers[i4].Y = (int)numberSpawn.Y;
                                    }
                                }
                                Wait = 10;

                                Phase = 4;
                                Points += 65;
                                Collect.Play();
                            }
                            else
                            {
                                if (Numbers[i2].Number == CorrectAnswer.ToString())
                                {
                                    for (int i5 = 0; i5 < 6; i5++)
                                    {
                                        Numbers[i5].Number = Main.Random.Next(1, 10).ToString();
                                        var numberSpawn = GetNumberSpawn();
                                        Numbers[i5].X = (int)numberSpawn.X;
                                        Numbers[i5].Y = (int)numberSpawn.Y;
                                        while (Numbers[i5].X == OurSnake.Cells[i].X && Numbers[i5].Y == OurSnake.Cells[i].Y)
                                        {
                                            numberSpawn = GetNumberSpawn();
                                            Numbers[i5].X = (int)numberSpawn.X;
                                            Numbers[i5].Y = (int)numberSpawn.Y;
                                        }
                                    }
                                    if (EquationsFinished1.Count == EquationsFinished2.Count)
                                    {
                                        EquationsFinished1.Add(FirstNumber + " " + Operator + " " + SecondNumber + " = " + CorrectAnswer);
                                    }
                                    else
                                    {
                                        EquationsFinished2.Add(FirstNumber + " " + Operator + " " + SecondNumber + " = " + CorrectAnswer);
                                    }

                                    FirstNumber = "□";
                                    Operator = "□";
                                    SecondNumber = "□";

                                    Phase = 1;
                                    Points += 125;
                                    Collect.Play();
                                }
                                else
                                {
                                    if (SnakeDead == false)
                                    {
                                    /*
                                    soundDie.stop();
                                    soundDie.currentTime = 0;
                                    soundDie.play();
                                    */
                                        Die.Play();
                                    }
                                    SnakeDead = true;
                                }
                            }
                        }
                    }
                    if (Wait > 0)
                {
                    return;
                }
                    for (int i2 = i + 1; i2 < OurSnake.Cells.Count; i2++)
                    {
                        if (OurSnake.Cells[i2].X == OurSnake.Cells[i].X && OurSnake.Cells[i2].Y == OurSnake.Cells[i].Y)
                        {
                            if (SnakeDead == false)
                            {
                            /*
                            soundDie.stop();
                            soundDie.currentTime = 0;
                            soundDie.play();
                            */
                                Die.Play();
                            }
                            SnakeDead = true;
                            
                        }
                    }
                    if (OurSnake.X < 0 || OurSnake.X >= 576 || OurSnake.Y < 0 || OurSnake.Y >= 576)
                    {
                        if (SnakeDead == false)
                        {
                        /*
                        soundDie.stop();
                        soundDie.currentTime = 0;
                        soundDie.play();
                        */
                            Die.Play();
                        }
                        SnakeDead = true;
                    }
                }
            if (!SnakeDead)
            {
                OurSnake.Cells.Add(new Vector2(OurSnake.X, OurSnake.Y));
                if (OurSnake.Cells.Count > OurSnake.MaxCells)
                {
                    OurSnake.Cells.RemoveAt(0);
                }
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(Font, FirstNumber, new Vector2(WidthOriginal / 2f - 50 - Font.MeasureString(SecondNumber).X / 2f, HeightOriginal * 0.025f), Color.White);
            spriteBatch.DrawString(Font, Operator, new Vector2(WidthOriginal / 2f - 25 - Font.MeasureString(SecondNumber).X / 2f, HeightOriginal * 0.025f), Color.White);
            spriteBatch.DrawString(Font, SecondNumber, new Vector2(WidthOriginal / 2f - Font.MeasureString(SecondNumber).X / 2f, HeightOriginal * 0.025f), Color.White);
            spriteBatch.DrawString(Font, "=", new Vector2(WidthOriginal / 2f + 25 - Font.MeasureString(SecondNumber).X / 2f, HeightOriginal * 0.025f), Color.White);
            spriteBatch.DrawString(Font, Result, new Vector2(WidthOriginal / 2f + 50 - Font.MeasureString(SecondNumber).X / 2f, HeightOriginal * 0.025f), Color.White);
            spriteBatch.DrawString(Font, "Pontos: " + Points, new Vector2(WidthOriginal * 0.05f - Font.MeasureString(SecondNumber).X / 2f, HeightOriginal * 0.025f), Color.White);
            spriteBatch.DrawString(Font, "Highscore: " + Highscore, new Vector2(WidthOriginal * 0.875f - Font.MeasureString("Highscore: " + Highscore).X / 2f, HeightOriginal * 0.025f), Color.White);
            //FIX MEASURESTRING SECONDNUMBER

            for (var i = 0; i < EquationsFinished1.Count; i++)
            {
                spriteBatch.DrawString(Font, EquationsFinished1[i], new Vector2(WidthOriginal / 2f - 500 - Font.MeasureString(EquationsFinished1[i]).X / 2f, HeightOriginal * 0.025f + 50 + (35 * i)), Color.White);
            }
            for (var i = 0; i < EquationsFinished2.Count; i++)
            {
                spriteBatch.DrawString(Font, EquationsFinished2[i], new Vector2(WidthOriginal / 2f + 500 - Font.MeasureString(EquationsFinished1[i]).X / 2f, HeightOriginal * 0.025f + 50 + (35 * i)), Color.White);
            }


            for (var i = 0; i < 18; i++)
            {
                for (var j = 0; j < 18; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle(i * 32 + BackgroundOffsetX, j * 32 + BackgroundOffsetY, Grid, Grid), new Color(41, 47, 97));
                    }
                    else
                    {
                        spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle(i * 32 + BackgroundOffsetX, j * 32 + BackgroundOffsetY, Grid, Grid), new Color(27, 32, 69));
                    }
                }
            }

            if (Phase > 0)
            {            
                for (int i2 = 0; i2 < 6; i2++)
                {
                    float scale = 1f;
                    float positionFixer = 0f;
                    if (Phase != 2 && int.Parse(Numbers[i2].Number) < -9)
                    {
                        scale = 0.65f;
                        positionFixer = 7f;
                    }
                    spriteBatch.DrawString(Font, Numbers[i2].Number, new Vector2(Numbers[i2].X + 16 - Font.MeasureString(Numbers[i2].Number).X / 2f + BackgroundOffsetX + positionFixer, Numbers[i2].Y + 16 - Font.MeasureString(Numbers[i2].Number).Y / 3f + BackgroundOffsetY + (positionFixer / 2f)), Color.White, 0f, default, scale, SpriteEffects.None, 1f);
                }      
            }

            for (int i = 0; i < OurSnake.Cells.Count; i++)
            {
                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)OurSnake.Cells[i].X + 3 + BackgroundOffsetX, (int)OurSnake.Cells[i].Y + 3 + BackgroundOffsetY, Grid - 6, Grid - 6), new Color(125, 213, 240));
                

                /*
                foreach (var uiElement in UIElements)
                {
                    uiElement.Draw(gameTime, spriteBatch);
                }
                */
            }
            if (OurSnake.DX == -Grid)
            {
                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)OurSnake.Cells[OurSnake.Cells.Count - 1].X + 3 + 4 + BackgroundOffsetX, (int)OurSnake.Cells[OurSnake.Cells.Count - 1].Y + 3 + 4 + BackgroundOffsetY, 6, 6), new Color(255, 234, 0));
                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)OurSnake.Cells[OurSnake.Cells.Count - 1].X + 3 + 4 + BackgroundOffsetX, (int)OurSnake.Cells[OurSnake.Cells.Count - 1].Y + 3 + 16 + BackgroundOffsetY, 6, 6), new Color(255, 234, 0));
            }
            else if (OurSnake.DY == -Grid)
            {
                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)OurSnake.Cells[OurSnake.Cells.Count - 1].X + 3 + 4 + BackgroundOffsetX, (int)OurSnake.Cells[OurSnake.Cells.Count - 1].Y + 3 + 4 + BackgroundOffsetY, 6, 6), new Color(255, 234, 0));
                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)OurSnake.Cells[OurSnake.Cells.Count - 1].X + 3 + 16 + BackgroundOffsetX, (int)OurSnake.Cells[OurSnake.Cells.Count - 1].Y + 3 + 4 + BackgroundOffsetY, 6, 6), new Color(255, 234, 0));
            }
            else if (OurSnake.DX == Grid)
            {
                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)OurSnake.Cells[OurSnake.Cells.Count - 1].X + 3 + 16 + BackgroundOffsetX, (int)OurSnake.Cells[OurSnake.Cells.Count - 1].Y + 3 + 4 + BackgroundOffsetY, 6, 6), new Color(255, 234, 0));
                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)OurSnake.Cells[OurSnake.Cells.Count - 1].X + 3 + 16 + BackgroundOffsetX, (int)OurSnake.Cells[OurSnake.Cells.Count - 1].Y + 3 + 16 + BackgroundOffsetY, 6, 6), new Color(255, 234, 0));
            }
            else if (OurSnake.DY == Grid)
            {
                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)OurSnake.Cells[OurSnake.Cells.Count - 1].X + 3 + 4 + BackgroundOffsetX, (int)OurSnake.Cells[OurSnake.Cells.Count - 1].Y + 3 + 16 + BackgroundOffsetY, 6, 6), new Color(255, 234, 0));
                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)OurSnake.Cells[OurSnake.Cells.Count - 1].X + 3 + 16 + BackgroundOffsetX, (int)OurSnake.Cells[OurSnake.Cells.Count - 1].Y + 3 + 16 + BackgroundOffsetY, 6, 6), new Color(255, 234, 0));

            }
            if (SnakeDead == true)
            {
                spriteBatch.DrawString(Font, "Game Over", new Vector2(WidthOriginal * 0.5f - Font.MeasureString("Game Over").X * 1.4f / 2f, HeightOriginal * 0.4f - Font.MeasureString("Game Over").Y / 2f), new Color(204, 239, 255), 0f, default, 1.4f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Font, "Aperte qualquer seta para reiniciar", new Vector2(WidthOriginal * 0.5f - Font.MeasureString("Aperte qualquer seta para reiniciar").X / 2f, HeightOriginal * 0.5f - Font.MeasureString("Aperte qualquer seta para reiniciar").Y / 2f), new Color(204, 239, 255), 0f, default, 1f, SpriteEffects.None, 1f);

            }
        }
        public Vector2 GetNumberSpawn()
        {
            var willTouchSnake = true;
            var spawnX = Main.Random.Next(0, 18) * Grid;
            var spawnY = Main.Random.Next(0, 18) * Grid;

            while (willTouchSnake)
            {             
                spawnX = Main.Random.Next(0, 18) * Grid;
                spawnY = Main.Random.Next(0, 18) * Grid;
                while (OurSnake.X == spawnX)
                {
                    spawnX = Main.Random.Next(0, 18) * Grid;
                }
                while (OurSnake.Y == spawnY)
                {
                    spawnY = Main.Random.Next(0, 18) * Grid;
                }
                willTouchSnake = false;
                foreach (NumberObject number in Numbers)
                {
                    if (spawnX == number.X && spawnY == number.Y)
                    {
                        willTouchSnake = true;
                    }
                }
                willTouchSnake = false;
                foreach (Vector2 position in OurSnake.Cells)
                {
                    if (spawnX == position.X && spawnY == position.Y)
                    {
                        willTouchSnake = true;
                    }
                }
            }
            return new Vector2(spawnX, spawnY);
        }

        public class NumberObject
        {
            public string Number;
            public int X;
            public int Y;
        }
        public class Snake
        {
            public int X = 160; //5 * 32 (Grid)
            public int Y = 160;
            public int DX = 32; //Grid
            public int DY = 0;
            public List<Vector2> Cells = new List<Vector2>();

            public int MaxCells = 4;
        }
    }
}