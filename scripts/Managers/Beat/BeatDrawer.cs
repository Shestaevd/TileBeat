using Godot;
using System.Collections.Generic;

namespace TileBeat.scripts.Managers.Beat
{
    public partial class BeatDrawer : Node2D
    {

        float _visibleBeats;
        float _ySize;
        float _bottomOffset;

        Rect2 _beatBox;
        Texture2D _beatBoxTexture;
        Texture2D _beatMarkerTexture;
        List<Vector2> _beats = new List<Vector2>();

        public override void _Draw()
        {
            float cameraX = GetViewportRect().Size.X;
            float beatBoxXSize = cameraX / _visibleBeats;

            float viewportXCenter = cameraX * 0.5f;
            float viewportYBottom = GetViewportRect().Size.Y;
            Vector2 beatBoxCenter = new Vector2(viewportXCenter, viewportYBottom - _bottomOffset);

            _beatBox = new Rect2();
            _beatBox.Size = new Vector2(beatBoxXSize, _ySize);
            _beatBox.Position = beatBoxCenter - new Vector2(_beatBox.Size.X * 0.5f, 0);
            DrawTextureRect(_beatBoxTexture, _beatBox, false);
            foreach(Vector2 beatPosition in _beats)
            {
                Rect2 rect = new Rect2();
                rect.Size = new Vector2(_beatMarkerTexture.GetWidth(), _ySize);
                rect.Position = beatPosition - new Vector2(rect.Size.X * 0.5f, 0);
                DrawTextureRect(_beatMarkerTexture, rect, false);
            }
        }


        public void UpdateBeatPositions(List<Vector2> beats)
        {
            _beats = beats;
        }

        public BeatDrawer(Texture2D hitBox, Texture2D hitMarker, float ySize, float visibleBeats, float bottomOffset) 
        {
            _visibleBeats = visibleBeats;
            _bottomOffset = bottomOffset;
            _ySize = ySize;
            _beatMarkerTexture = hitMarker;
            _beatBoxTexture = hitBox;
        }

        public override void _Process(double delta)
        {
            QueueRedraw();
        }
    }
}
