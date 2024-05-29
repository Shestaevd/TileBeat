using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TileBeat.scripts.Managers.Beat
{

    public partial class BeatManager : AudioStreamPlayer
	{
        private event Action<uint> OnBeat;

        private LinkedList<AbstractBeat> _beats = new LinkedList<AbstractBeat>();
        private AbstractBeat _prevBeat = null;

        public static BeatManager Spawn(GodotTrack godotTrack, Node parent)
        {
            BeatManager bm = new BeatManager(godotTrack);
            bm.Name = "BeatManager";
            parent.AddChild(bm);
            return bm;
        }

        public LinkedList<AbstractBeat> Beats
        {
            get { return _beats; }
        }

        public BeatManager(GodotTrack track)
		{
            _beats = new LinkedList<AbstractBeat>(track.beats.OrderBy(a => a.TargetPosition).ToList());
            Stream = track.audioStream;
        }

        public void Subscribe(Action<uint> action)
        {
            OnBeat += action;
        }

        public void SubscribeOnNext(Action<uint> action)
        {
            uint nextIndex = CurrentBeatIndex();
            OnBeat += i => { if (i == nextIndex + 1) action(i); };
        }

        public override void _Ready()
        {
            Stop();
        }

        public override void _Process(double delta)
        {

            foreach (AbstractBeat abstarctBeat in _beats)
            {
                abstarctBeat.SetPosition(GetPlaybackPosition());
            }
            
            AbstractBeat currentBeat = _beats.FirstOrDefault();
            if (currentBeat != null && currentBeat.IsExpired() && currentBeat is Beat beat)
            {
                OnBeat?.Invoke(beat.Index);
                beat.OnBeat?.Invoke();
                _prevBeat = currentBeat;
                _beats.RemoveFirst();
            }
        }

        public uint CurrentBeatIndex()
        {
            AbstractBeat currentBeat = _beats.FirstOrDefault();
            if (currentBeat != null)
            {
                return currentBeat.Index;
            }
            return uint.MaxValue;
        }

        public float UntilNextBeat()
        {
            AbstractBeat currentBeat = _beats.FirstOrDefault();
            if (currentBeat != null)
            {
                return currentBeat.UntilBeat();
            }
            return float.MaxValue;
        }

        public float FromLastBeat()
        {
            if (_prevBeat == null)
            {
                return float.MaxValue;
            }
            else
            {
                return GetPlaybackPosition() - _prevBeat.TargetPosition;
            }
        }

        public float InBeatRangePrecision(float precision)
        {
            float until = UntilNextBeat();
            float from = FromLastBeat();
            float currentBeatPosition = until > from ? from : until;
            
            if (currentBeatPosition > precision) 
                return 0f; 
            else
                return currentBeatPosition / (precision / 100f);

        }

        public void Pause()
        {
            Stop();
        }

        public void Reset()
        {
            Stop();
            Seek(0);
        }
    }
}
