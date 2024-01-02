using Godot;
using System.Collections.Generic;
using TileBeat.scripts.BeatSystem.BeatSystemGodot.Track;
using TileBeat.scripts.BeatSystem.BeatSystemGodot.Track.Loader;
using TileBeat.scripts.Loaders;
using TileBeat.scripts.Managers;
using TileBeat.scripts.Managers.Beat;

namespace TileBeat.scripts.TestLogic
{
	public partial class LevelMakerTest : Node2D
	{
		public override void _Ready()
		{
			int x = 4;
			int y = 4;
			Sprite2D[,] sprites = new Sprite2D[x, y];

			Sprite2D sprite = SpriteLoader.LoadSprite("E:\\GodotProjects\\TileBeat\\assets\\test\\tile1024.png", 1);
			AudioStream audio = AudioStreamLoader.LoadAudio("E:\\GodotProjects\\BeatSystem\\assets\\music\\tunetank.com_6221_nitro_by_ahoami.mp3");

			GodotTrack gt = new TrackLoader().Load("E:\\GodotProjects\\BeatSystem\\assets\\music\\tunetank.com_6221_nitro_by_ahoami.mp3", 120);

			Sprite2D beatBox = SpriteLoader.LoadSprite("E:\\GodotProjects\\TileBeat\\assets\\test\\beat_box.png", 2);
			Sprite2D beatLine = SpriteLoader.LoadSprite("E:\\GodotProjects\\TileBeat\\assets\\test\\beat_line.png", 1);
			Sprite2D beatMarker = SpriteLoader.LoadSprite("E:\\GodotProjects\\TileBeat\\assets\\test\\beat_marker.png", 0);

            for (int i = 0; i < sprites.GetLength(0); i++)
				for (int j = 0; j < sprites.GetLength(1); j++)
				{
					sprites[i, j] = sprite;
				}

			//Camera2D camera = GetNode<Camera2D>("Camera2D");
			CanvasLayer canvasLayer = GetNode<CanvasLayer>("CanvasLayer");

			//GridManager gm = new GridManager(20, sprites);
			
			Queue<AbstractBeat> beats = new Queue<AbstractBeat>();
			for (uint i = 0; i < 100; i++) beats.Enqueue(new Beat(i));

            BeatManager bm = new BeatManager(30f, 10f, canvasLayer, gt, beatBox.Texture, beatMarker.Texture, beats, 3);
			
            //camera.Position = gm.GridCenter;

            //camera.Zoom = new Vector2(0.09f, 0.09f);

			bm.SetVolume(0.0f);

			//AddChild(gm);
			AddChild(bm);
			
		}
	}
}
