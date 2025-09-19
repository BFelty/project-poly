using Godot;
using LastPolygon.Globals;
using LastPolygon.Players;
using LastPolygon.Weapons;

namespace LastPolygon.Enemies;

public partial class Enemy : Area2D
{
	[Export]
	public float Speed { get; set; }

	[Export]
	private int _maxHealth;

	private int _currentHealth;

	private bool IsDead { get; set; } = false;

	public override void _Ready()
	{
		_currentHealth = _maxHealth;
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
	}

	private void HandleMovement(double delta)
	{
		Vector2 velocity = Vector2.Left * Speed;
		Translate(velocity * (float)delta);
	}

	private void TakeDamage(int damageTaken)
	{
		_currentHealth -= damageTaken;

		if (_currentHealth <= 0)
		{
			Kill();
		}
	}

	public void Kill()
	{
		// Count an enemy as dead once it's health reaches zero, even if it
		// hasn't been deleted from memory yet
		IsDead = true;
		QueueFree();
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is Bullet)
		{
			Bullet bullet = area as Bullet;
			TakeDamage(bullet.Damage);
			GD.Print("Took damage from " + bullet);
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		// Do not allow dead enemies to damage Players
		if (IsDead)
			return;

		if (body is Player)
		{
			GD.Print(
				"Enemy emit signal: " + SignalBus.SignalName.PlayerDamaged
			);
			SignalBus.Instance.EmitSignal(
				SignalBus.SignalName.PlayerDamaged,
				body as Player
			);

			// Each Player only does 1 damage to an enemy. This allows for
			// Players to be sacrificed early on effectively, but that is less
			// viable in later waves
			TakeDamage(1);
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
