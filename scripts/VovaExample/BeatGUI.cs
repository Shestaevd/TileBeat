//using BeatSystem.BeatSystemGodot;
//using Godot;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//public partial class BeatGUI : Control
//{
//	private Texture2D _BeatLineTexture;
//	private GodotBeatSystem _BeatSystem;

//	private float _TextureHeight;
//	private Vector2 _ViewportSize;
//	private Vector2 _BeatLineSize;

//	private float _Interval;
//	private float _Accurancy;
//	private int _CountVisibleBeatLines;

//	private List<BeatPairs> _BeatPairs;

//	public event Action HitTarget;

//    public BeatGUI(GodotBeatSystem beatSystem, Texture2D beatLineTexture, float accurancy = 0.5f, int countVisibleBeatLines = 2)
//	{
//		_Accurancy = accurancy;
//		_BeatSystem = beatSystem;
//		_BeatLineTexture = beatLineTexture;
//		_CountVisibleBeatLines = countVisibleBeatLines;
//    }

//    public override void _Ready()
//	{
//        _Interval = _BeatSystem.GetInterval();
//        _TextureHeight = _BeatLineTexture.GetHeight();

//        _ViewportSize = GetViewportRect().Size;
//        _BeatLineSize = new Vector2((float)(_ViewportSize.X * 0.5f / _CountVisibleBeatLines * _Accurancy), _TextureHeight);

//        _BeatPairs = new List<BeatPairs>();

//        _BeatSystem.OnNextBeat += () =>
//        {
//            BeatPairs pairs = new BeatPairs(_ViewportSize.X * 0.5f / _Interval, _BeatLineTexture);
//            _BeatPairs.Add(pairs);
//        };

//        SetAnchorsPreset(LayoutPreset.FullRect);
//		Resized += () =>
//		{
//			_ViewportSize = GetViewportRect().Size;
//			_BeatLineSize = new Vector2((float)(_ViewportSize.X * 0.5f / _CountVisibleBeatLines * _Accurancy), _TextureHeight);
//        };

//		_BeatSystem.Play();
//	}

//	public override void _Process(double delta)
//	{
//        float nextBeat = _BeatSystem.UntilNextBeat();

//		for (int i = 0; i < _BeatPairs.Count; i++)
//			_BeatPairs[i].Update();
//		_BeatPairs.RemoveAll(x => x.ReadyToDelete);

//		if(Input.IsKeyPressed(Key.Space))
//		{
//			if (_BeatPairs.Count == 0)
//				return;

//			BeatPairs pairs = _BeatPairs.Last();
//			if (pairs.Progress >= _Accurancy)
//				HitTarget?.Invoke();
//		}
//    }


//}
