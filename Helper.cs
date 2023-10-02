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
using Microsoft.Xna.Framework.Audio;
using static System.Net.Mime.MediaTypeNames;

namespace Luhe
{
    public  class Helper
    {
        public static IEnumerable<string> SplitString(string text, double rectangleWidth, SpriteFont font)
        {
            var words = text.Split(' ');
            string buffer = string.Empty;

            foreach (var word in words)
            {
                var newBuffer = buffer + " " + word;
                if (word == words[0])
                    newBuffer = word;
                else
                    newBuffer = buffer + " " + word;

                Vector2 FontMeasurements = font.MeasureString(newBuffer);

                if (FontMeasurements.X >= rectangleWidth)
                {
                    yield return buffer;
                    buffer = word;
                }
                else
                {
                    buffer = newBuffer;
                }
            }
            yield return buffer;
        }
    }
}