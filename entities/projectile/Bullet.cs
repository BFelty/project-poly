using Godot;
using LastPolygon.Audio;
using LastPolygon.Globals;
using LastPolygon.Interfaces;

namespace LastPolygon.Weapons;

public partial class Bullet : Area2D
{
	[Export]
	public float Speed { get; set; }

	[Export]
	private int _damage = 1;

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
	}

	private void HandleMovement(double delta)
	{
		Vector2 velocity = Vector2.Right * Speed;
		Translate(velocity * (float)delta);
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}

	// Frees itself when colliding with any area
	private void OnBodyEntered(Node2D body)
	{
		if (body is IDamageable damageable)
		{
			damageable.TakeDamage(_damage);
			AudioManager.Instance.CreateAudio(
				SoundEffect.SoundEffectType.GunshotImpact
			);
		}
		QueueFree();
	}
}
