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
        public void Spawn(Vector2 spawnPosition, Sprite2D sprite)
        {
            throw new NotImplementedException();
        }
    }
}
