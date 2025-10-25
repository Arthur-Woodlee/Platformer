using Microsoft.Xna.Framework.Graphics;
using Platformer.PlatformerSceneEngine;
using Platformer.Positioning;


namespace Platformer.PlatformerElementSelector
{
    public abstract class ElementSelectorSceneObject : ISceneObject, IRectangleSceneObject
    {
        private IElementSelector _elementSelector;
        private Texture2D _texture;
        private PlatformerRectangle _position;
        public ElementSelectorSceneObject(IElementSelector elementSelector, PlatformerRectangle initialPosition)
        {
            this._elementSelector = elementSelector;
            this._position = initialPosition;
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
        public abstract void Draw();
        public PlatformerRectangle GetPosition()
        {
            return new PlatformerRectangle(_position.X, _position.Y, _position.Width, _position.Height);
        }
    }
}