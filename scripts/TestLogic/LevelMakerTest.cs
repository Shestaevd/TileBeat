using Godot;
using TileBeat.scripts.Loaders;
using TileBeat.scripts.Managers;

namespace TileBeat.scripts.TestLogic
{
	public partial class LevelMakerTest : Node2D
	{
		public override void _Ready()
		{
			int x = 3;
			int y = 3;
			Sprite2D[,] sprites = new Sprite2D[x, y];

			Sprite2D background = SpriteLoader.LoadSprite("E:\\GodotProjects\\TileBeat\\assets\\test\\aaa.jpg", 0);
			Sprite2D sprite = SpriteLoader.LoadSprite("E:\\GodotProjects\\TileBeat\\assets\\test\\download.png", 1);

			for (int i = 0; i < sprites.GetLength(0); i++)
				for (int j = 0; j < sprites.GetLength(1); j++)
				{
					sprites[i, j] = sprite;
				}

			Camera2D camera = GetNode<Camera2D>("Camera2D");

			GridManager gm = new GridManager(x, y, 10, sprites, camera, background);

			AddChild(gm);
		}
	}
}
