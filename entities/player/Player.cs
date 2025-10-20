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
	private float _minimumDistanceToMove = 10f;
	private float _movementEpsilonSquared = 1f;

	private AnimationPlayer _animationPlayer;
	private bool _hasMoved = false;
	private bool _hasShot = false;

	private Timer _despawnTimer;

	public override void _Ready()
	{
		// Connect to local events
		// Don't need to disconnect because the subjects and observer are
		// freed at the same time
		_health.ActorDied += HandleDeath;

		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_despawnTimer = GetNode<Timer>("DespawnTimer");
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
			_hasMoved =
				GetPositionDelta().LengthSquared() > _movementEpsilonSquared;
		}

		// Keep the player from exiting the viewport
		Position = Position.Clamp(Vector2.Zero, GetViewportRect().Size);
	}

	private void HandleAnimation(double delta)
	{
		if (_hasShot)
		{
			_animationPlayer.Play("shoot");
		}
		else
		{
			if (_hasMoved)
			{
				_animationPlayer.Play("run");
			}
			else
			{
				_animationPlayer.Play("idle");
			}
		}
	}

	// ! Something is weird with the physics interpolation and the bullet
	// ! Fix it later when polishing
	public void Shoot()
	{
		_hasShot = true;

		Bullet bullet = Weapon.Instantiate() as Bullet;

		// Set bullet position 4 pixels right of the player's origin
		Vector2 bulletOffset = new(4, 0);
		bullet.GlobalPosition = Position + bulletOffset;

		GetTree().CurrentScene.AddChild(bullet);
	}

	private void OnAnimationPlayerAnimationFinished(StringName animName)
	{
		if (animName == "shoot")
		{
			_hasShot = false;
		}
	}

	public void TakeDamage(int damageTaken)
	{
		_health.TakeDamage(damageTaken);
	}

	public void HandleDeath()
	{
		// Stop input and interactions with player
		SetProcess(false);
		SetPhysicsProcess(false);
		GetNode<CollisionShape2D>("CollisionShape2D")
			.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);

		_animationPlayer.Play("die");

		// Let the PlayerManager know a player has died
		if (GetParent() is PlayerManager playerManager)
		{
			playerManager.OnPlayerDeath(this);
		}

		_despawnTimer.Start();
	}

	private void OnDespawnTimerTimeout()
	{
		QueueFree();
	}
}
