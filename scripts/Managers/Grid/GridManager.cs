using Godot;
using System;
using System.Collections.Generic;
using TileBeat.scripts.GameObjects.Tiles;

namespace TileBeat.scripts.Managers
{
	public partial class GridManagerV1 : Node2D
	{
		private float _tileMargin;

		private Sprite2D[,] _sprites;
		private Vector2 _gridCenter;
        private Vector2 _gridBottom;

        public Vector2 GridCenter
        {
            get { return _gridCenter; }
        }
        
        public Vector2 GridBottom
        {
            get { return _gridBottom; }
        }
        public Sprite2D[,] Sprites
		{
			get { return _sprites; }
		}
		public GridManagerV1(float tileMargin, Sprite2D[,] sprites)
		{
			_sprites = sprites;
			_tileMargin = tileMargin;

            float nodeXSize = 0;
            float nodeYSize = 0;

            for (int i = 0; i < _sprites.GetLength(0); i++)
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    Sprite2D sprite = _sprites[i, j];
                    Vector2 tSize = sprite.Texture.GetSize();

                    nodeXSize = Math.Max(nodeXSize, tSize.X);
                    nodeYSize = Math.Max(nodeYSize, tSize.Y);
                }

            Vector2 start = _sprites[0, 0].Position;

            for (int i = 0; i < _sprites.GetLength(0); i++)
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    if (_sprites[i, j] != null)
                    {
                        Sprite2D sprite = (Sprite2D)_sprites[i, j].Duplicate();
                        sprite.Position = new Vector2(
                            start.X + nodeXSize * i + i * _tileMargin,
                            start.X + nodeYSize * j + j * _tileMargin
                        );
                        AddChild(sprite);
                    }
                }

            _gridCenter = new Vector2(
                    start.X - (nodeXSize + _tileMargin) / 2 + (nodeXSize + _tileMargin) / 2 * _sprites.GetLength(1),
                    start.Y - (nodeYSize + _tileMargin) / 2 + (nodeYSize + _tileMargin) / 2 * _sprites.GetLength(0)
            );

            _gridBottom = new Vector2(
                    _gridCenter.X,
                    start.Y - (nodeYSize + _tileMargin) / 2 + (nodeYSize + _tileMargin) * _sprites.GetLength(0)
            );
        }

		public override void _Ready()
		{

		}
    }

    public partial class GridManager : Node2D
    {
        private List<List<AbstractTile>> _tiles = new List<List<AbstractTile>>();
        private Vector2 _startPosition;
        private float _tileMargin;
        private Vector2 SpriteSize;

        public GridManager(List<List<AbstractTile>>tiles, float tileMargin, Vector2 startPosition = new Vector2()) 
        {
            _startPosition = startPosition;
            _tileMargin = tileMargin;
            SpawnGrid(tiles);
        }
        
        public void SpawnGrid(List<List<AbstractTile>> tiles)
        {
            for (int column = 0; column < _tiles.Count; column++)
                for (int index = 0; index < _tiles[column].Count; index++)
                {
                    AbstractTile tile = _tiles[column][index];
                    SpawnTile(tile);
                }
            _tiles = tiles;
            for ( int column = 0; column < _tiles.Count; column++)
                for (int index = 0; index < _tiles[column].Count; index++)
                {
                    AbstractTile tile = _tiles[column][index];
                    SpawnTile(tile);
                }
        }

        public void SpawnEmptyGrid(uint GridX, uint GridY) 
        {
            List<List<AbstractTile>> tiles = new List<List<AbstractTile>>();
            for (int column = 0; column <= GridY; column++)
                for (int index = 0; index <= GridX; index++)
                {
                    if (tiles.Count < column)
                        tiles.Add(new List<AbstractTile>());
                    tiles[column].Add(new EmptyTile((uint) index, (uint) column));
                }
            SpawnGrid(tiles);
        }

        public void SpawnTile(AbstractTile tile) 
        {
            throw new NotImplementedException();
        }

        public Vector2 GetTilePosition(uint GridX, uint GridY)
        {
            throw new NotImplementedException();
        }

        public AbstractTile GetTile(uint GridX, uint GridY) 
        {
            throw new NotImplementedException();
        }

    }
}
