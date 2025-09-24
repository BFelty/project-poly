using Godot;

namespace LastPolygon.Resources;

[GlobalClass]
public partial class EnemyResource : Resource
{
	// ! This is a temporary variable to distinguish between enemies visually
	// ! before I add better art
	[Export]
	public Color Color { get; set; }

	[Export]
	public int Health { get; set; }

	[Export]
	public float Speed { get; set; }

	// This needs a parameterless constructor for use within Godot's inspector
	public EnemyResource()
		: this(0, 0) { }

	public EnemyResource(int health, float speed)
	{
		Health = health;
		Speed = speed;
	}
}
