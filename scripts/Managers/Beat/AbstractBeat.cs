using Godot;
using System;

namespace TileBeat.scripts.Managers.Beat
{
    public abstract class AbstractBeat
    {

        public Action OnBeat;

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
        //public float HowAccurate(float accuracy)
        //{
        //    if (accuracy > 1f || accuracy < 0f) throw new ArgumentException("accuracy must be greater then 0 and less then 1");
        //    float accuracyStartTime = Mathf.Lerp(0, _targetTime, accuracy);
        //    float accuracyTargetTime = _targetTime - accuracyStartTime;
        //    float oneP = accuracyTargetTime * 0.01f;
        //    float accuracyCurrentTime = _currentTime - accuracyStartTime;

        //    if (accuracyCurrentTime > 0)
        //    {
        //        return accuracyCurrentTime / oneP;
        //    }
        //    else
        //    {
        //        return 0f;
        //    }
        //}

        public float UntilBeat() 
        {
            return _targetTime - _currentTime;
        }
        public virtual void Create(float targetTime, float currentInterval) 
        {
            _currentTime = currentInterval;
            _targetTime = targetTime;
        }
        public void Move(double delta)
        {
            _currentTime += (float)delta;
        }
        public EmptyBeat ToEmptyBeat()
        {
            EmptyBeat eb = new EmptyBeat(Index);
            eb._currentTime = _currentTime;
            eb._targetTime = _targetTime;
            eb.OnBeat = OnBeat;
            return eb;
        }
    }

    public class EmptyBeat : AbstractBeat
    {
        public EmptyBeat(uint index) : base(index) { }
    }

    public class Beat : AbstractBeat 
    {
        public Beat(uint index) : base(index) { }

        public Tuple<Vector2, Vector2> GetPosition(Vector2 center, float viewportX)
        {
            Vector2 start = new Vector2(center.X - viewportX / 2, center.Y);
            Vector2 startTwin = new Vector2(center.X + viewportX / 2, center.Y);
            return Tuple.Create(start.Lerp(center, _currentTime / _targetTime), startTwin.Lerp(center, _currentTime / _targetTime));
        }
    }
}
