//using Godot;
//using System;
//using System.Collections.Generic;

//public partial class Main : Node2D
//{
//	public const int MaxCount = 20;

//	public List<Beat> Beats { get; set; }

//	// Called when the node enters the scene tree for the first time.
//	public override void _Ready()
//	{
//		Beats = new List<Beat>();
//	}

//	// Called every frame. 'delta' is the elapsed time since the previous frame.
//	public override void _Process(double delta)
//	{
//		Beats.ForEach(x => x.Update((float)delta));
//		QueueRedraw();
//		Beats.RemoveAll(x => x.ReadyToDelete);
//	}

//    public override void _Input(InputEvent input)
//    {
//		if (input is InputEventKey key && key.Pressed && key.Keycode == Key.Space)
//			Beats.Add(new Beat(2));
//    }

//    public override void _Draw()
//    {
//        float width = GetViewportRect().Size.X;
//        float heigth = GetViewportRect().Size.Y;

//		float beatWidth = width / MaxCount * 0.5f;

//        for(int i = 0; i <  Beats.Count; i++)
//		{
//			Rect2 rect = new Rect2();
//			rect.Size = new Vector2(beatWidth, 30);
//			rect.Position = new Vector2(Beats[i].Delta * (width / 2), heigth - 30);

//			DrawRect(rect, Colors.Green);

//			rect.Position = new Vector2((1 - Beats[i].Delta) * (width / 2) + width * 0.5f, heigth - 30);
//            DrawRect(rect, Colors.Green);
//        }
//    }
//}

//public class Beat
//{
//    private float _TargetTime;
//    private float _CurrentDelta;

//	public bool ReadyToDelete
//		=> _TargetTime <= _CurrentDelta;

//    public Beat(float targetTime)
//    {
//        _TargetTime = targetTime;
//    }

//    public float Delta { get; private set; }

//    public void Update(float delta)
//	{
//		_CurrentDelta += delta;
//		Delta = _CurrentDelta / _TargetTime;
//	}
//}