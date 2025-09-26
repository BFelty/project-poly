using Godot;
using LastPolygon.Components;
using LastPolygon.Components.Movement;
using LastPolygon.Globals;
using LastPolygon.Interfaces;
using LastPolygon.Resources;

namespace LastPolygon.Enemies;

public partial class Enemy : CharacterBody2D, IDamageable
{
	// Data on the enemy variant
	public EnemyResource EnemyData { get; set; }

	private Polygon2D enemyShape;

	private HealthComponent _health;
	private TextureProgressBar _healthBar;

	private BaseMovementStrategy _movementStrategy;
	private float _speed;

	private void Initialize()
	{
		enemyShape.Color = EnemyData.Color; // ! Temporary
		_health = new(EnemyData.Health);
		_movementStrategy =
			EnemyData.MovementStrategy.Duplicate() as BaseMovementStrategy;
		_speed = EnemyData.Speed;
	}

	public override void _Ready()
	{
		// Get references to required Enemy children
		// ! Temporary reference until I replace programmer art
		enemyShape = FindChild("Polygon2D") as Polygon2D;

		// Set up enemy from EnemyResource
		Initialize();

		// Connect to local events
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
		_movementStrategy.Move(this, _speed, delta);
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

		if (body is IDamageable damageable)
		{
			// Damage the body the Enemy collided with
			damageable.TakeDamage(1); // TODO - Implement attack component maybe

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
		GD.Print("Enemy emit event: EnemyLeak");
		EventBus.InvokeEnemyLeak();

		QueueFree();
	}
}
