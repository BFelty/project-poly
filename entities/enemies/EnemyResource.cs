using Godot;
using LastPolygon.Components.Movement;

namespace LastPolygon.Enemies;

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
	public BaseMovementStrategy MovementStrategy { get; set; }

	[Export]
	public float Speed { get; set; }

	// This needs a parameterless constructor for use within Godot's inspector
	public EnemyResource()
		: this(new Color(0, 0, 0, 1), 0, null, 0) { }

	public EnemyResource(
		Color color,
		int health,
		BaseMovementStrategy movementStrategy,
		float speed
	)
	{
		Color = color;
		Health = health;
		MovementStrategy = movementStrategy;
		Speed = speed;
	}
}
