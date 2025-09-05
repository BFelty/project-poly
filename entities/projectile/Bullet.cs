using Godot;

namespace LastPolygon.Weapon;

public partial class Bullet : Area2D
{
	[Export]
	public float Speed { get; set; }

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

	private void OnBodyEntered(Node2D body)
	{
		QueueFree();
	}
}
