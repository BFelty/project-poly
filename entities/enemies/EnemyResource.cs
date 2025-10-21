using Godot;
using LastPolygon.Components.Movement;

namespace LastPolygon.Enemies;

[GlobalClass]
public partial class EnemyResource : Resource
{
	[Export]
	public int Health { get; set; }

	[Export]
	public BaseMovementStrategy MovementStrategy { get; set; }

	[Export]
	public float Speed { get; set; }

	// This needs a parameterless constructor for use within Godot's inspector
	public EnemyResource()
		: this(0, null, 0) { }

	public EnemyResource(
		int health,
		BaseMovementStrategy movementStrategy,
		float speed
	)
	{
		Health = health;
		MovementStrategy = movementStrategy;
		Speed = speed;
	}
}
