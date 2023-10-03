using BeatSystem.scripts.BeatSystem.Domain.Properties;
using Godot;
using System;

namespace TileBeat.scripts.BeatSystem.BeatSystemGodot.Track
{

    public record GodotTrack(AudioStream audioStream, int bpm) : HasBpm, HasFullLength
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
