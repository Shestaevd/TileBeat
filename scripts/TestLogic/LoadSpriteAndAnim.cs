using Godot;
using System.IO;
using TileBeat.scripts.Loaders;

public partial class LoadSpriteAndAnim : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string[] sprites =
			{
				"E:\\UnityProjects\\real_hero\\Assets\\Sprites\\Hero\\Run\\1.png",
				"E:\\UnityProjects\\real_hero\\Assets\\Sprites\\Hero\\Run\\2.png",
				"E:\\UnityProjects\\real_hero\\Assets\\Sprites\\Hero\\Run\\3.png",
				"E:\\UnityProjects\\real_hero\\Assets\\Sprites\\Hero\\Run\\4.png"
			};

		string audio = "E:\\GodotProjects\\BeatSystem\\assets\\music\\house2.mp3";

		AnimatedSprite2D animatedSprite2D = new AnimatedSprite2D();
		animatedSprite2D.SpriteFrames = new SpriteFrames();

		SpriteLoader.LoadAnimation(animatedSprite2D, "testAnim", 1f, sprites);
		animatedSprite2D.SpriteFrames.SetAnimationLoop("testAnim", true);
		AddChild(animatedSprite2D);
		animatedSprite2D.Play("testAnim");

		AudioStream stream = AudioLoader.LoadAudio(audio);
		AudioStreamPlayer2D player = new AudioStreamPlayer2D();
		player.Stream = stream;
		player.VolumeDb = 0.1f;
		AddChild(player);
		player.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
