using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameJamTest.Screens;
using GameJamTest.Assets;
namespace GameJamTest.GameObjects
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ZombieExplosion : GameJamComponent
    {
        private int aliveTime;
        protected Animation explosionAnimation;
        public ZombieExplosion(Game game, GameScreen screen, Vector2 position)
            : base(game,screen,position)
        {
            // TODO: Construct any child components here
            this.aliveTime = 0;
            width = 45;
            height = 22;
            explosionAnimation = new Animation(this.Game.Content, "Sprites/cannonzombieboom", width, height, 5, 15);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            
            base.Initialize();
        }
        
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            explosionAnimation.Update(gameTime);
            this.aliveTime += 1;
            if (this.aliveTime > 75)
            {
                this.Destroy();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            explosionAnimation.Draw((this.Game as Game1).SpriteBatch, position, 0f, scale);
            base.Draw(gameTime);
        }
    }
}
