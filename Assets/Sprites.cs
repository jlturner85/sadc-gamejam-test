using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameJamTest.Assets
{
    class Sprites
    {
        private static Texture2D ship;

        public static void LoadContent(ContentManager content)
        {
            ship = content.Load<Texture2D>("Sprites/ship");
        }

        public static Texture2D Ship
        {
            get { return ship; }
        }
    }
}
