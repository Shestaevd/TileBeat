//using BeatSystem;
//using BeatSystem.BeatSystemGodot;
//using BeatSystem.BeatSystemGodot.Track;
//using BeatSystem.BeatTrackingSystem.Domain.Utils;
//using BeatSystem.scripts.BeatSystem.Domain.Track;
//using Godot;

//public partial class Main : Node
//{
//	private BeatGUI _BeatGUI;

//	public static Main Root { get; private set; }

//	public override void _Ready()
//	{
//		Root = this;

//		AudioStreamPlayer player = new AudioStreamPlayer();
//		player.VolumeDb = -20;
//		AddChild(player);

//		_BeatGUI = new BeatGUI(
//			new GodotBeatSystem(new GodotTrack(GD.Load<AudioStream>("res://Deep_House_Summer_Vibes.mp3"), 120), player),
//			GD.Load<Texture2D>("res://Icon.svg"));

//		_BeatGUI.HitTarget += () => GD.Print("Hit");

//		AddChild(_BeatGUI);
//	}
//}
