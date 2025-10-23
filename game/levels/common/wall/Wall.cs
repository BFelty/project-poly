using Godot;
using LastPolygon.Interfaces;

namespace LastPolygon.Game;

public partial class Wall : StaticBody2D
{
	public void OnBodyEntered(Node2D body)
	{
		// ! Originally, I wanted the edges of the map to be kill zones for the
		// ! player, but this ended up being an annoying game mechanic. I still
		// ! need to get close to the wall, but I have to be very careful as to
		// ! not kill myself. It's very unfun to pay attention to.
		/*if (body is IDamageable damageable)
		{
			damageable.TakeDamage(1);
		}*/
	}
}
