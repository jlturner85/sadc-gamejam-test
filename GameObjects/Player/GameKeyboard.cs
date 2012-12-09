using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameJamTest.GameObjects.Player
{

    public class GameKeyboard
    {
        private GameKey up;
        private GameKey left;
        private GameKey down;
        private GameKey right;
        private GameKey fire;
        private GameKey back;

        private List<GameKey> keys;

        public GameKeyboard()
        {
            this.up = new GameKey(Keys.W);
            this.left = new GameKey(Keys.A);
            this.down = new GameKey(Keys.S);
            this.right = new GameKey(Keys.D);
            this.fire = new GameKey(Keys.Space);
            this.back = new GameKey(Keys.Escape);

            this.keys = new List<GameKey>();
            this.keys.Add(this.up);
            this.keys.Add(this.left);
            this.keys.Add(this.down);
            this.keys.Add(this.right);
            this.keys.Add(this.fire);
            this.keys.Add(this.back);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            foreach (GameKey key in this.keys)
            {
                if (keyboardState.IsKeyDown(key.Key))
                {
                    switch (key.KeyState)
                    {
                        case KeyState.UP:
                            key.KeyState = KeyState.PRESSED;
                            break;
                        case KeyState.PRESSED:
                        case KeyState.HELD:
                            key.KeyState = KeyState.HELD;
                            break;
                    }
                }
                else
                {
                    key.KeyState = KeyState.UP;
                }
            }
        }

        public GameKey Up
        {
            get { return this.up; }
        }

        public GameKey Left
        {
            get { return this.left; }
        }

        public GameKey Down
        {
            get { return this.down; }
        }

        public GameKey Right
        {
            get { return this.right; }
        }

        public GameKey Fire
        {
            get { return this.fire; }
        }

        public GameKey Back
        {
            get { return this.back; }
        }
    }

    public class GameKey
    {
        private Keys key;
        private KeyState keyState;

        public GameKey(Keys key)
        {
            this.Key = key;
            this.KeyState = KeyState.UP;
        }

        public Keys Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        public KeyState KeyState
        {
            get { return this.keyState; }
            set { this.keyState = value; }
        }

        public bool IsPressed()
        {
            return this.KeyState == KeyState.PRESSED;
        }

        public bool IsHeld()
        {
            return this.KeyState != KeyState.UP;
        }

    }

    public enum KeyState
    {
        UP, PRESSED, HELD
    }
}
