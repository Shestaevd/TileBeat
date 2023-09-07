using Godot;
using System;
using System.Collections.Generic;

public partial class LevelView : Node2D
{

	public int XSize = 10;
	public int YSize = 10;

	float NodeXSize = 0;
	float NodeYSize = 0;

	public Node2D[,] tiles;

	public string[,] tileNames;
	public Dictionary<string, Sprite2D> sprites;

	private Camera2D camera;
	private Sprite2D background;

	private void testlogic()
	{
		sprites = new Dictionary<string, Sprite2D>();
		var tilePS = GD.Load<PackedScene>("scenes/test/tile.tscn");

		var tile1 = (Sprite2D)tilePS.Instantiate();
		sprites.Add("t1", tile1);

		var tile2 = (Sprite2D)tilePS.Instantiate();
		tile2.Rotate(90);
		sprites.Add("t2", tile2);

		var tile3 = (Sprite2D)tilePS.Instantiate();
		tile3.Rotate(-90);
		sprites.Add("t3", tile3);

		tileNames = new string[10, 10] {
			{ "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
			{ "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
			{ "t1","t1","t2","t2","t2","t2","t2","t2","t1","t1" },
			{ "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
			{ "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
			{ "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
			{ "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
			{ "t1","t1","t3","t3","t3","t3","t3","t3","t1","t1" },
			{ "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
			{ "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
		};
	}

	public override void _Ready()
	{
		if (tileNames == null) testlogic();

		camera = (Camera2D) GetNode("Camera2D");
		background = (Sprite2D) GetNode("background");

		foreach (KeyValuePair<string, Sprite2D> entry in sprites)
		{
			var tSize = entry.Value.Texture.GetSize();

			NodeXSize = Math.Max(NodeXSize, tSize.X);
			NodeYSize = Math.Max(NodeYSize, tSize.Y);
		}

		float TileMargin = 10;

		float TotalGridSizeX = XSize * (NodeXSize + TileMargin) + TileMargin;
		float TotalGridSizeY = YSize * (NodeYSize + TileMargin) + TileMargin;

		var viewportsize = camera.GetViewport().GetVisibleRect().Size;

		var zoom = Math.Min(viewportsize.X / TotalGridSizeX, viewportsize.Y / TotalGridSizeY);

		camera.Zoom = new Vector2(
			zoom,
			zoom
		);

		background.ApplyScale(new Vector2(
			1/zoom,
			1/zoom
		));

		tiles = new Node2D[XSize, YSize];

		var startOfGridX = - TotalGridSizeX / 2 - NodeXSize / 2 + TileMargin;
		var startOfGridY = - TotalGridSizeY / 2 - NodeYSize / 2 + TileMargin;

		for (int i = 0; i < XSize; i++)
		{
			for (int j = 0; j < YSize; j++)
			{
				var tile = new Node2D
				{
					Position = new Vector2(
						startOfGridX + NodeXSize * i + NodeXSize / 2 + (TileMargin * i),
						startOfGridY + NodeYSize * j + NodeYSize / 2 + (TileMargin * j)
					)
				};

				var sprite = (Sprite2D)sprites[tileNames[i, j]].Duplicate();
				sprite.Position = new Vector2(NodeXSize / 2, NodeYSize / 2);
				tile.AddChild(sprite);

				AddChild(tile);

				tiles[i, j] = tile;
			}
		}
	}


}
