using Godot;
using LastPolygon.Weapons;

namespace LastPolygon.Players;

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

		// Keep the player from exiting the viewport
		Position = Position.Clamp(Vector2.Zero, GetViewportRect().Size);
	}

	// ! Something is weird with the physics interpolation and the bullet
	// ! Fix it later when polishing
	private void HandleShooting()
	{
		if (Input.IsActionPressed("shoot"))
		{
			Bullet bullet = Weapon.Instantiate() as Bullet;

			// Set bullet position 4 pixels right of the player's origin
			Vector2 bulletOffset = new(4, 0);
			bullet.GlobalPosition = Position + bulletOffset;

			GetParent().AddChild(bullet);
		}
	}
}
