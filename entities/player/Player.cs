using Godot;
using LastPolygon.Components;
using LastPolygon.Interfaces;
using LastPolygon.Weapons;

namespace LastPolygon.Players;

public partial class Player : CharacterBody2D, IDamageable
{
	[Export]
	public float Speed { get; set; }

	[Export]
	public PackedScene Weapon { get; set; }

	private HealthComponent _health = new(1);

	private Vector2 _target;

	public override void _Ready()
	{
		// Connect to local events
		// Don't need to disconnect because the subjects and observer are
		// freed at the same time
		_health.ActorDied += HandleDeath;
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
	}

	private void HandleMovement(double delta)
	{
		_target = GetGlobalMousePosition();
		Velocity = Position.DirectionTo(_target) * Speed;
		if (Position.DistanceTo(_target) > 4)
		{
			MoveAndSlide();
		}

		// Keep the player from exiting the viewport
		Position = Position.Clamp(Vector2.Zero, GetViewportRect().Size);
	}

	// ! Something is weird with the physics interpolation and the bullet
	// ! Fix it later when polishing
	public void Shoot()
	{
		Bullet bullet = Weapon.Instantiate() as Bullet;

		// Set bullet position 4 pixels right of the player's origin
		Vector2 bulletOffset = new(4, 0);
		bullet.GlobalPosition = Position + bulletOffset;

		GetTree().CurrentScene.AddChild(bullet);
	}

	public void TakeDamage(int damageTaken)
	{
		_health.TakeDamage(damageTaken);
	}

	public void HandleDeath()
	{
		// Let the PlayerManager handle what happens when a Player dies
		if (GetParent() is PlayerManager playerManager)
		{
			playerManager.OnPlayerDeath(this);
		}
	}
}
