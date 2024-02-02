using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TileBeat.scripts.Managers.Beat
{
    public partial class BeatClassicDrawer : Node2D
    {
        private float _targetSeconds;
        private uint _showSeconds;
        private float _ySize;
        private float _bottomOffset;

        private Rect2 _beatBox;
        private Texture2D _beatBoxTexture;
        private Texture2D _beatMarkerTexture;

        private List<Vector2> _beatsToDraw = new List<Vector2>();


        BeatManager _beatManager;
        public override void _Draw()
        {
            float viewportXSize = GetViewportRect().Size.X;
            float beatBoxXSize = viewportXSize * 0.5f * _targetSeconds;

            float viewportXCenter = viewportXSize * 0.5f;
            float viewportYBottom = GetViewportRect().Size.Y;
            Vector2 beatBoxCenter = new Vector2(viewportXCenter, viewportYBottom - _bottomOffset);

            _beatBox = new Rect2();
            _beatBox.Size = new Vector2(beatBoxXSize, _ySize);
            _beatBox.Position = beatBoxCenter - new Vector2(_beatBox.Size.X * 0.5f, 0);
            DrawTextureRect(_beatBoxTexture, _beatBox, false);
            foreach(Vector2 beatPosition in _beatsToDraw)
            {
                Rect2 rect = new Rect2();
                
                rect.Size = new Vector2(_beatMarkerTexture.GetWidth(), _ySize);
                rect.Position = beatPosition - new Vector2(rect.Size.X * 0.5f, 0);
                
                DrawTextureRect(_beatMarkerTexture, rect, false);
            }
        }

        public BeatClassicDrawer(Texture2D hitBox, Texture2D hitMarker, float ySize, uint showSeconds, float targetSeconds, float bottomOffset, BeatManager beatManager) 
        {
            _targetSeconds = targetSeconds;
            _beatManager = beatManager;
            _showSeconds = showSeconds;
            _bottomOffset = bottomOffset;
            _ySize = ySize;
            _beatMarkerTexture = hitMarker;
            _beatBoxTexture = hitBox;
        }

        public override void _Process(double delta)
        {
            float viewportXSize   = GetViewportRect().Size.X;
            float viewportXCenter = viewportXSize * 0.5f;
            float viewportYBottom = GetViewportRect().Size.Y;
            Vector2 beatBoxCenter = new Vector2(viewportXCenter, viewportYBottom - _bottomOffset);

            Vector2 start = new Vector2(beatBoxCenter.X - viewportXCenter / 2, beatBoxCenter.Y);
            Vector2 startTwin = new Vector2(beatBoxCenter.X + viewportXCenter / 2, beatBoxCenter.Y);

            List<Beat> beatsToDraw = _beatManager
                .Beats
                .Where(beat => beat.UntilBeat() < _showSeconds && beat is Beat)
                .Select(beat => (Beat) beat)
                .ToList();

            List<Vector2> positions = new List<Vector2>();

            foreach(AbstractBeat beat in beatsToDraw) 
            {
                float untilBeat = beat.UntilBeat();
                float showSecondsOneP = (float) _showSeconds / 100;
                float percent = 1 - (untilBeat / showSecondsOneP / 100);
                positions.Add(start.Lerp(beatBoxCenter, percent));
                positions.Add(startTwin.Lerp(beatBoxCenter, percent));
            }

            _beatsToDraw = positions;

            QueueRedraw();
        }
    }
}
