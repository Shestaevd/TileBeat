using Godot;
using System;

namespace TileBeat.scripts.Managers.Beat
{
    public abstract class AbstractBeat
    {

        public event Action OnBeat;

        protected float _targetTime;
        protected float _currentTime = 0;

        public uint Index;

        public AbstractBeat(uint index)
        {
            Index = index;
        }

        public virtual void Move(double delta, Vector2 center)
        {
            _currentTime += (float) delta;
        }
        public bool IsExpired()
        {
            return _currentTime > _targetTime;
        }
        public float UntilBeat() 
        {
            return _targetTime - _currentTime;
        }
        public virtual void Clear() { }
        public virtual void Spawn(Node parent, float targetTime, float viewportX, float spriteSizeY) { }
    }

    public class EmptyBeat : AbstractBeat
    {
        public EmptyBeat(float targetTime, uint index) : base(index)
        {
            _targetTime = targetTime;
        }

    }

    public class Beat : AbstractBeat 
    {
        private TextureRect _sprite;
        private TextureRect _spriteTwin;
        private Vector2 _center;
        private float _spriteSizeY;
        private float _viewportX;
        public Beat(Texture2D sprite, uint index) : base(index)
        {
            TextureRect spriteRect = new TextureRect();
            spriteRect.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
            spriteRect.Texture = sprite;
            
            _sprite = (TextureRect) spriteRect.Duplicate();
            _spriteTwin = (TextureRect) spriteRect.Duplicate();
            _spriteTwin.FlipV = true;
        }



        public override void Move(double delta, Vector2 center)
        {
            _sprite.Size = new Vector2(_sprite.Texture.GetWidth(), _spriteSizeY);
            _spriteTwin.Size = new Vector2(_sprite.Texture.GetWidth(), _spriteSizeY);
            Vector2 start = new Vector2(center.X - _viewportX / 2, center.Y);
            Vector2 startTwin = new Vector2(center.X + _viewportX / 2, center.Y);
            _currentTime += (float) delta;
            _sprite.Position = start.Lerp(center, _currentTime / _targetTime);
            _spriteTwin.Position = startTwin.Lerp(center, _currentTime / _targetTime);
        }

        public override void Clear()
        {
            if (IsExpired())
            {
                _sprite.QueueFree();
                _spriteTwin.QueueFree();
            }
        }

        public override void Spawn(Node parent, float targetTime, float viewportX, float spriteSizeY)
        {
            _targetTime = targetTime;
            _spriteSizeY = spriteSizeY;
            _viewportX = viewportX;
            parent.AddChild(_sprite);
            parent.AddChild(_spriteTwin);
        }
    }
}
