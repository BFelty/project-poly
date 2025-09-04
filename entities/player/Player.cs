using System;
using Godot;
using LastPolygon.Weapon;

namespace LastPolygon.Player;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed { get; set; }

	[Export]
	public PackedScene Weapon { get; set; }

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
		HandleShooting();
	}

	private void HandleMovement(double delta)
	{
		Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Input
			.GetVector("move_left", "move_right", "move_up", "move_down")
			.Normalized();

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			float decelerationRate = Speed * (float)delta * 10; // 1/6th of a second
			velocity.X = Mathf.MoveToward(Velocity.X, 0, decelerationRate);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, decelerationRate);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void HandleShooting()
	{
		if (Input.IsActionPressed("shoot"))
		{
			Bullet bullet = Weapon.Instantiate() as Bullet;
			Vector2 bulletOffset = new(4, 0); // 4 pixels to the right of the player
			bullet.GlobalPosition = Position + bulletOffset;
			GetParent().AddChild(bullet);
		}
	}
}
