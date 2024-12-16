using Godot;
using System;

public partial class ProceduralBall : CharacterBody2D
{
	
	[Export] public CharacterBody2D[] target;
	private int CurrentWaypointIndex= 0;
	private Vector2 Xp, X, Xd;
	private Vector2 Y, Yd;
	private float K1, K2, K3;
	AnimatedSprite2D pan;
	
	[Export] public float Z;
	[Export] public float F;
	[Export] public float R;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.GlobalPosition= target[0].GlobalPosition;
		defineVariables();
		pan = GetNode<AnimatedSprite2D>("PlayerAnimation");
	}
	
	public void defineVariables()
	{
		K1 = Z / (Mathf.Pi * F);
		K2 = 1 / ((2 * Mathf.Pi * F)*(2 * Mathf.Pi * F));
		K3 = R * Z / (2 * Mathf.Pi * F);
		
		Xp= target[0].GlobalPosition;
		Y= target[0].GlobalPosition;
		Yd= Vector2.Zero;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		X= target[0].GlobalPosition;
		Xd = (X - Xp) / (float)delta;
		Xp= X;
		
		
		Y= Y+ (float)delta* Yd;
		Yd= Yd+ (float)delta* (X+ K3* Xd- Y- K1* Yd)/ K2;
		
		
		
		this.GlobalPosition= Y;
		this.Velocity= Yd;
		
		if (Y.DistanceTo(X) > 0.5) {
			pan.Rotation= Yd.Angle();
			pan.Play("Walk");
		}
		else{ pan.Play("Idle");}
		
		
		
		MoveAndSlide();
	}
}
