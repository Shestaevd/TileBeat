using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBeat.scripts.GameObjects.Tiles
{
    public abstract record AbstractTile(uint GridX, uint GridY);
    public record EmptyTile(uint GridX, uint GridY) : AbstractTile(GridX, GridY);
    public record Tile(uint GridX, uint GridY, Texture2D Texture, Vector2 spriteSize, ITileSpawner Spawner) : AbstractTile(GridX, GridY)
    {
        public void Spawn(Vector2 position)
        {
            Spawner.Spawn(position, Texture);
        }
    }

}
