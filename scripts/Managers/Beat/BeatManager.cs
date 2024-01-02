using BeatSystem.scripts.BeatSystem.Domain.System;
using Godot;
using System;
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
        private float _accuracy;
		private uint _visibleBeats;
        private float _interval;
        private BeatQueue _beatQueue;
        private Vector2 _beatPosition;
        private Vector2 _viewportCenter;

        private Vector2 _beatBoxCenter;
        private float _bottomOffset;
        private float _ySize;

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

            _beatQueue = new BeatQueue(queue, _interval, visibleBeats, canvas); // generate and move beat sprites

            Subscribe(i => { 
                if (i == 0)
                { 
                    Play();
                    GD.Print("track started"); 
                } 
            });
        }

        public override void _Ready()
		{

            float viewportXCenter = GetViewportRect().Size.X * 0.5f;
            float viewportYBottom = GetViewportRect().Size.Y;
            _beatBoxCenter = new Vector2(viewportXCenter, viewportYBottom - _bottomOffset);

            float cameraX = viewportSizeX();
            float beatBoxXSize = cameraX / _visibleBeats * _accuracy;

            TextureRect bb = new TextureRect();

            bb.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
            bb.StretchMode = TextureRect.StretchModeEnum.Scale;
            bb.Texture = _hitZone;
            bb.Size = new Vector2(beatBoxXSize, _ySize);
            bb.Position = _beatBoxCenter - new Vector2(bb.Size.X * 0.5f, 0);

            _canvasLayer.AddChild(bb);

            //_beatPosition = _beatBox.Position;
            //_beatBox.Position = new Vector2(viewportCenter() - beatBoxXSize * 0.5f, _beatBox.Position.Y);
            //_beatBox.Size = new Vector2(beatBoxXSize, _beatBox.Size.Y);


            //_beatBox.Resized += () =>
            //{
            //    _viewportXCenter = GetViewportRect().Size.X * 0.5f;
            //    _viewportYBottom = GetViewportRect().Size.Y;
            //    _beatBoxCenter = new Vector2(_viewportXCenter, _viewportYBottom - _bottomOffset);

            //    float cameraX = viewportSizeX();
            //    float beatBoxXSize = cameraX / _visibleBeats * _accuracy;

            //    //_beatPosition = _beatBox.Position;
            //    //_beatBox.Position = new Vector2(viewportCenter() - beatBoxXSize * 0.5f, _beatBox.Position.Y);
            //    //_beatBox.Size = new Vector2(beatBoxXSize, _beatBox.Size.Y);
            //};
        }

        public void Subscribe(Action<uint> action)
        {
            _beatQueue.OnBeat += action;
        }

        public override void _Process(double delta)
        {
            _beatQueue.Play(delta, viewportSizeX(), _beatBoxCenter, _ySize);
        }

        private float viewportSizeX() // can be const
        {
            return GetViewportRect().Size.X;
        }

        private float viewportCenter() // can be const
        {
            return GetViewportRect().Size.X / 2;
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
