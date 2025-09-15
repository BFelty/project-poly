using System.Runtime.Serialization.Formatters;
using Godot;
using LastPolygon.Weapons;

namespace LastPolygon.Players;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed { get; set; }

	[Export]
	public PackedScene Weapon { get; set; }

	private Timer _fireRateTimer;

	// How many seconds until the next shot is allowed
	[Export]
	private float FireRate { get; set; }
	private bool canShoot = true;

	public bool IsDead { get; set; } = false;

	private Vector2 _target;

	public override void _Ready()
	{
		_fireRateTimer = FindChild("FireRateTimer") as Timer;
		_fireRateTimer.WaitTime = FireRate;
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
		HandleShooting();
	}

	public void Kill()
	{
		QueueFree();
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
	private void HandleShooting()
	{
		if (Input.IsActionPressed("shoot"))
		{
			if (canShoot)
			{
				canShoot = false;
				_fireRateTimer.Start();

				Bullet bullet = Weapon.Instantiate() as Bullet;

				// Set bullet position 4 pixels right of the player's origin
				Vector2 bulletOffset = new(4, 0);
				bullet.GlobalPosition = Position + bulletOffset;

				GetTree().CurrentScene.AddChild(bullet);
			}
		}
	}

	private void OnFireRateTimerTimeout()
	{
		_fireRateTimer.Stop();
		canShoot = true;
	}
}
