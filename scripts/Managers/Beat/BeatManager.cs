using BeatSystem.scripts.BeatSystem.Domain.System;
using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using TileBeat.scripts.BeatSystem.BeatSystemGodot.Track;
using TileBeat.scripts.BeatSystem.BeatTrackingSystem.Domain.Utils;

namespace TileBeat.scripts.Managers.Beat
{
	public partial class BeatManager : AudioStreamPlayer2D, IAudioBeatSystem<GodotTrack>
	{

        private GodotTrack _track;
		private Texture2D _marker;
		private Texture2D _hitZone;
		private TextureRect _beatBox;
        private CanvasLayer _canvasLayer;
        private BeatQueue _beatQueue;
        private BeatDrawer _beatDrawer;
        private Queue<AbstractBeat> _queue;

        private Vector2 _beatPosition;
        private Vector2 _viewportCenter;

        private float _bottomOffset;
        private float _ySize;
        private float _accuracy;
        private float _interval;
        private uint _visibleBeats;

        public BeatManager(
            float bottomOffset,
            float ySize,
            CanvasLayer canvas, //	should have TextureRec named "BeatBox". 		
            GodotTrack track,
            Texture2D hitZone,
            Texture2D marker,
            Queue<AbstractBeat> queue,
            uint visibleBeats = 3,
            float accuracy = 0.5f //   from 0 to 1
        )
		{
            _queue = queue;
            _hitZone = hitZone;
            _track = track;
			_marker = marker;
            _accuracy = accuracy;
            _visibleBeats = visibleBeats == 0 ? 1 : visibleBeats;
            _interval = Utils.FindInterval(track.GetFullLength(), track.GetBpm());
            _canvasLayer = canvas;
            _bottomOffset = bottomOffset;
            _ySize = ySize;
            Stream = _track.audioStream;
        }

        public override void _Ready()
		{
            _beatDrawer = new BeatDrawer(_hitZone, _marker, _ySize, _visibleBeats, _accuracy, _bottomOffset);
            _beatQueue = new BeatQueue(_queue, _interval, _visibleBeats, _beatDrawer); // generate and move beat sprites

            AddChild(_beatDrawer);

            Subscribe(i => {
                if (i == 0)
                {
                    Play();
                    GD.Print("track started");
                }
            });
        }

        public void Subscribe(Action<uint> action)
        {
            _beatQueue.OnBeat += action;
        }

        public override void _Process(double delta)
        {
            float viewportXCenter = viewportCenter();
            float viewportYBottom = GetViewportRect().Size.Y;
            Vector2 beatBoxCenter = new Vector2(viewportXCenter, viewportYBottom - _bottomOffset);
            _beatQueue.Play(delta, viewportSizeX(), beatBoxCenter);
        }

        private float viewportSizeX() // can be const
        {
            return GetViewportRect().Size.X;
        }

        private float viewportCenter() // can be const
        {
            return viewportSizeX() * 0.5f;
        }

        private int currentInterval()
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
            return Utils.FindTargetBeat(_track.GetFullLength(), _interval);
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
            return _beatQueue.UntilNextInterval();
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
