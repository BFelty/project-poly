using Godot;

namespace LastPolygon.Weapons;

public partial class Bullet : Area2D
{
	[Export]
	public float Speed { get; set; }

	[Export]
	public int Damage { get; set; } = 1;

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
	}

	private void HandleMovement(double delta)
	{
		Vector2 velocity = Vector2.Right * Speed;
		Translate(velocity * (float)delta);
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}

	// Frees itself when colliding with any area
	private void OnAreaEntered(Area2D area)
	{
		GD.Print("Area collided with: " + area);
		// TODO - Only allow a bullet to collide once
		QueueFree();
	}
}
