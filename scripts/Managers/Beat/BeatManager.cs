using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using TileBeat.scripts.BeatSystem.BeatSystemGodot.Track;

namespace TileBeat.scripts.Managers.Beat
{

	public partial class BeatManager : AudioStreamPlayer
	{
        private event Action<uint> OnBeat;
        private LinkedList<AbstractBeat> _beats;

        public LinkedList<AbstractBeat> Beats
        {
            get { return _beats; }
        }

        public BeatManager(GodotTrack track, LinkedList<AbstractBeat> beats)
		{
            _beats = new LinkedList<AbstractBeat>(beats.OrderBy(a => a.TargetDelta).ToList());
            Stream = track.audioStream;
           
        }


        public override void _Ready()
		{
            Subscribe(i => { 
                if (i == 0)
                {
                    GD.Print("track started");
                }
            });
        }

        public void Subscribe(Action<uint> action)
        {
            OnBeat += action;
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
                GD.Print($"Beat expired {currentBeat.CurrentDelta}");
                OnBeat?.Invoke(beat.Index);
                beat.OnBeat?.Invoke();
                _beats.RemoveFirst();
            }
        }

        public uint CurrentBeat()
        {
            AbstractBeat currentBeat = _beats.FirstOrDefault();
            if (currentBeat != null)
            {
                return currentBeat.Index;
            }
            return uint.MaxValue;
        }

        public void SetPlaybackPosition(long position)
        {
            Seek(position);
        }

        public void SetVolume(float volume)
        {
            VolumeDb = Mathf.Lerp(-80f, 24f, volume);
        }

        public float GetVolume()
        {
            return VolumeDb;
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

        public void Pause()
        {
            Stop();
        }

        public void Reset()
        {
            Stop();
            SetPlaybackPosition(0);
        }
    }
}
