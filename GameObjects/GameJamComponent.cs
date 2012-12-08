using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameJamTest.GameObjects
{
    public class GameJamComponent : DrawableGameComponent
    {
        private Layer layer;

        public GameJamComponent(Game game)
            : base(game)
        {
        }

        public Layer Layer
        {
            get { return this.layer; }
            set { this.layer = value; }
        }
    }
}
