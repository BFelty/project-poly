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

	[Export]
	private int _maxHealth;
	private int _currentHealth;

	//private HealthComponent health;
	private TextureProgressBar _healthBar;

	private bool _isDead = false;

	public override void _Ready()
	{
		// TODO - New health component implementation
		//health = new HealthComponent();

		_currentHealth = _maxHealth;

		// Initialize health bar
		_healthBar = FindChild("HealthBar") as TextureProgressBar;
		_healthBar.Hide();

		// Change health bar values based on current stats
		_healthBar.MaxValue = _maxHealth;
		_healthBar.Value = _maxHealth;
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
		// Update health variable and health bar
		_currentHealth -= damageTaken;
		_healthBar.Value = _currentHealth;
		_healthBar.Show();

		if (_currentHealth <= 0)
		{
			Kill();
		}
	}

	public void Kill()
	{
		// Count an enemy as dead once it's health reaches zero, even if it
		// hasn't been deleted from memory yet
		_isDead = true;
		QueueFree();
	}

	private void OnBodyEntered(Node2D body)
	{
		// Do not allow dead enemies to damage Players
		if (_isDead)
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
