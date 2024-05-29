using System;
using TileBeat.scripts.Managers.Beat;

namespace TileBeat.scripts.GameUtils
{
    public static class BeatUtils
    {
        public static float FindInterval(float fullLength, int bpm)
        {
            double bps = Math.Round(bpm / 60d, 1);
            double bpt = Math.Round(fullLength * bps, 1);
            return (float) Math.Round(fullLength / bpt, 1);
        }
        
        
    }
}