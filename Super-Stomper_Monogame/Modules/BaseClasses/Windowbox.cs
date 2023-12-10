 using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Super_Stomper_Monogame.Modules.BaseClasses
{
    internal class Windowbox
    {
        //scherm breedte
        private readonly int designedResolutionWidth;
        // scherm hoogte
        private readonly int designedResolutionHeight;
        private readonly Microsoft.Xna.Framework.Game game;
        private readonly Color clearColor;


        private RenderTarget2D _renderTarget;

        private Rectangle _renderScaleRectangle;

        public Windowbox(Microsoft.Xna.Framework.Game game, int designedResolutionWidth, int designedResolutionHeight)
        {
            this.game = game;
            this.designedResolutionWidth = designedResolutionWidth;
            this.designedResolutionHeight = designedResolutionHeight;

            clearColor = new Color(0xff181818);

            game.Window.ClientSizeChanged += (s, e) => SetDesignResolution();



        }

        private GameWindow Window => game.Window;
        private float DesignedResolutionAspectRatio =>
            designedResolutionWidth / (float)designedResolutionHeight;

        public int DesignedResolutionWidth => designedResolutionWidth;
        public int DesignedResolutionHeight => designedResolutionHeight;

        public Rectangle GetScaledRect() => new Rectangle(Point.Zero, (_renderScaleRectangle.Size.ToVector2() / (_renderScaleRectangle.Size.ToVector2() / new Vector2(designedResolutionWidth, designedResolutionHeight))).ToPoint());


        public void Draw()
        {

            game.GraphicsDevice.Clear(Color.SkyBlue);
        }



        public void SetDesignResolution()
        {
            _renderTarget = new RenderTarget2D(game.GraphicsDevice,
                    designedResolutionWidth, designedResolutionHeight,
                    false,
                    SurfaceFormat.Color, DepthFormat.None, 0,
                    RenderTargetUsage.DiscardContents);

            _renderScaleRectangle = GetScaleRectangle();

           
            Rectangle GetScaleRectangle()
            {
                var variance = 0.5;
                var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

                Rectangle scaleRectangle;

                if (actualAspectRatio <= DesignedResolutionAspectRatio)
                {
                    var presentHeight = (int)(Window.ClientBounds.Width / DesignedResolutionAspectRatio + variance);
                    var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;
                    scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
                }
                else
                {
                    var presentWidth = (int)(Window.ClientBounds.Height * DesignedResolutionAspectRatio + variance);
                    var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;
                    scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
                }

                return scaleRectangle;
            }
        }
    }
}
