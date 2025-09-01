using Godot;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;

	public override void _PhysicsProcess(double delta)
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
}
