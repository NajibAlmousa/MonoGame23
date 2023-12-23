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
        private readonly int designedResolutionWidth;
        // scherm hoogte
        private readonly int designedResolutionHeight;
        private readonly Microsoft.Xna.Framework.Game game;
        private readonly Color clearColor;


        private RenderTarget2D _renderTarget;

        private Rectangle _renderScaleRectangle;
        private bool _initilized = false;

        public ScreenManager(Microsoft.Xna.Framework.Game game, int designedResolutionWidth, int designedResolutionHeight)
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

        public void Draw(SpriteBatch spriteBatch,
            Action<SpriteBatch> renderAction,
            /* === SpriteBatch.Begin() parameters === */
            SpriteSortMode sortMode = SpriteSortMode.Deferred,
            BlendState blendState = null,
            SamplerState samplerState = null,
            DepthStencilState depthStencilState = null,
            RasterizerState rasterizerState = null,
            Effect effect = null,
            Matrix? transformMatrix = null)
        {
            Draw(
                spriteBatch,
                renderAction,
                clearColor,
                sortMode,
                blendState,
                samplerState,
                depthStencilState,
                rasterizerState,
                effect,
                transformMatrix);

        }
        public Rectangle GetScaledRect() => new Rectangle(Point.Zero, (_renderScaleRectangle.Size.ToVector2() / (_renderScaleRectangle.Size.ToVector2() / new Vector2(designedResolutionWidth, designedResolutionHeight))).ToPoint());
        public Point GetCorrectMousePos() => ((Mouse.GetState().Position - _renderScaleRectangle.Location).ToVector2() / (_renderScaleRectangle.Size.ToVector2() / new Vector2(designedResolutionWidth, designedResolutionHeight))).ToPoint();


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
            if (!_initilized)
            {
                SetDesignResolution();
                _initilized = !_initilized;
            }

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
