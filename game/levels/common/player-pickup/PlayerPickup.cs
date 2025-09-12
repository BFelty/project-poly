using Godot;
using LastPolygon.Globals;
using LastPolygon.Players;

namespace LastPolygon.Upgrades;

public partial class PlayerPickup : Area2D
{
	[Export]
	public float Speed { get; set; }

	private bool _isCollected = false;

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
	}

	private void HandleMovement(double delta)
	{
		Vector2 velocity = Vector2.Left * Speed;
		Translate(velocity * (float)delta);
	}

	private void OnBodyEntered(Node2D body)
	{
		// Check if the PlayerPickup has already been collected
		// This prevents the same pickup from being used multiple times
		if (_isCollected)
			return;

		if (body is Player)
		{
			_isCollected = true;

			// Emit signal from SignalBus autoload
			GD.Print(
				"PlayerPickup emit signal: "
					+ SignalBus.SignalName.PlayerPickupCollected
			);
			SignalBus.Instance.EmitSignal(
				SignalBus.SignalName.PlayerPickupCollected,
				body.GlobalPosition
			);

			QueueFree();
		}
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}
