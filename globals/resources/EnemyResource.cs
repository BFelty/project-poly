using Godot;

namespace LastPolygon.Resources;

[GlobalClass]
public partial class EnemyResource : Resource
{
	[Export]
	public int Health { get; set; }

	// This needs a parameterless constructor for use within Godot's inspector
	public EnemyResource()
		: this(0) { }

	public EnemyResource(int health)
	{
		Health = health;
	}
}
