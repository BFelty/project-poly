using Godot;
using LastPolygon.Components;
using LastPolygon.Globals;
using LastPolygon.Interfaces;
using LastPolygon.Players;

namespace LastPolygon.Enemies;

public partial class Enemy : Area2D, IDamageable
{
	[Export]
	public float Speed { get; set; }

	private HealthComponent _health;
	private TextureProgressBar _healthBar;

	public override void _Ready()
	{
		_health = new HealthComponent();

		// Connect to local signals
		// Don't need to disconnect because the subjects and observer are
		// freed at the same time
		_health.ActorDied += HandleDeath;

		// Initialize health bar
		_healthBar = FindChild("HealthBar") as TextureProgressBar;
		_healthBar.Hide();

		// Change health bar values based on current stats
		_healthBar.MaxValue = _health.MaxHealth;
		_healthBar.Value = _health.CurrentHealth;
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

	public void TakeDamage(int damageTaken)
	{
		// Update health component and health bar
		_health.TakeDamage(damageTaken);
		_healthBar.Value = _health.CurrentHealth;
		_healthBar.Show();
	}

	public void HandleDeath()
	{
		QueueFree();
	}

	private void OnBodyEntered(Node2D body)
	{
		// Do not allow dead enemies to damage Players
		if (!_health.IsAlive)
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
