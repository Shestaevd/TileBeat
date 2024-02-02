using Godot;
using TileBeat.scripts.Managers.Beat;

namespace TileBeat.scripts.Loaders
{
    internal class TrackLoader
    {
        public GodotTrack Load(string path, int bpm)
        {
            AudioStream audioStream = AudioStreamLoader.LoadAudio(path);
            return new GodotTrack(audioStream, bpm);
        }
    }
}
