using System;
using System.Runtime.Serialization.Formatters;
using Godot;
using LastPolygon.Components;
using LastPolygon.Interfaces;
using LastPolygon.Weapons;

namespace LastPolygon.Players;

public partial class Player : CharacterBody2D, IDamageable
{
	private AnimatedSprite2D _animatedSprite;

	[Export]
	public float Speed { get; set; }

	[Export]
	public PackedScene Weapon { get; set; }

	private HealthComponent _health = new(1);

	private Vector2 _target;
	private float _minimumDistanceToMove = 10f;
	private float _movementEpsilonSquared = 1f;
	private bool _hasMoved = false;

	public override void _Ready()
	{
		// Connect to local events
		// Don't need to disconnect because the subjects and observer are
		// freed at the same time
		_health.ActorDied += HandleDeath;

		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
	}

	public override void _Process(double delta)
	{
		HandleAnimation(delta);
	}

	private void HandleMovement(double delta)
	{
		_target = GetGlobalMousePosition();
		Velocity = GlobalPosition.DirectionTo(_target) * Speed;

		if (GlobalPosition.DistanceTo(_target) < _minimumDistanceToMove)
		{
			_hasMoved = false;
		}
		else
		{
			MoveAndSlide();

			// Check if the player actually moved
			if (GetPositionDelta().LengthSquared() > _movementEpsilonSquared)
			{
				_hasMoved = true;
			}
			else
			{
				_hasMoved = false;
			}
		}

		// Keep the player from exiting the viewport
		Position = Position.Clamp(Vector2.Zero, GetViewportRect().Size);
	}

	private void HandleAnimation(double delta)
	{
		if (_hasMoved)
		{
			_animatedSprite.Play("run");
		}
		else
		{
			_animatedSprite.Play("idle");
		}
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
