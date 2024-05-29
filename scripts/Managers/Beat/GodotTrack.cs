using Godot;
using System.Collections.Generic;

namespace TileBeat.scripts.Managers.Beat
{

    public record GodotTrack(AudioStream audioStream, int bpm, LinkedList<AbstractBeat> beats)
    {
        private float fullLength = (float) audioStream.GetLength();

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
