using Godot;
using LastPolygon.Globals;
using LastPolygon.Players;
using LastPolygon.Weapons;

namespace LastPolygon.Enemies;

public partial class Enemy : Area2D
{
	[Export]
	public float Speed { get; set; }

	public bool IsDead { get; set; } = false;

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
	}

	public void Kill()
	{
		QueueFree();
	}

	private void HandleMovement(double delta)
	{
		Vector2 velocity = Vector2.Left * Speed;
		Translate(velocity * (float)delta);
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is Bullet)
		{
			Kill();
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		// Only allows an enemy to kill a Player once
		if (IsDead)
			return;

		if (body is Player)
		{
			IsDead = true;

			GD.Print(
				"Enemy emit signal: " + SignalBus.SignalName.PlayerDamaged
			);
			SignalBus.Instance.EmitSignal(
				SignalBus.SignalName.PlayerDamaged,
				body as Player
			);

			QueueFree();
		}
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		// Alert the game that an enemy has gone off screen
		// ! At the time of writing, this can only happen when the enemy gets
		// ! past the player, which should make the player lose.
		GD.Print("Enemy emit signal: " + SignalBus.SignalName.EnemyLeak);
		SignalBus.Instance.EmitSignal(SignalBus.SignalName.EnemyLeak);

		QueueFree();
	}
}
