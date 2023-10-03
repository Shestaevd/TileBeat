using Godot;
using System;
namespace TileBeat.scripts.GameUtils
{
    internal class CameraUtils
    {

        // gonna keep it here for the time
        public static Camera2D ScaleCameraToGrid(Camera2D camera, Sprite2D[,] grid, int tileMargin)
        {

            Vector2 viewportsize = camera.GetViewport().GetVisibleRect().Size;

            int ySize = grid.GetLength(0);
            int xSize = grid.GetLength(1);

            float nodeXSize = 0;
            float nodeYSize = 0;

            float maxNodeSize = Math.Max(nodeXSize, nodeYSize);

            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Sprite2D sprite = grid[i, j];
                    Vector2 tSize = sprite.Texture.GetSize();

                    nodeXSize = Math.Max(nodeXSize, tSize.X);
                    nodeYSize = Math.Max(nodeYSize, tSize.Y);
                }

            float zoomToGrid = Math.Min(
                viewportsize.X / (xSize * (nodeXSize + tileMargin) + tileMargin + maxNodeSize),
                viewportsize.Y / (ySize * (nodeYSize + tileMargin) + tileMargin + maxNodeSize)
            );

            float zoom = zoomToGrid;

            camera.Zoom = new Vector2(
                zoom,
                zoom
            );

            return camera;

        }

        public static Camera2D SetPositionToGridCenter(Camera2D camera, Sprite2D[,] grid, int tileMargin) 
        {

            Vector2 start = grid[0, 0].Position;
            int ySize = grid.GetLength(0);
            int xSize = grid.GetLength(1);

            float nodeXSize = 0;
            float nodeYSize = 0;

            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Sprite2D sprite = grid[i, j];
                    Vector2 tSize = sprite.Texture.GetSize();

                    nodeXSize = Math.Max(nodeXSize, tSize.X);
                    nodeYSize = Math.Max(nodeYSize, tSize.Y);
                }

            camera.Position = new Vector2(
                    start.X - (tileMargin / 2 + nodeXSize) / 2 + (nodeXSize + tileMargin) / 2 * xSize,
                    start.Y - (tileMargin / 2 + nodeYSize) / 2 + (nodeYSize + tileMargin) / 2 * ySize
            );

            return camera;
        }
    }
}
