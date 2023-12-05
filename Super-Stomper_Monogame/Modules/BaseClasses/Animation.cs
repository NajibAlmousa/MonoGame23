using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Super_Stomper_Monogame.Modules.BaseClasses
{
    internal class Animation
    {
        public bool looping;
        public bool finished { private set; get; }
        public int currentFrame { private set; get; }
        public bool started { private set; get; }

        private Texture2D texture;
       
        private int[] frames;
        private Point frameSize;

        public Animation(Texture2D texture, float timeBetweenFrames, bool looping, int[] frames, Point frameSize)
        {
            this.texture = texture;
           
            this.looping = looping;
            this.frames = frames;
            this.frameSize = frameSize;

            finished = false;
            started = false;
            currentFrame = 0;
        }

        public void Update(float deltaTime)
        {
            if (currentFrame >= frames.Length)
            {

                currentFrame = looping ? 0 : currentFrame - 1;
            }


        }

        
        public Rectangle GetCurrentFrame() => new Rectangle((frames[currentFrame] * frameSize.X) % texture.Width, ((frames[currentFrame] * frameSize.X) / texture.Width) * frameSize.Y, frameSize.X, frameSize.Y);


    }
}
