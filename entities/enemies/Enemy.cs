using Godot;

namespace LastPolygon.Enemies;

public partial class Enemy : Area2D
{
	[Export]
	public float Speed { get; set; }

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
	}

	private void HandleMovement(double delta)
	{
		Vector2 velocity = Vector2.Left * Speed;
		Translate(velocity * (float)delta);
	}
}
