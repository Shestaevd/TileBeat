using System;
using System.Collections.Generic;

namespace TileBeat.scripts.Managers.Beat
{
    public static class BeatQueueAutoMaker
    {
        public static LinkedList<AbstractBeat> GenerateBeatsByBpm(float trackLength, int bpm, uint beats, Dictionary<int, Action> actions = null)
        {
            float interval = GameUtils.BeatUtils.FindInterval(trackLength, bpm);
            LinkedList<AbstractBeat> beatList = new LinkedList<AbstractBeat>();
            for (uint i = 0; i < beats; i++) 
            {
                if (actions != null)
                {
                    Action mbAction;
                    actions.TryGetValue((int)i, out mbAction);
                    beatList.AddLast(new Beat(i, i * interval, mbAction));
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
