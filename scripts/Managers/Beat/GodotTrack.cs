using Godot;

namespace TileBeat.scripts.Managers.Beat
{

    public record GodotTrack(AudioStream audioStream, int bpm)
    {
        private float fullLength = (float)audioStream.GetLength();

        public int GetBpm()
        {
            return bpm;
        }

        public float GetFullLength()
        {
            return fullLength;
        }
    }
}
