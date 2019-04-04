using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BestGameEver.Droid
{
    public class Buffer
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly RenderTarget2D _buffer;
        private Rectangle _targetRectangle;

        public Buffer(GraphicsDevice graphicsDevice)
        {
            this._graphicsDevice = graphicsDevice;

            this._buffer = new RenderTarget2D(graphicsDevice, 800, 600, false, SurfaceFormat.Color, DepthFormat.Depth24, 0, RenderTargetUsage.DiscardContents);

            this._targetRectangle = this.CalculateTargetRectangle(this._graphicsDevice.Viewport.Width, this._graphicsDevice.Viewport.Height, 800, 600);
        }

        public void InitBuffer()
        {
            this._graphicsDevice.SetRenderTarget(this._buffer);
        }

        public void DrawBufferToScreen(SpriteBatch spriteBatch)
        {
            this._graphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin();
            spriteBatch.Draw(this._buffer, this._targetRectangle, Color.White);
            spriteBatch.End();
        }

        private Rectangle CalculateTargetRectangle(int targetWidth, int targetHeight, int gameWidth, int gameHeight)
        {
            var gameAspectRatio = gameWidth / (float)gameHeight;
            var targetAspectRatio = targetWidth / (float)targetHeight;

            if (Math.Abs(gameAspectRatio - targetAspectRatio) < 0.01)
            {
                return new Rectangle(0, 0, targetWidth, targetHeight);
            }
            if (targetAspectRatio < gameAspectRatio)
            {
                var x = gameHeight / (float)gameWidth;
                var newHeight = (int)(x * targetWidth);
                return new Rectangle(0, (targetHeight - newHeight) / 2, targetWidth, newHeight);
            }
            if (targetAspectRatio > gameAspectRatio)
            {
                var x = gameWidth / (float)gameHeight;
                var newWidth = (int)(x * targetHeight);
                return new Rectangle((targetWidth - newWidth) / 2, 0, newWidth, targetHeight);
            }

            return new Rectangle(0, 0, targetWidth, (int)(targetHeight * targetAspectRatio));
        }

        public void Resize()
        {
            this._targetRectangle = this.CalculateTargetRectangle(this._graphicsDevice.Viewport.Width, this._graphicsDevice.Viewport.Height, 800, 600);

        }
    }
}