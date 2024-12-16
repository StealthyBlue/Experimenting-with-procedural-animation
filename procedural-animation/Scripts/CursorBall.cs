using Godot;
using System;

public partial class CursorBall : CharacterBody2D
{
	
	public override void _Process(double delta)
	{
		this.GlobalPosition = GetViewport().GetMousePosition();
		MoveAndSlide();
	}
}
