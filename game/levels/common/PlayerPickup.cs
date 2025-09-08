using Godot;
using LastPolygon.Players;

namespace LastPolygon.Upgrades;

public partial class PlayerPickup : Area2D
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

	public void OnBodyEntered(Node2D body)
	{
		if (body is Player)
		{
			QueueFree();
		}
	}
}
