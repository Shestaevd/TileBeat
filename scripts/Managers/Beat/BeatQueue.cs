using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TileBeat.scripts.Managers.Beat
{
    public class BeatQueue
    {
        public event Action<uint> OnBeat;

        private Queue<AbstractBeat> _queue;
        private Queue<AbstractBeat> _inPlay = new Queue<AbstractBeat>();
        private BeatDrawer _drawer;
        private float _interval;
        private float _showBeats;

        public BeatQueue(Queue<AbstractBeat> queue, float interval, uint showBeats, BeatDrawer drawer)
        {
            _queue = queue;
            _drawer = drawer;
            _interval = interval;
            _showBeats = showBeats;

            if (showBeats == 0) showBeats = 1;
            if (showBeats > 10) showBeats = 10; // i don't see any reason to show more then 10 beat markers at a time

            for (uint beats = 1; beats <= showBeats; beats++)
            {
                AbstractBeat beat = new EmptyBeat(uint.MaxValue);
                beat.Spawn(interval * beats);
                _inPlay.Enqueue(beat); // initialize track with empty beats
            }
        }

        public bool Play(double delta, float viewportX, Vector2 center)
        {
            List<Vector2> positions = new List<Vector2>();
            foreach (AbstractBeat aBeat in _inPlay)
            {
                aBeat.Move(delta);
                if (aBeat is Beat beat)
                {
                    Tuple<Vector2, Vector2> newPos = beat.GetPosition(center, viewportX);
                    positions.Add(newPos.Item1);
                    positions.Add(newPos.Item2);
                }
                   
            }

            _drawer.UpdateBeatsPositions(positions);

            if (_inPlay.TryPeek(out AbstractBeat currentBeat) && currentBeat.IsExpired())
            {
                OnBeat?.Invoke(currentBeat.Index);
                _inPlay.Dequeue();

                if (_queue.TryDequeue(out AbstractBeat nextBeat))
                {
                    nextBeat.Spawn(_interval * _showBeats);
                    _inPlay.Enqueue(nextBeat);
                }
            }

            return _inPlay.Count > 0;
        }

        public float UntilNextInterval()
        {
            if (_inPlay.TryPeek(out AbstractBeat currentBeat))
            {
                return currentBeat.UntilBeat();
            }
            else
            {
                return 0f;
            }
        }

        public uint CurrentBeatIndex()
        {
            if (_inPlay.TryPeek(out AbstractBeat currentBeat))
            {
                return currentBeat.Index;
            }
            else
            {
                return 0;
            }
        }
    }
}
