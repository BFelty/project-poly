using Godot;
using LastPolygon.Audio;
using LastPolygon.Components;
using LastPolygon.Components.Movement;
using LastPolygon.Globals;
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
	public Area2D DetectionArea;

	private StandardMovementStrategy _aiMovement = new();
	private PlayerMovementStrategy _playerMovement = new();

	private AnimationPlayer _animationPlayer;
	public bool HasMoved { get; set; } = false;
	private bool _hasShot = false;
	public bool IsControlled { get; set; } = false;

	public override void _Ready()
	{
		// Connect to global events
		EventBus.GameEnded += KillPlayerOnGameEnded;

		// Connect to local events
		// Don't need to disconnect because the subjects and observer are
		// freed at the same time
		_health.ActorDied += HandleDeath;

		DetectionArea = GetNode<Area2D>("Area2D");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	public override void _ExitTree()
	{
		// Disconnect from global events to prevent disposed object errors
		EventBus.GameEnded -= KillPlayerOnGameEnded;
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
		if (IsControlled)
		{
			_playerMovement.Move(this, Speed, delta);
		}
		else
		{
			_aiMovement.Move(this, Speed * 0.6f, delta);
		}
	}

	private void HandleAnimation(double delta)
	{
		if (_hasShot)
		{
			_animationPlayer.Play("shoot");
		}
		else
		{
			if (HasMoved || !IsControlled)
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

		// Set bullet position 11 pixels right of the player's origin
		// This is where the gun barrel ends
		Vector2 bulletOffset = new(11, 0);
		bullet.GlobalPosition = Position + bulletOffset;

		GetTree().CurrentScene.AddChild(bullet);

		AudioManager.Instance.CreateAudio(SoundEffect.SoundEffectType.Gunshot);
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
		// Stop input and other animations
		SetProcess(false);
		SetPhysicsProcess(false);

		_animationPlayer.Play("die");
		AudioManager.Instance.CreateAudio(
			SoundEffect.SoundEffectType.PlayerDeath
		);

		EventBus.InvokePlayerDied(this);
	}

	private void KillPlayerOnGameEnded(int _1, float _2)
	{
		HandleDeath();
	}

	private void OnAreaEntered(Area2D area)
	{
		bool isPlayer = area.GetCollisionLayerValue(1);
		if (isPlayer)
		{
			Player player = area.GetParent<Player>();
			if (player.IsControlled == false)
			{
				GD.Print("invoke player recruited");
				EventBus.InvokePlayerRecruited(player);
			}
			else
			{
				GD.Print("player already controlled");
			}
		}
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		GD.Print("Player freed!");
		QueueFree();
	}
}
