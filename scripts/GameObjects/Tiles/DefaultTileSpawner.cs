using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBeat.scripts.GameObjects.Tiles
{
    internal class DefaultTileSpawner : ITileSpawner
    {
        public DefaultTileSpawner() { }
        public void Spawn(Node2D root, Vector2 spawnPosition, Texture2D sprite, Vector2 targetSize)
        {
            throw new NotImplementedException();
        }
    }
}
