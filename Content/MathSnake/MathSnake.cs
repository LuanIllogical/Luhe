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

namespace Luhe
{
    public class MathSnake : Jogo
    {
        public string FirstNumber = "□";
        public string Operator = "□";
        public string SecondNumber = "□";
        public string Result = "?";
        public int Points;

        public int Grid = 32;
        public int Count = 0;

        public bool SnakeDead = false;

        public int Wait = 0;

        //Sons

        public List<NumberObject> Numbers = new List<NumberObject>();
        public string Operation;
        public int Phase;
        public int CorrectAnswer;

        public Snake OurSnake;

        public int BackgroundOffsetX;
        public int BackgroundOffsetY;
        public MathSnake(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SpriteFont font) : base(graphics, spriteBatch, font)
        {
            Tipo = 1;
            BackgroundColor = new Color(11, 13, 36);
        }

        public override void Initialize()
        {
            OurSnake = new Snake();
            BackgroundOffsetX = (int)(Main.RenderTargetDestination.Width / 2f - 288);
            BackgroundOffsetY = (int)(Main.RenderTargetDestination.Height / 2f) - 288;
            OurSnake.Cells.Add(new Vector2(12, 12));
        }
        public override void Update(GameTime gameTime)
        {
            /*
            foreach (var uiElement in UIElements)
            {
                uiElement.Update(gameTime);
            }
            */
            if (++Count < 6)
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
                    thisNumber.X = GetNumberSpawn();
                    thisNumber.X = GetNumberSpawn();
                    Numbers.Add(thisNumber);
                }
                Phase = 1;
            }
            //Sons
            var SnakeReborn = false;
            if (Wait == 0)
            {
                KeyboardState state = Keyboard.GetState();
                if (state.IsKeyDown(Keys.Left) && OurSnake.DX == 0)
                {
                    OurSnake.DX = -Grid;
                    OurSnake.DY = 0;
                    SnakeReborn = true;
                }
                else if (state.IsKeyDown(Keys.Up) && OurSnake.DY == 0)
                {
                    OurSnake.DY = -Grid;
                    OurSnake.DX = 0;
                    SnakeReborn = true;
                }
                else if (state.IsKeyDown(Keys.Right) && OurSnake.DX == 0)
                {
                    OurSnake.DX = Grid;
                    OurSnake.DY = 0;
                    SnakeReborn = true;
                }
                else if (state.IsKeyDown(Keys.Down) && OurSnake.DY == 0)
                {
                    OurSnake.DY = Grid;
                    OurSnake.DX = 0;
                    SnakeReborn = true;
                }
                if (SnakeDead && SnakeReborn)
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
                    Points = 0;

                    for (int i5 = 0; i5 < 6; i5++)
                    {
                        Numbers[i5].Number = Main.Random.Next(1, 10).ToString();
                        Numbers[i5].X = GetNumberSpawn();
                        Numbers[i5].Y = GetNumberSpawn();
                    }
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
                                Numbers[0].X = GetNumberSpawn();
                                Numbers[0].Y = GetNumberSpawn();
                                while (Numbers[0].X == OurSnake.Cells[i].X && Numbers[0].Y == OurSnake.Cells[i].Y)
                                {
                                    Numbers[0].X = GetNumberSpawn();
                                    Numbers[0].Y = GetNumberSpawn();
                                }

                                Numbers[1].Number = "-";
                                Numbers[1].X = GetNumberSpawn();
                                Numbers[1].Y = GetNumberSpawn();
                                while (Numbers[1].X == OurSnake.Cells[i].X && Numbers[1].Y == OurSnake.Cells[i].Y)
                                {
                                    Numbers[1].X = GetNumberSpawn();
                                    Numbers[1].Y = GetNumberSpawn();
                                }

                                Numbers[2].Number = "×";
                                Numbers[2].X = GetNumberSpawn();
                                Numbers[2].Y = GetNumberSpawn();
                                while (Numbers[2].X == OurSnake.Cells[i].X && Numbers[2].Y == OurSnake.Cells[i].Y)
                                {
                                    Numbers[2].X = GetNumberSpawn();
                                    Numbers[2].Y = GetNumberSpawn();
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
                            }
                            else if (Phase == 2)
                            {
                                Operator = Numbers[i2].Number.ToString();

                                for (int i3 = 0; i3 < 6; i3++)
                                {
                                    Numbers[i3].Number = Main.Random.Next(1, 10).ToString();
                                    Numbers[i3].X = GetNumberSpawn();
                                    Numbers[i3].Y = GetNumberSpawn();
                                    while (Numbers[i3].X == OurSnake.Cells[i].X && Numbers[i3].Y == OurSnake.Cells[i].Y)
                                    {
                                        Numbers[i3].X = GetNumberSpawn();
                                        Numbers[i3].Y = GetNumberSpawn();
                                    }
                                }

                                Phase = 3;
                                Points += 40;
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
                                Numbers[0].X = GetNumberSpawn();
                                Numbers[0].Y = GetNumberSpawn();
                                while (Numbers[0].X == OurSnake.Cells[i].X && Numbers[0].Y == OurSnake.Cells[i].Y)
                                {
                                    Numbers[0].X = GetNumberSpawn();
                                    Numbers[0].Y = GetNumberSpawn();
                                }

                                for (int i4 = 1; i4 < 6; i4++)
                                {
                                    Numbers[i4].Number = (CorrectAnswer + Main.Random.Next(-10, 10)).ToString();
                                    Numbers[i4].X = GetNumberSpawn();
                                    Numbers[i4].Y = GetNumberSpawn();
                                    while (Numbers[i4].X == OurSnake.Cells[i].X && Numbers[i4].Y == OurSnake.Cells[i].Y)
                                    {
                                        Numbers[i4].X = GetNumberSpawn();
                                        Numbers[i4].Y = GetNumberSpawn();
                                    }
                                }
                                Wait = 10;

                                Phase = 4;
                                Points += 65; 
                            }
                            else
                            {
                                if (Numbers[i2].Number == CorrectAnswer.ToString())
                                {
                                    for (int i5 = 0; i5 < 6; i5++)
                                    {
                                        Numbers[i5].Number = Main.Random.Next(1, 10).ToString();
                                        Numbers[i5].X = GetNumberSpawn();
                                        Numbers[i5].Y = GetNumberSpawn();
                                        while (Numbers[i5].X == OurSnake.Cells[i].X && Numbers[i5].Y == OurSnake.Cells[i].Y)
                                        {
                                            Numbers[i5].X = GetNumberSpawn();
                                            Numbers[i5].Y = GetNumberSpawn();
                                        }
                                    }
                                    FirstNumber = "□";
                                    Operator = "□";
                                    SecondNumber = "□";

                                    Phase = 1;
                                    Points += 125;
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
                        }
                        SnakeDead = true;
                }
                    /*
                    if (SnakeDead == true)
                    {
                        context.fillStyle = "#ccefff";
                        context.font = "40px Consolas";
                        context.fillText("Game Over", 300, 380)
                        context.font = "34px Consolas";
                        context.fillText("Aperte qualquer seta para reiniciar", 75, 420)
                        context.fillStyle = "#7dd5f0";
                    }
                    */
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
            spriteBatch.DrawString(Font, FirstNumber, new Vector2(Main.RenderTargetDestination.Width / 2f - 50 - Font.MeasureString(SecondNumber).X / 2f, Main.RenderTargetDestination.Height * 0.025f), Color.White);
            spriteBatch.DrawString(Font, Operator, new Vector2(Main.RenderTargetDestination.Width / 2f - 25 - Font.MeasureString(SecondNumber).X / 2f, Main.RenderTargetDestination.Height * 0.025f), Color.White);
            spriteBatch.DrawString(Font, SecondNumber, new Vector2(Main.RenderTargetDestination.Width / 2f - Font.MeasureString(SecondNumber).X / 2f, Main.RenderTargetDestination.Height * 0.025f), Color.White);
            spriteBatch.DrawString(Font, "=", new Vector2(Main.RenderTargetDestination.Width / 2f + 25 - Font.MeasureString(SecondNumber).X / 2f, Main.RenderTargetDestination.Height * 0.025f), Color.White);
            spriteBatch.DrawString(Font, Result, new Vector2(Main.RenderTargetDestination.Width / 2f + 50 - Font.MeasureString(SecondNumber).X / 2f, Main.RenderTargetDestination.Height * 0.025f), Color.White);
            spriteBatch.DrawString(Font, "Pontos: " + Points, new Vector2(Main.RenderTargetDestination.Width * 0.05f - Font.MeasureString(SecondNumber).X / 2f, Main.RenderTargetDestination.Height * 0.025f), Color.White);


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
                    spriteBatch.DrawString(Font, Numbers[i2].Number, new Vector2(Numbers[i2].X + 16 - Font.MeasureString(Numbers[i2].Number).X / 2f + BackgroundOffsetX, Numbers[i2].Y + 16 - Font.MeasureString(Numbers[i2].Number).Y / 3f + BackgroundOffsetY), Color.White);
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
        }
        public int GetNumberSpawn()
        {
            var spawn = Main.Random.Next(0, 18) * Grid;
            while (Math.Abs(OurSnake.X - spawn) < 4 || Math.Abs(OurSnake.Y - spawn) < 4)
            {
                spawn = Main.Random.Next(0, 18) * Grid;
            }
            return spawn;
        }

        public class NumberObject
        {
            public string Number;
            public int X;
            public int Y;
        }
        public class Snake
        {
            public int X = 160;
            public int Y = 160;
            public int DX = 32; //Grid
            public int DY = 0;
            public List<Vector2> Cells = new List<Vector2>();
            public int MaxCells = 4;
        }
    }
}