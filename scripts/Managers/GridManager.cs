using Godot;
using System;

namespace TileBeat.scripts.Managers
{
    public partial class GridManager : Node2D
    {
        private float _tileMargin;

        private int _xSize;
        private int _ySize;

        private Sprite2D[,] _sprites;

        private Camera2D _camera;
        private Sprite2D _background;

        public GridManager(int xSize, int ySize, float tileMargin, Sprite2D[,] sprites, Camera2D camera, Sprite2D background)
        {
            _xSize = xSize;
            _ySize = ySize;
            _sprites = sprites;
            _camera = camera;
            _tileMargin = tileMargin;
            _background = background;
        }

        public override void _Ready()
        {
            if (_sprites == null || _sprites.Length == 0) throw new ArgumentNullException("_sprites are empty");
            if (_background == null) throw new ArgumentNullException("_background is null");
            if (_camera == null) throw new ArgumentNullException("_camera is null");

            float NodeXSize = 0;
            float NodeYSize = 0;

            for (int i = 0; i < _sprites.GetLength(0); i++)
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    Sprite2D sprite = _sprites[i, j];
                    var tSize = sprite.Texture.GetSize();

                    NodeXSize = Math.Max(NodeXSize, tSize.X);
                    NodeYSize = Math.Max(NodeYSize, tSize.Y);
                }

            float TotalGridSizeX = _xSize * (NodeXSize + _tileMargin) + _tileMargin;
            float TotalGridSizeY = _ySize * (NodeYSize + _tileMargin) + _tileMargin;

            float startOfGridX = -TotalGridSizeX / 2 - NodeXSize / 2 + _tileMargin;
            float startOfGridY = -TotalGridSizeY / 2 - NodeYSize / 2 + _tileMargin;

            for (int i = 0; i < _sprites.GetLength(0); i++)
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    Sprite2D sprite = (Sprite2D) _sprites[i, j].Duplicate();
                    sprite.Position = new Vector2(
                        startOfGridX + NodeXSize * i + NodeXSize / 2 + (_tileMargin * i),
                        startOfGridY + NodeYSize * j + NodeYSize / 2 + (_tileMargin * j)
                    );

                    AddChild(sprite);
                }

            // set camera position to grid center

            Vector2 viewportsize = _camera.GetViewport().GetVisibleRect().Size;

            float zoom = Math.Min(viewportsize.X / TotalGridSizeX, viewportsize.Y / TotalGridSizeY);

            _camera.Zoom = new Vector2(
                zoom,
                zoom
            );

            _background.ApplyScale(new Vector2(
                1 / zoom,
                1 / zoom
            ));

            _background.Position = _camera.Position;

            AddChild(_background);



        }
    }
}

