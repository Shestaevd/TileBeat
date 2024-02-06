using System;

namespace TileBeat.scripts.GameUtils
{
    internal class BeatUtils
    {
        public static float FindInterval(float fullLength, int bpm)
        {
            double bps = Math.Round(bpm / 60d, 1);
            double bpt = Math.Round(fullLength * bps, 1);
            return (float) Math.Round(fullLength / bpt, 1);
        }
    }
}