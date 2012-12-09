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
        //public static Texture2D skull;
        public static Texture2D lifebar;
        public static Texture2D lifebarBg;
        
        public static void LoadContent(ContentManager content)
        {
            //skull = content.Load<Texture2D>("Sprites/skull");
            lifebar = content.Load<Texture2D>("Sprites/lifebar");
            lifebarBg = content.Load<Texture2D>("Sprites/lifebarbackground");
        }
    }
}
