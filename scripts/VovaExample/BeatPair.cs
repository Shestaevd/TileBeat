using Godot;

namespace TileBeat.scripts.VovaExample
{
    public class BeatPairs
    {
        public BeatLine Line { get; }
        public BeatLine Twin { get; }

        public float Progress { get; private set; }
        public bool ReadyToDelete { get; private set; }

        private CanvasLayer _root;

        public BeatPairs(Vector2 center, float _targetTime, float speed, Texture2D texture, CanvasLayer root)
        {
            Line = new BeatLine(center, _targetTime, false, speed, texture);
            Twin = new BeatLine(center, _targetTime, true, speed, texture);
            _root = root;
            _root.AddChild(Line);
            _root.AddChild(Twin);
        }

        public void Update()
        {
            Progress = Line.Position.X / (_root.GetViewport().GetVisibleRect().Size.X * 0.5f);

            if (Progress > 1)
            {
                Line.QueueFree();
                Twin.QueueFree();
                ReadyToDelete = true;
            }
        }
    }
}
