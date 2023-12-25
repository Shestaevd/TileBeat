using BeatSystem.scripts.BeatSystem.Domain.System;
using Godot;
using System;
using System.Collections.Generic;
using TileBeat.scripts.BeatSystem.BeatSystemGodot.Track;
using TileBeat.scripts.BeatSystem.BeatTrackingSystem.Domain.Utils;

namespace TileBeat.scripts.Managers.Beat
{
	public partial class BeatManager : AudioStreamPlayer2D, IAudioBeatSystem<GodotTrack>
	{

        private GodotTrack _track;
		private Texture2D _marker;
		private TextureRect _beatBox;
        private CanvasLayer _canvasLayer;
        private float _accuracy;
		private uint _visibleBeats;
        private float _interval;
        private BeatQueue _beatQueue;
        private Vector2 _beatPosition;
        private Vector2 _viewportCenter;
        private const string _beatBoxNodeName = "BeatBox";

        public BeatManager(
            CanvasLayer canvas, //	should have TextureRec named "BeatBox". 		
            GodotTrack track,
            Texture2D hitZone,
            Texture2D marker,
            Queue<AbstractBeat> queue,
            uint visibleBeats = 3,
            float accuracy = 0.5f //   from 0 to 1
            )
		{
            _track = track;
			_marker = marker;
            _accuracy = accuracy;
            _visibleBeats = visibleBeats == 0 ? 1 : visibleBeats;
            _interval = Utils.FindInterval(track.GetFullLength(), track.GetBpm());
            _canvasLayer = canvas;

            Stream = _track.audioStream;

            _beatQueue = new BeatQueue(queue, _interval, visibleBeats, canvas); // generate and move beat sprites

            Subscribe(i => { 
                if (i == 0)
                { 
                    Play();
                    GD.Print("start track play"); 
                } 
            });

            _beatBox = canvas.GetNode<TextureRect>(_beatBoxNodeName);
            _beatBox.Texture = hitZone;
        }

        public override void _Ready()
		{
            float cameraX = viewportSizeX();
            float beatBoxXSize = cameraX / _visibleBeats * _accuracy;

            _beatPosition = _beatBox.Position;
            GD.Print("_beatPosition: " + _beatPosition);
            //_beatBox.Position = _beatBox.Position - new Vector2(beatBoxXSize / 2, 0);
            GD.Print(" _beatBox.Position: " + _beatBox.Position);
            _beatBox.Size = new Vector2(beatBoxXSize, _beatBox.Texture.GetHeight());


            _beatBox.Resized += () =>
            {
                _beatPosition = _beatBox.Position + new Vector2(beatBoxXSize / 2, 0);
                //_ViewportSize = GetViewportRect().Size;
                //_BeatLineSize = new Vector2((float)(_ViewportSize.X * 0.5f / _CountVisibleBeatLines * _Accurancy), _TextureHeight);
            };
        }

        public void Subscribe(Action<uint> action)
        {
            _beatQueue.OnBeat += action;
        }

        public override void _Process(double delta)
		{
            
            _beatQueue.Play(delta, viewportSizeX(), new Vector2(viewportCenter(), _beatBox.Position.Y), _beatBox.Size.Y);
            
        }

        private float viewportSizeX()
        {
            return GetViewportRect().Size.X;
        }

        private float viewportCenter()
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
