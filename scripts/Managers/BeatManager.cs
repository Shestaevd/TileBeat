using BeatSystem.scripts.BeatSystem.Domain.System;
using Godot;
using System;
using System.Collections.Generic;
using TileBeat.scripts.BeatSystem.BeatSystemGodot;
using TileBeat.scripts.BeatSystem.BeatSystemGodot.Track;
using TileBeat.scripts.BeatSystem.BeatTrackingSystem.Domain.Utils;

namespace TileBeat.scripts.Managers
{
	public partial class BeatManager : AudioStreamPlayer2D, IAudioBeatSystem<GodotTrack>
	{
        private abstract record Beat(float timeUntilBeat);
        private partial record SkipStartBeat(float timeUntilBeat) : Beat(timeUntilBeat); // will not trigger anything
        private partial record LiveBeat(TextureRect obj, float timeUntilBeat) : Beat(timeUntilBeat);

        public event Action OnBeat;

        private GodotTrack _track;
		private Texture2D _marker;
		private TextureRect _beatBox;
		private float _accuracy;
		private uint _visibleBeats;
        private float _viewportY;
        private Beat[] _beats;
        private float _lastBeat;
        private uint _leftToSkip;
        private float _interval;

        private const string _beatBoxNodeName = "BeatBox";
        //														                            should have TextureRec named "BeatBox". 		                 from 0 to 1
		//																				                      V									                   V
        public BeatManager(float viewportY, GodotTrack track, Texture2D hitZone, Texture2D marker, CanvasLayer canvas, uint skipFirst, uint visibleBeats, float accuracy)
		{

            _track = track;
			_marker = marker;
            _viewportY = viewportY;
            _accuracy = accuracy;
            _visibleBeats = visibleBeats == 0 ? 1 : visibleBeats;
            _leftToSkip = skipFirst;
            _beats = new Beat[visibleBeats];
            _interval = Utils.FindInterval(track.GetFullLength(), track.GetBpm());
            Stream = _track.audioStream;

            for (int i = 0; i < visibleBeats; i++)
            {
                if (skipFirst > i)
                {
                    _beats[i] = new SkipStartBeat((i + 1) * GetInterval());
                    _leftToSkip--;
                }
                else
                {
                    TextureRect textureRect = new TextureRect();
                    textureRect.Texture = marker;
                    textureRect.Position = new Vector2(1, _beatBox.Position.Y);
                    _beats[i] = new LiveBeat(textureRect, (i + 1) * GetInterval());
                }
            }

            _beatBox = canvas.GetNode<TextureRect>(_beatBoxNodeName);
            _beatBox.Texture = hitZone;
        }

        public override void _Ready()
		{
            float beatBoxXSize = _viewportY / _visibleBeats / 100 * (_accuracy * 100);

            _beatBox.Size = new Vector2(beatBoxXSize, _beatBox.Texture.GetSize().Y);
            _beatBox.Position = _beatBox.Position - new Vector2(beatBoxXSize / 2, 0);
        }

        public override void _Process(double delta)
		{
            if (Playing)
            {
                for(int index = 0; index < _beats.Length; index++) 
                {
                    if (_beats[index] != null)
                    {
                        _beats[index] = _beats[index] with { timeUntilBeat = _beats[index].timeUntilBeat - (float) delta };
                        if (_beats[index].timeUntilBeat < 0)
                        {

                        } else if (_beats[index] is LiveBeat beat)
                        {
                            beat.obj.Position = beat.obj.Position.Lerp(_beatBox.Position, 0f);
                        }
                    }
                }

                _lastBeat = UntilNextBeat();
            }
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
            VolumeDb = (float)volume;
        }

        public float GetVolume()
        {
            return VolumeDb;
        }

        public float UntilNextBeat()
        {
            return _interval - (GetPlaybackPosition() % _interval);
        }

        public void Pause()
        {
            Stop();
        }

        public new void Stop()
        {
            base.Play();
        }

        public void Play()
        {
           base.Play();
        }
    }
}
