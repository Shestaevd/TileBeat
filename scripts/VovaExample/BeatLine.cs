using Godot;
using System.Diagnostics;

public partial class BeatLine : TextureRect
{
	private float _speed;
	private float _offset;
	private Vector2 _center;
	private Vector2 _start;
	private float _currentTime = 0f;
	private float _targetTime;
	public bool IsTwin { get; }

	public BeatLine(Vector2 center, float _targetTime, bool twin, float speed, Texture2D texture)
	{
		IsTwin = twin;
        _center = center;
        Texture = texture;
        _speed = twin ? speed * -1 : speed;
	}

	public override void _Ready()
	{
		_start = new Vector2(IsTwin ? GetViewportRect().Size.X : 0, _center.Y);
        Position = _start;
    }

	public override void _Process(double delta)
	{
		_currentTime += (float) delta;
        Position = _start.Lerp(_center, _currentTime / _targetTime);
	}
}