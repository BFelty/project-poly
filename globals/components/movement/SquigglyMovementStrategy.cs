using Godot;

namespace LastPolygon.Components.Movement;

// Enemy moves in a sine wave pattern towards the left
// This requires the GlobalClass attribute to be used within Godot's editor
[GlobalClass]
public partial class SquigglyMovementStrategy : BaseMovementStrategy
{
	private float _amplitude = 20f; // Height of wave
	private float _frequency = 2f; // Oscillations per second

	private float _time = 0;

	public override void Move(
		CollisionObject2D objectToMove,
		float speed,
		double delta
	)
	{
		//Vector2 velocity = Vector2.Left * speed;
		//objectToMove.Translate(velocity * (float)delta);

		_time += (float)delta;

		// TODO - Still need to figure out this sine function
		Vector2 velocity = new(
			-speed,
			_amplitude * Mathf.Sin(_time * _frequency / Mathf.Tau)
		);

		objectToMove.Translate(velocity * (float)delta);
	}
}
