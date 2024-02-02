using Godot;
using System.Collections.Generic;
using TileBeat.scripts.Loaders;
using TileBeat.scripts.Managers.Beat;

namespace TileBeat.scripts.TestLogic
{
    public partial class LevelMakerTest : Node2D
	{

		BeatManager bm;
        BeatClassicDrawer bcd;
        AudioStreamPlayer kicker;
		public override void _Ready()
		{
			int x = 4;
			int y = 4;
			Sprite2D[,] sprites = new Sprite2D[x, y];

			Sprite2D sprite = SpriteLoader.LoadSprite("E:\\GodotProjects\\TileBeat\\assets\\test\\tile1024.png", 1);
			AudioStream audio = AudioStreamLoader.LoadAudio("E:\\GodotProjects\\TileBeat\\assets\\test\\2000s-house-kick-single-shot-g-sharp-key-541-44z.mp3");

            GodotTrack gt = new TrackLoader().Load("C:\\Users\\shest\\Downloads\\SXNSTXRM SNOW.mp3", 120);


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

            kicker = new AudioStreamPlayer();

            LinkedList<AbstractBeat> beats = BeatQueueAutoMaker.GenerateBeatsByBpm(gt, 127);
            bm = new BeatManager(gt, beats);
            bcd = new BeatClassicDrawer(beatBox.Texture, beatMarker.Texture, 4f, 4, 0.5f, 30f,  bm);

            //camera.Position = gm.GridCenter;
            //camera.Zoom = new Vector2(0.09f, 0.09f);

            GD.Print(beats.Count);

			bm.VolumeDb += -10;
            kicker.VolumeDb += -10;
            //AddChild(gm);
            kicker.Stream = audio;
            AddChild(kicker);
			AddChild(bm);
			AddChild(bcd);

            bm.Play();
        }

        public override void _Input(InputEvent @event)
        {

            if (@event is InputEventKey eventKey)
            {
                if (!eventKey.Echo && eventKey.Pressed && eventKey.Keycode == Key.Space)
				{
                    kicker.Play();
                    if (bm.UntilNextBeat() < 0.5f)
                        GD.Print("you got it: " + bm.UntilNextBeat());
                    else
                        GD.Print("bad timing: " + bm.UntilNextBeat());
                }
                    
            }
            
        }

        public override void _Process(double delta)
        {

			
            
        }
    }
}
