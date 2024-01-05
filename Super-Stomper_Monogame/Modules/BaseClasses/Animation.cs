﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Stomper_Monogame.Modules.BaseClasses
{
    internal class Animation
    {
        public bool looping;
        public bool finished { private set; get; }
        public bool started { private set; get; }
        public int currentFrame { private set; get; }
        private int[] frames;

        private Texture2D texture;
        private Point frameSize;

        private float timeBetweenFrames;
        private float timer;

        public Animation(Texture2D texture, float timeBetweenFrames, bool looping, int[] frames, Point frameSize)
        {
            this.texture = texture;
            this.timeBetweenFrames = timeBetweenFrames;
            this.looping = looping;
            this.frames = frames;
            this.frameSize = frameSize;

            finished = false;
            started = false;
            currentFrame = 0;
        }

        public void Update(float deltaTime)
        {
            //wnr animaatie klaat we hoeven niks te updaten
            if (finished)
                return;

            if (frames.Length <= 1)
                return;

            timer += deltaTime;
            int passedAFrame = (int)(timer / timeBetweenFrames);
            timer -= passedAFrame * timeBetweenFrames;
            currentFrame += passedAFrame;

            if (currentFrame >= frames.Length)
            {
                finished = !looping;
                started = !finished;
                currentFrame = looping ? 0 : currentFrame - 1;
            }


        }


        public void Reset(bool start = false)
        {
            currentFrame = 0;
            finished = false;
            started = start;
            timer = 0;
        }

        public Rectangle GetCurrentFrame() => new Rectangle((frames[currentFrame] * frameSize.X) % texture.Width, ((frames[currentFrame] * frameSize.X) / texture.Width) * frameSize.Y, frameSize.X, frameSize.Y);


    }
}
