using Godot;
using System;

namespace TileBeat.scripts.Managers.Beat
{
    public abstract record AbstractBeat
    {

        protected float _targetDelta;
        public float TargetDelta
        {
            get { return _targetDelta; }
        }
        protected float _currentDelta;
        public float CurrentDelta
        {
            get { return _targetDelta; }
        }

        public uint Index;

        public AbstractBeat(uint index, float targetTime)
        {
            Index = index;
            _targetDelta = targetTime;
        }

        public bool IsExpired()
        {
            return _currentDelta > _targetDelta;
        }

        public float UntilBeat() 
        {
            return _targetDelta - _currentDelta;
        }

        public void SetPosition(double delta)
        {
            _currentDelta = (float) delta;
        }
    }

    public record EmptyBeat(uint index, float targetDelta) : AbstractBeat(index, targetDelta);

    public record Beat(uint index, float targetDelta, Action OnBeat = null) : AbstractBeat(index, targetDelta);

}



//public Tuple<Vector2, Vector2> GetPosition(Vector2 center, float viewportX)
//{
//    Vector2 start = new Vector2(center.X - viewportX / 2, center.Y);
//    Vector2 startTwin = new Vector2(center.X + viewportX / 2, center.Y);
//    return Tuple.Create(start.Lerp(center, _currentDelta / _targetDelta), startTwin.Lerp(center, _currentDelta / _targetDelta));
//}

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