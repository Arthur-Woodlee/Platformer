using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Platformer.PlatformerSceneEngine;
using Platformer.Positioning;


namespace Platformer.PlatformerElementSelector
{
    public class ElementSelectorSceneObject : ISceneObject, IRectangleSceneObject
    {
        private IElementSelector _elementSelector;
        private Texture2D _texture;
        private PlatformerRectangle _position;
        protected DrawConfig _drawConfig;
        private SpriteBatch _spriteBatch;
        public ElementSelectorSceneObject(IElementSelector elementSelector, PlatformerRectangle initialPosition, DrawConfig drawConfig, SpriteBatch spriteBatch)
        {
            this._elementSelector = elementSelector ?? throw new System.ArgumentNullException(nameof(elementSelector));
            this._position = initialPosition ?? throw new System.ArgumentNullException(nameof(initialPosition));
            this._drawConfig = drawConfig ?? throw new System.ArgumentNullException(nameof(drawConfig));
            this._spriteBatch = spriteBatch ?? throw new System.ArgumentNullException(nameof(spriteBatch));
        }
        public void Update(ushort updateCycleLength, ushort cycleNumber)
        {
            Element sequenceElement = _elementSelector.SelectElement(updateCycleLength, cycleNumber);
            if (sequenceElement != null)
            {
                this._texture = sequenceElement.Texture != null ? sequenceElement.Texture : this._texture;
                this._position = sequenceElement.Position != null ? sequenceElement.Position : this._position;
            }
        }
        public void Draw()
        {
            if (_texture == null || _drawConfig == null)
                return;

            var destination = new Rectangle(_position.X, _position.Y, _position.Width, _position.Height);
            var drawingColor = _drawConfig.Color;

            bool sourceRectangle_color_rotation_origin_effects_layerDepth =
                _drawConfig.Rotation.HasValue &&
                _drawConfig.Origin.HasValue &&
                _drawConfig.Effects.HasValue &&
                _drawConfig.LayerDepth.HasValue;

            if (sourceRectangle_color_rotation_origin_effects_layerDepth)
            {
                _spriteBatch.Draw(
                    _texture,
                    destination,
                    _drawConfig.SourceRectangle,
                    drawingColor,
                    _drawConfig.Rotation.Value,
                    _drawConfig.Origin.Value,
                    _drawConfig.Effects.Value,
                    _drawConfig.LayerDepth.Value
                );
                return;
            }

            bool sourceRectangle_color =
                _drawConfig.SourceRectangle.HasValue &&
                !_drawConfig.Rotation.HasValue &&
                !_drawConfig.Origin.HasValue &&
                !_drawConfig.Effects.HasValue &&
                !_drawConfig.LayerDepth.HasValue;

            if (sourceRectangle_color)
            {
                _spriteBatch.Draw(_texture, destination, _drawConfig.SourceRectangle, drawingColor);
                return;
            }

            _spriteBatch.Draw(_texture, destination, drawingColor);
        }
        public PlatformerRectangle GetPosition()
        {
            return new PlatformerRectangle(_position.X, _position.Y, _position.Width, _position.Height);
        }
    }
}