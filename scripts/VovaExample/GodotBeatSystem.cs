//using BeatSystem.scripts.BeatSystem.Domain.System;
//using System;
//using BeatSystem.BeatTrackingSystem.Domain.Utils;
//using BeatSystem.BeatSystemGodot.Track;
//using Godot;

//namespace BeatSystem.BeatSystemGodot
//{
//	public class GodotBeatSystem : IAudioBeatSystem<GodotTrack>
//	{
//		private int _CurrentBeat;

//		private float _Interval;
//		private GodotTrack _Track;
//        private AudioStreamPlayer _Player;

//		public event Action OnNextBeat;

//		public GodotBeatSystem(GodotTrack track, AudioStreamPlayer player) 
//		{
//			_Interval = Utils.FindInterval(track.GetFullLength(), track.GetBpm());
//			_Track = track;
//			_Player = player;
			
//			_Player.Stream = _Track.audioStream;
//		}

//		public float GetInterval()
//		{
//			return _Interval;
//		}

//		public float GetPlaybackPosition()
//		{
//			return _Player.GetPlaybackPosition();
//		}

//		public float InTargetOfBeat()
//		{
//			return Utils.FindTargetBeat(_Track.GetFullLength(), _Interval);
//		}

//		public void Pause()
//		{
//			_Player.Stop();
//		}

//		public void Play()
//		{
//			_Player.Play();
//		}

//		public void SetPlaybackPosition(long position)
//		{
//			_Player.Seek(position);
//		}

//		public void SetVolume(float volume)
//		{
//			_Player.VolumeDb = (float) volume;
//		}

//		public float GetVolume()
//		{
//			return _Player.VolumeDb;
//		}

//		public void Stop()
//		{
//			_Player.Stop();
//		}

//		public float UntilNextBeat()
//		{
//            int numBeat = (int)(GetPlaybackPosition() / _Interval);

//			if (_CurrentBeat != numBeat)
//			{
//				_CurrentBeat = numBeat;
//				OnNextBeat?.Invoke();
//			}

//            return _Interval - (GetPlaybackPosition() % _Interval);
//		}
//	}
//}
