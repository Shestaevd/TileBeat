using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TileBeat.scripts.Managers.Beat
{
    public class BeatQueue
    {
        public event Action<uint> OnBeat;

        private Queue<AbstractBeat> _queue;
        private LinkedList<AbstractBeat> _inPlay = new LinkedList<AbstractBeat>();
        private BeatDrawer _drawer;
        private BeatManager _beatManager;
        private float _interval;
        private float _showBeats;
        private float _accuracy;

        public BeatQueue(float interval, uint showBeats,  float accuracy, Queue<AbstractBeat> queue, BeatDrawer drawer, BeatManager beatManager)
        {
            _accuracy = accuracy;
            _queue = queue;
            _drawer = drawer;
            _interval = interval;

            _showBeats = showBeats;
            _beatManager = beatManager;

            for (uint beats = 1; beats <= showBeats; beats++)
            {
                AbstractBeat beat = new EmptyBeat(uint.MaxValue);
                beat.Create(interval * beats, 0);
                _inPlay.AddLast(beat); // initialize track with empty beats
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

            _drawer.UpdateBeatPositions(positions);

            foreach (Vector2 v in positions)
            {
                GD.PrintRaw(v.X + ", ");
            }

            AbstractBeat currentBeat = _inPlay.FirstOrDefault();
            if (currentBeat != null && currentBeat.IsExpired())
            {
                OnBeat?.Invoke(currentBeat.Index);
                _inPlay.RemoveFirst();

                if (_queue.TryDequeue(out AbstractBeat nextBeat))
                {
                    GD.Print(_beatManager.UntilNextBeat());
                    nextBeat.Create(_interval * _showBeats, _beatManager.UntilNextBeat());
                     _inPlay.AddLast(nextBeat);
                }
            }

            return _inPlay.Count > 0;
        }

        public float HowAccurate()
        {
            AbstractBeat currentBeat = GetFirstNonEmptyBeat();
            if (currentBeat != null)
            {
                //return currentBeat.HowAccurate(_accuracy);
            }
            return 0;
        }

        public void ClearCurrentBeat()
        {
            AbstractBeat currentBeat = GetFirstNonEmptyBeat();
            if (currentBeat != null)
            {
                _inPlay.First.ValueRef = currentBeat.ToEmptyBeat();
            }
        }

        public float UntilNextInterval()
        {
            AbstractBeat currentBeat = GetFirstNonEmptyBeat();
            if (currentBeat != null)
            {
                return currentBeat.UntilBeat();
            }
            else
            {
                return float.MaxValue;
            }
        }

        public uint CurrentBeatIndex()
        {
            AbstractBeat currentBeat = GetFirstNonEmptyBeat();
            if (currentBeat != null)
            {
                return currentBeat.Index;
            }
            else
            {
                return uint.MaxValue;
            }
        }

        private Beat GetFirstNonEmptyBeat()
        {
            for (LinkedListNode<AbstractBeat> aBeat = _inPlay.First; aBeat != null; aBeat = aBeat.Next)
                if (aBeat.Value is Beat beat) return beat;
            return null;
        }
    }
}
