using BeatSystem.scripts.BeatSystem.Domain.System.AudioLoader;
using TileBeat.scripts.Loaders;
using Godot; 

namespace TileBeat.scripts.BeatSystem.BeatSystemGodot.Track.Loader
{
	internal class TrackLoader : IAudioLoader<GodotTrack>
	{
		public GodotTrack Load(string path, int bpm)
		{
			AudioStream audioStream = AudioStreamLoader.LoadAudio(path);
			return new GodotTrack(audioStream, bpm);
		}
	}
}
