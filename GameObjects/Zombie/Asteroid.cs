using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJamTest.Assets;
using GameJamTest.Screens;

namespace GameJamTest.GameObjects.Zombie
{
    class Asteroid : GameJamComponent
    {
        Vector2 velocity;

        public Asteroid(Game game, GameScreen screen, Vector2 position, Vector2 velocity)
            : base(game, screen, position, velocity)
        {
            this.Layer = Layer.ASTEROID;
            //this.Sprite = Sprites.Asteroid;
        }
    }
}
