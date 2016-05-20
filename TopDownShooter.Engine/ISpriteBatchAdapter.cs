namespace TopDownShooter.Engine
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines an interface for adapting <see cref="SpriteBatch" />.
    /// </summary>
    public interface ISpriteBatchAdapter
    {
        /// <summary>
        /// Begins a new sprite and text batch with the specified render state.
        /// </summary>
        /// <param name="sortMode">
        /// The drawing order for sprite and text drawing. <see cref="Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred" /> by default.
        /// </param>
        /// <param name="blendState">
        /// State of the blending. Uses Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend if null.
        /// </param>
        /// <param name="samplerState">
        /// State of the sampler. Uses Microsoft.Xna.Framework.Graphics.SamplerState.LinearClamp if null.
        /// </param>
        /// <param name="depthStencilState">
        /// State of the depth-stencil buffer. Uses Microsoft.Xna.Framework.Graphics.DepthStencilState.None if null.
        /// </param>
        /// <param name="rasterizerState">
        /// State of the rasterization. Uses Microsoft.Xna.Framework.Graphics.RasterizerState.CullCounterClockwise if null.
        /// </param>
        /// <param name="effect">
        /// A custom Microsoft.Xna.Framework.Graphics.Effect to override the default sprite effect. Uses default sprite effect if null.
        /// </param>
        /// <param name="transformMatrix">
        /// An optional matrix used to transform the sprite geometry. Uses Microsoft.Xna.Framework.Matrix.Identity if null.
        /// </param>
        void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = default(Matrix?));

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">The rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="effects">Modifications for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        void Draw(Texture2D texture, Vector2 position, Color color);

        void DrawString(SpriteFont font, string text, Vector2 position, Color color);

        /// <summary>
        /// Flushes all batched text and sprites to the screen.
        /// </summary>
        void End();
    }
}