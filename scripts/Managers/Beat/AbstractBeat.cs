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

        public virtual void Move(double delta)
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
        public virtual void Spawn(Node parent, float targetTime, float viewportX, Vector2 center, float spriteSizeY) { }
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
            spriteRect.Texture = sprite;
            
            _sprite = (TextureRect) spriteRect.Duplicate();
            _spriteTwin = (TextureRect) spriteRect.Duplicate();
            _spriteTwin.FlipV = true;
            
        }



        public override void Move(double delta)
        {
            _sprite.Size = new Vector2(_sprite.Size.X, _spriteSizeY);
            _spriteTwin.Size = new Vector2(_sprite.Size.X, _spriteSizeY);
            Vector2 start = new Vector2(_center.X - _viewportX / 2, _center.Y);
            Vector2 startTwin = new Vector2(_center.X + _viewportX / 2, _center.Y);
            _currentTime += (float) delta;
            _sprite.Position = start.Lerp(_center, _currentTime / _targetTime);
            _spriteTwin.Position = startTwin.Lerp(_center, _currentTime / _targetTime);
        }

        public override void Clear()
        {
            if (IsExpired())
            {
                _sprite.QueueFree();
                _spriteTwin.QueueFree();
            }
        }

        public override void Spawn(Node parent, float targetTime, float viewportX, Vector2 center, float spriteSizeY)
        {
            _targetTime = targetTime;
            _center = center;
            _spriteSizeY = spriteSizeY;
            _viewportX = viewportX;
            parent.AddChild(_sprite);
            parent.AddChild(_spriteTwin);
        }
    }
}
