using BeatSystem.scripts.BeatSystem.Domain.Properties;
using Godot;
using System;

namespace TileBeat.scripts.BeatSystem.BeatSystemGodot.Track
{
    internal record GodotTrack(AudioStream audioStream, int bpm) : HasBpm, HasFullLength
    {
        private double fullLength = audioStream.GetLength();

        public int GetBpm()
        {
            return bpm;
        }

        public double GetFullLength()
        {
            return fullLength;
        }
    }
}
