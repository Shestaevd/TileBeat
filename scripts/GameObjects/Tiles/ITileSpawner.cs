using Godot;

namespace TileBeat.scripts.GameObjects.Tiles
{
    public interface ITileSpawner
    {
        public void Spawn(Vector2 spawnPosition, Texture2D sprite, Vector2 targetSize);
    }
}
