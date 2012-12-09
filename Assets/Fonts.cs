using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameJamTest.Assets
{
    public class Fonts
    {
        private static SpriteFont titleFont;

        public static void LoadContent(ContentManager content)
        {
            titleFont = content.Load<SpriteFont>("Fonts/titlefont");
        }

        public static SpriteFont TitleFont
        {
            get { return titleFont; }
        }
    }
}
