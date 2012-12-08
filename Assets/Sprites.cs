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
        private static Texture2D bullet;
        private static Texture2D zombie;
        private static Texture2D boom;

        public static void LoadContent(ContentManager content)
        {
            ship = content.Load<Texture2D>("Sprites/ship");
            bullet = content.Load<Texture2D>("Sprites/bullet");
            zombie = content.Load<Texture2D>("Sprites/zombie");
            boom = content.Load<Texture2D>("Sprites/boom");
        }

        public static Texture2D Ship
        {
            get { return ship; }
        }

        public static Texture2D Bullet
        {
            get { return bullet; }
        }

        public static Texture2D Zombie
        {
            get { return zombie; }
        }

        public static Texture2D Boom
        {
            get { return boom; }
        }
    }
}
