using Godot;
using LastPolygon.Globals;
using LastPolygon.Interfaces;
using LastPolygon.Players;

namespace LastPolygon.Game;

public partial class Wall : Area2D
{
	public void OnBodyEntered(Node2D body)
	{
		if (body is IDamageable damageable)
		{
			damageable.TakeDamage(1);
		}
	}
}
