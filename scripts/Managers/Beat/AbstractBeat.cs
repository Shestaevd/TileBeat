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

        public bool IsExpired()
        {
            return _currentTime > _targetTime;
        }
        public float UntilBeat() 
        {
            return _targetTime - _currentTime;
        }
        public virtual void Spawn(float targetTime) 
        {
            _targetTime = targetTime;
        }
        public void Move(double delta)
        {
            _currentTime += (float)delta;
        }
    }

    public class EmptyBeat : AbstractBeat
    {
        public EmptyBeat(uint index) : base(index) { }
    }

    public class Beat : AbstractBeat 
    {
        private float _viewportX;
        public Beat(uint index) : base(index) { }

        public Tuple<Vector2, Vector2> GetPosition(Vector2 center, float viewportX)
        {
            Vector2 start = new Vector2(center.X - viewportX / 2, center.Y);
            Vector2 startTwin = new Vector2(center.X + viewportX / 2, center.Y);
            return Tuple.Create(start.Lerp(center, _currentTime / _targetTime), startTwin.Lerp(center, _currentTime / _targetTime));
        }
    }
}
