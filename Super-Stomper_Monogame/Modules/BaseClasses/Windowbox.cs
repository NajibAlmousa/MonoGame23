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

            
        }

        private GameWindow Window => game.Window;
        
        public int DesignedResolutionWidth => designedResolutionWidth;
        public int DesignedResolutionHeight => designedResolutionHeight;

        public Rectangle GetScaledRect() => new Rectangle(Point.Zero, (_renderScaleRectangle.Size.ToVector2() / (_renderScaleRectangle.Size.ToVector2() / new Vector2(designedResolutionWidth, designedResolutionHeight))).ToPoint());
    }
}
