using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJamTest.GameObjects
{
    public class Layers
    {
        public static List<Layer> Values()
        {
            List<Layer> values = new List<Layer>();
            values.Add(Layer.ASTEROID);
            values.Add(Layer.BULLET);
            values.Add(Layer.ZOMBIE);
            values.Add(Layer.PLAYER);
            values.Add(Layer.FRONT);
            return values;
        }
    }

    public enum Layer
    {
        ASTEROID, BULLET, ZOMBIE, PLAYER, FRONT
    }
}
