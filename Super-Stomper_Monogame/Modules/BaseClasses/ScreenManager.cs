 using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Super_Stomper_Monogame.Modules.BaseClasses
{
    internal class ScreenManager
    {
        //scherm breedte
        private readonly int targetResolutionWidth;
        // scherm hoogte
        private readonly int targetResolutionHeight;
        private readonly Microsoft.Xna.Framework.Game game;
        private readonly Color clearColor;


        private RenderTarget2D _renderTarget;
        private Rectangle _renderScaleRectangle;
        private bool initilized;
        

        public ScreenManager(Microsoft.Xna.Framework.Game game, int targetResolutionWidth, int targetResolutionHeight)
        {
            this.game = game;
            this.targetResolutionWidth = targetResolutionWidth;
            this.targetResolutionHeight = targetResolutionHeight;
            clearColor = new Color(0xff181818);

            game.Window.ClientSizeChanged += (s, e) => SetDesignResolution();
        }

        private GameWindow Window => game.Window;
        private float DesignedResolutionAspectRatio =>
            targetResolutionWidth / (float)targetResolutionHeight;

        public int DesignedResolutionWidth => targetResolutionWidth;
        public int DesignedResolutionHeight => targetResolutionHeight;

        public void Draw(SpriteBatch spriteBatch,Action<SpriteBatch> renderAction,
            SpriteSortMode sortMode = SpriteSortMode.Deferred,
            BlendState blendState = null,
            SamplerState samplerState = null,
            DepthStencilState depthStencilState = null,
            RasterizerState rasterizerState = null,
            Effect effect = null,
            Matrix? transformMatrix = null)
        {
            if (!initilized)
            {
                SetDesignResolution();
                initilized = true;
            }

            game.GraphicsDevice.SetRenderTarget(_renderTarget);
            game.GraphicsDevice.Clear(Color.SkyBlue);

            
        }
        public Rectangle GetScaledRect() => new Rectangle(Point.Zero, (_renderScaleRectangle.Size.ToVector2() / (_renderScaleRectangle.Size.ToVector2() / new Vector2(targetResolutionWidth, targetResolutionHeight))).ToPoint());
        public Point GetCorrectMousePos() => ((Mouse.GetState().Position - _renderScaleRectangle.Location).ToVector2() / (_renderScaleRectangle.Size.ToVector2() / new Vector2(targetResolutionWidth, targetResolutionHeight))).ToPoint();


        public void Draw(
            SpriteBatch spriteBatch,
            Action<SpriteBatch> renderAction,
            Color clearColor,
            /* === SpriteBatch.Begin() parameters === */
            SpriteSortMode sortMode = SpriteSortMode.Deferred,
            BlendState blendState = null,
            SamplerState samplerState = null,
            DepthStencilState depthStencilState = null,
            RasterizerState rasterizerState = null,
            Effect effect = null,
            Matrix? transformMatrix = null)
        {
           

            // Draw on the graphics pad.
            game.GraphicsDevice.SetRenderTarget(_renderTarget);
            game.GraphicsDevice.Clear(Color.SkyBlue);

            spriteBatch.Begin(
                sortMode,
                blendState, samplerState,
                depthStencilState,
                rasterizerState,
                effect,
                transformMatrix);


            renderAction?.Invoke(spriteBatch);

            spriteBatch.End();

            // Display the contents of the graphics buffer window-wide.
            game.GraphicsDevice.SetRenderTarget(null);
            game.GraphicsDevice.Clear(ClearOptions.Target, clearColor, 1.0f, 0);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, samplerState: samplerState);
            spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);
            spriteBatch.End();
        }

        public void SetDesignResolution()
        {
            _renderTarget = new RenderTarget2D(game.GraphicsDevice,
                    targetResolutionWidth, targetResolutionHeight,
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
