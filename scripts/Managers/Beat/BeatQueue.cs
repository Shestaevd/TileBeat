using Godot;
using System;
using System.Collections.Generic;

namespace TileBeat.scripts.Managers.Beat
{
    public class BeatQueue
    {
        public event Action<uint> OnBeat;

        private Queue<AbstractBeat> _queue;
        private Queue<AbstractBeat> _inPlay = new Queue<AbstractBeat>();
        private CanvasLayer _root;
        private float _interval;
        private float _showBeats;

        public BeatQueue(Queue<AbstractBeat> queue, float interval, uint showBeats, CanvasLayer root)
        {
            _queue = queue;
            _root = root;
            _interval = interval;
            _showBeats = showBeats;

            if (showBeats == 0) showBeats = 1;
            if (showBeats > 10) showBeats = 10; // i don't see any reason to show more then 10 beat markers at a time

            for (uint beats = 1; beats <= showBeats; beats++)
            {
                _inPlay.Enqueue(new EmptyBeat(interval * beats, uint.MaxValue)); // initialize track with empty beats
            }
        }

        public bool Play(double delta, float viewportX, Vector2 center, float spriteSizeY)
        {
            foreach (AbstractBeat beat in _inPlay)
            {
                beat.Move(delta, center);
            }
            
            if (_inPlay.TryPeek(out AbstractBeat currentBeat) && currentBeat.IsExpired())
            {
                OnBeat?.Invoke(currentBeat.Index);
                currentBeat.Clear();
                _inPlay.Dequeue();

                if (_queue.TryDequeue(out AbstractBeat nextBeat))
                {
                    nextBeat.Spawn(_root, _interval * _showBeats, viewportX, spriteSizeY);
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
