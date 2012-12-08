using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameJamTest.Util
{
    static class AudioManager
    {
        

        public static void playSoundEffect(SoundEffect effect)
        {
            effect.Play();
        }

        public static void playMusic(Song newMusic)
        {
            
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(newMusic);
            
        }

        public static void stopMusic()
        {
            MediaPlayer.Stop();
        }
    }
}
