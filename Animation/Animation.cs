using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace GameJamTest.Animation
{
    public class Animation
    {
        int index;

        Texture2D texture;

        public int FrameHeight
        {
            get;
            private set;
        }

        public int FrameWidth
        {
            get;
            private set;
        }

        int framesPerRow;

        public int NumberOfFrames
        {
            get;
            private set;
        }

        int fps;
        public string FPSDisplay
        {
            get;
            private set;
        }

        float timer;

        float delayTimer;

        bool repeating;

        /// <summary>
        /// Generates a new Animation Object
        /// </summary>
        /// <param name="content">Content object so we can call .Load</param>
        /// <param name="assert">The assert name of the texture/sprite sheet</param>
        /// <param name="frameWidth">The width of a single frame</param>
        /// <param name="frameHeight">The height of a single frame</param>
        /// <param name="numberOfFrames">The total number of frames in this animation</param>
        /// <param name="framesPerSecond">The speed you want the animation to last</param>

        public Animation(ContentManager content, string assert, int frameWidth, int frameHeight, int numberOfFrames, int framesPerSecond)
        {
            texture = content.Load<Texture2D>(assert);
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            framesPerRow = texture.Width / frameWidth;
            fps = framesPerSecond;
            FPSDisplay = assert + " FPS:    " + fps.ToString();
            NumberOfFrames = numberOfFrames;
            repeating = false;
        }

        public void DisableRepeating()
        {
            repeating = true;
        }

        public void DisableRepeating()
        {
            repeating = false;
        }

        public void Update(GameTime gameTime)
        {
            if (timer > (1.0f / (float)fps))
            {
                if (index != (NumberOfFrames - 1))
                {
                    index++;
                }
                else if (repeating)
                {
                    index = 0;
                }

                timer = 0;
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void RestartAnimation()
        {
            index = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int rowNumber = index / framesPerRow;
            spriteBatch.Draw(texture, position, new Rectangle((index - (rowNumber * framesPerRow)) * FrameWidth,
                rowNumber * FrameHeight, FrameWidth, FrameHeight), Color.White);
        }
                
    }
}
