using BeatSystem.scripts.BeatSystem.Domain.Track;
using System;
using System.Collections.Generic;
using TileBeat.scripts.BeatSystem.BeatSystemGodot.Track;
using TileBeat.scripts.BeatSystem.BeatTrackingSystem.Domain.Utils;

namespace TileBeat.scripts.Managers.Beat
{
    public static class BeatQueueAutoMaker
    {
        public static LinkedList<AbstractBeat> GenerateBeatsByBpm(GodotTrack track, uint beats, List<Action> actions = null)
        {
            
            float interval = Utils.FindInterval(track.GetFullLength(), track.GetBpm());
            LinkedList<AbstractBeat> beatList = new LinkedList<AbstractBeat>();
            for (uint i = 0; i < beats; i++) 
            {
                if (actions != null && actions.Count > i)
                {
                    beatList.AddLast(new Beat(i, i * interval, actions[(int) i]));
                }
                else
                {
                    beatList.AddLast(new Beat(i, i * interval));
                }
            }
            return beatList;

        }
    }
}
