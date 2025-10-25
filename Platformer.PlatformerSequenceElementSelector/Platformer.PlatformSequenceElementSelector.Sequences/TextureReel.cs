using Microsoft.Xna.Framework.Graphics;

namespace Platformer.PlatformerSequenceElementSelector.Sequences
{
    public class TextureReel
    {
        private Texture2D[] _textureReel;
        private int _currentTexture;
        public TextureReel(Texture2D[] textureReel)
        {
            _textureReel = textureReel;
            _currentTexture = 0;
        }
        public void ResetReel()
        {
            _currentTexture = 0;
        }
        public Texture2D GetTexture()
        {
            Texture2D next = _textureReel[_currentTexture];
            if (_currentTexture == _textureReel.Length - 1)
            {
                _currentTexture = 0;
            }
            else
            {
                _currentTexture++;
            }
            return next;
        }
        public Texture2D Peek()
        {
            return _textureReel[_currentTexture];
        }
    }
}