using Godot;

namespace TileBeat.scripts.GameObjects.Tiles
{
    internal interface ITileSpawner
    {
        public void Spawn(Vector2 spawnPosition, Sprite2D sprite);
    }
}
