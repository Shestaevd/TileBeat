using Godot;
using System;

namespace TileBeat.scripts.Managers.Beat
{
    public abstract record AbstractBeat
    {

        protected float _targetPosition;
        public float TargetPosition
        {
            get { return _targetPosition; }
        }
        protected float _currentPosition;
        public float CurrentPosition
        {
            get { return _targetPosition; }
        }

        public uint Index;

        public AbstractBeat(uint index, float targetTime)
        {
            Index = index;
            _targetPosition = targetTime;
        }

        public bool IsExpired()
        {
            return _currentPosition > _targetPosition;
        }

        public float UntilBeat() 
        {
            return _targetPosition - _currentPosition;
        }

        public void SetPosition(double position)
        {
            _currentPosition = (float) position;
        }
    }

    public record Beat(uint index, float targetPosition, Action OnBeat = null) : AbstractBeat(index, targetPosition);

}