using Godot;
using System.Collections.Generic;
using System.Linq;
using TileBeat.scripts.BeatSystem.BeatSystemGodot.Track;
using TileBeat.scripts.BeatSystem.BeatSystemGodot.Track.Loader;
using TileBeat.scripts.Loaders;
using TileBeat.scripts.Managers;
using TileBeat.scripts.Managers.Beat;

namespace TileBeat.scripts.TestLogic
{
	public partial class LevelMakerTest : Node2D
	{

		BeatManager bm;
        AudioStreamPlayer kicker;
		public override void _Ready()
		{
			int x = 4;
			int y = 4;
			Sprite2D[,] sprites = new Sprite2D[x, y];

			Sprite2D sprite = SpriteLoader.LoadSprite("E:\\GodotProjects\\TileBeat\\assets\\test\\tile1024.png", 1);
			AudioStream audio = AudioStreamLoader.LoadAudio("E:\\GodotProjects\\TileBeat\\assets\\test\\2000s-house-kick-single-shot-g-sharp-key-541-44z.mp3");

            GodotTrack gt = new TrackLoader().Load("E:\\GodotProjects\\TileBeat\\assets/test/01 - Judgement.mp3", 254);


            Sprite2D beatBox = SpriteLoader.LoadSprite("E:\\GodotProjects\\TileBeat\\assets\\test\\beat_box.png", 2);
			Sprite2D beatLine = SpriteLoader.LoadSprite("E:\\GodotProjects\\TileBeat\\assets\\test\\beat_line.png", 1);
			Sprite2D beatMarker = SpriteLoader.LoadSprite("E:\\GodotProjects\\TileBeat\\assets\\test\\beat_marker.png", 0);

            for (int i = 0; i < sprites.GetLength(0); i++)
				for (int j = 0; j < sprites.GetLength(1); j++)
				{
					sprites[i, j] = sprite;
				}

			//Camera2D camera = GetNode<Camera2D>("Camera2D");

			//GridManager gm = new GridManager(20, sprites);
			
			Queue<AbstractBeat> beats = new Queue<AbstractBeat>();
			for (uint i = 0; i < 100; i++) beats.Enqueue(new Beat(i));

            kicker = new AudioStreamPlayer();
            bm = new BeatManager(30f, 10f, gt, beatBox.Texture, beatMarker.Texture, beats, kicker, 8);

			//camera.Position = gm.GridCenter;

			//camera.Zoom = new Vector2(0.09f, 0.09f);

			bm.VolumeDb -= 10;

            //AddChild(gm);

            

            kicker.Stream = audio;
            AddChild(kicker);
			AddChild(bm);
            bm.Play();
            kicker.Play();
        }

        public override void _Input(InputEvent @event)
        {

            if (@event is InputEventKey eventKey)
            {
                if (!eventKey.Echo && eventKey.Pressed && eventKey.Keycode == Key.Space)
				{
                    kicker.Play();
                    if (bm.IsInTargetBeat())
                        GD.Print("you got it: " + bm.UseNextBeat());
                    else
                        GD.Print("bad timing: " + bm.UseNextBeat());
                }
                    
            }
            
        }

        public override void _Process(double delta)
        {

			GD.Print(bm.CurrentInterval());
            
        }
    }
}
