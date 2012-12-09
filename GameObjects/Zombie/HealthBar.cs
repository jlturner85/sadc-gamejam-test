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
    class HealthBar
    {

        private int hp;
        private int maxHp;

        public HealthBar(int maxHp)
        {
            this.maxHp = maxHp;
            this.hp = maxHp;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        private void DrawBar(SpriteBatch spriteBatch, int location, int missingPiece, int drawnPiece)
        {
        }

        public int Hp
        {
            get { return this.hp; }
            set { this.hp = value; }
        }
    }
}
