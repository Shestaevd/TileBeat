using Godot;
using TileBeat.scripts.Managers.Beat;

namespace TileBeat.scripts.UI
{
    public partial class SimpleBeatDrawer : Node2D
    {
        private BeatManager _beatManager;
        private Sprite2D _sprite2D;
        private float _target;

        public SimpleBeatDrawer(BeatManager beatManager, Sprite2D sprite2D, float target)
        {
            _beatManager = beatManager;
            _sprite2D = sprite2D;
            _target = target;
        }

        public override void _Ready()
        {
            _sprite2D.Modulate = new Color(1, 0, 0);
        }

        public override void _Process(double delta)
        {
            bool inBeat = _beatManager.InBeatRangePrecision(_target) > 0;
            _sprite2D.Modulate = new Color(inBeat ? 0 : 1, inBeat ? 1 : 0, 0);
        }

    }
}
