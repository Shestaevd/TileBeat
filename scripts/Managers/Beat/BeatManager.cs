using BeatSystem.scripts.BeatSystem.Domain.System;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using TileBeat.scripts.BeatSystem.BeatSystemGodot.Track;
using TileBeat.scripts.BeatSystem.BeatTrackingSystem.Domain.Utils;

namespace TileBeat.scripts.Managers.Beat
{

	public partial class BeatManager : AudioStreamPlayer2D, IAudioBeatSystem<GodotTrack>
	{
        private event Action<uint> OnBeat;

        private BeatDrawer _beatDrawer;

        private Vector2 _beatPosition;
        private Vector2 _viewportCenter;
        private LinkedList<AbstractBeat> _inPlay = new LinkedList<AbstractBeat>();
        private Queue<AbstractBeat> _queue;

        private int _lastInterval = 0;
        private float _bottomOffset;
        private float _ySize;
        private float _interval;
        private uint _visibleBeats;

        AudioStreamPlayer _player;
        public BeatManager(
            float bottomOffset,
            float ySize,
            GodotTrack track,
            Texture2D hitZone,
            Texture2D marker,
            Queue<AbstractBeat> queue,
            AudioStreamPlayer test,
            uint visibleBeats = 3
            
        )
		{
            _queue = queue;
           _player = test;
            _visibleBeats = visibleBeats == 0 ? 1 : visibleBeats > 10 ? 10 : visibleBeats;
            _interval = Utils.FindInterval(track.GetFullLength(), track.GetBpm());
            _bottomOffset = bottomOffset;
            _ySize = ySize;
            Stream = track.audioStream;

            _beatDrawer = new BeatDrawer(hitZone, marker, _ySize, _visibleBeats, _bottomOffset);
        }


        public override void _Ready()
		{
            AddChild(_beatDrawer);

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
            float viewportXCenter = viewportCenter();
            float viewportYBottom = GetViewportRect().Size.Y;
            Vector2 beatBoxCenter = new Vector2(viewportXCenter, viewportYBottom - _bottomOffset);
            List<Vector2> positions = new List<Vector2>();

            foreach (AbstractBeat aBeat in _inPlay)
            {
                aBeat.Move(delta);

                if (aBeat is Beat beat)
                {
                    Tuple<Vector2, Vector2> newPos = beat.GetPosition(beatBoxCenter, viewportSizeX());
                    positions.Add(newPos.Item1);
                    positions.Add(newPos.Item2);
                }
            }

            _beatDrawer.UpdateBeatPositions(positions);

            AbstractBeat currentBeat = _inPlay.FirstOrDefault();
            if (currentBeat != null && currentBeat.IsExpired())
            {
                OnBeat?.Invoke(currentBeat.Index);
                currentBeat.OnBeat?.Invoke();
                _inPlay.RemoveFirst();
            }

            int current = CurrentInterval();
            if (_lastInterval != current)
            {
                _lastInterval = current;
                _queue.TryDequeue(out AbstractBeat aBeat);
                aBeat.Create(_interval * _visibleBeats, _interval - UntilNextBeat());
                _inPlay.AddLast(aBeat);
            }

        }

        public bool IsInTargetBeat()
        {
            return InTargetOfBeat() != 0;
        }

        public float UseNextBeat()
        {
            AbstractBeat currentBeat = _inPlay.FirstOrDefault();
            if (currentBeat != null && currentBeat is Beat)
            {
                _inPlay.First.ValueRef = currentBeat.ToEmptyBeat();
                return InTargetOfBeat();
            }
            return 0f;
        }

        private float viewportSizeX()
        {
            return GetViewportRect().Size.X;
        }

        private float viewportCenter()
        {
            return viewportSizeX() * 0.5f;
        }

        public int CurrentInterval()
        {
            return (int)(GetPlaybackPosition() / _interval);
        }

        // ------------------------------------------------------------------------------------------------

        public float GetInterval()
        {
            return _interval;
        }

        public float InTargetOfBeat()
        {
            float oneP = _interval * 0.01f;
            return GetPlaybackPosition() % _interval / oneP;
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
            return _interval - (GetPlaybackPosition() % _interval); ;
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

        public void Play()
        {
            base.Play();
        }
    }
}
