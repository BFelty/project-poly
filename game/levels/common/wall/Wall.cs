using Godot;
using LastPolygon.Interfaces;

namespace LastPolygon.Game;

public partial class Wall : StaticBody2D
{
	public void OnBodyEntered(Node2D body)
	{
		if (body is IDamageable damageable)
		{
			damageable.TakeDamage(1);
		}
	}
}
