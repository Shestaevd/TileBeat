using BeatSystem.scripts.BeatSystem.Domain.System;
using System;
using TileBeat.scripts.BeatSystem.BeatTrackingSystem.Domain.Utils;
using TileBeat.scripts.BeatSystem.BeatSystemGodot.Track;
using Godot;

namespace TileBeat.scripts.BeatSystem.BeatSystemGodot
{
	internal class GodotBeatSystem : IAudioBeatSystem<GodotTrack>
	{
		private GodotTrack _track;
		private float _interval;
		private AudioStreamPlayer2D _player;
		public GodotBeatSystem(GodotTrack track, AudioStreamPlayer2D player) 
		{
			_interval = Utils.FindInterval(track.GetFullLength(), track.GetBpm());
			_track = track;
			_player = player;
			
			_player.Stream = _track.audioStream;
		}
		public float GetInterval()
		{
			return _interval;
		}

		public float GetPlaybackPosition()
		{
			return _player.GetPlaybackPosition();
		}

		public float InTargetOfBeat()
		{
			return Utils.FindTargetBeat(_track.GetFullLength(), _interval);
		}

		public void Pause()
		{
			_player.Stop();
		}

		public void Play()
		{
			_player.Play();
		}

		public void SetPlaybackPosition(long position)
		{
			_player.Seek(position);
		}

		public void SetVolume(float volume)
		{
			_player.VolumeDb = (float) volume;
		}

		public float GetVolume()
		{
			return _player.VolumeDb;
		}

		public void Stop()
		{
			_player.Stop();
		}

		public float UntilNextBeat()
		{
			return _interval - (GetPlaybackPosition() % _interval);
		}
	}
}
