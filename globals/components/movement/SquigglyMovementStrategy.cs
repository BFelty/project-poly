using Godot;

namespace LastPolygon.Components.Movement;

// Enemy moves in a sine wave pattern to the left
// This requires the GlobalClass attribute to be used within Godot's editor
[GlobalClass]
public partial class SquigglyMovementStrategy : BaseMovementStrategy
{
	//
	private float _amplitude = 150f; // Height of wave
	private float _frequency = 0.5f; // Oscillations per second

	private float _time = 0; // x variable

	public override void Move(
		CharacterBody2D objectToMove,
		float speed,
		double delta
	)
	{
		_time += (float)delta;

		// This equation calculates the current offset along a sine wave.
		//
		// Variables:
		//   _amplitude → The maximum height of the wave (controls how far
		//				  up/down it moves).
		//   _frequency → How many full sine cycles occur per second (controls
		// 				  speed of oscillation).
		//   _time      → Elapsed time since the start (used to animate
		// 				  movement and set the correct offset).
		//   Mathf.Tau  → A full rotation in radians (2π). Multiplying
		// 				  frequency * time * Tau converts time into the proper
		// 				  angular position along the wave.
		//
		// The negative sign ( -_amplitude ) flips the wave vertically, which
		// causes the enemy to start it's oscillation upwards. The result is a
		// smoothly oscillating value that moves between -_amplitude and
		// +_amplitude over time, creating sine-wave motion.
		float sine = -_amplitude * Mathf.Sin(_frequency * _time * Mathf.Tau);

		objectToMove.Velocity = new(-speed, sine);

		objectToMove.MoveAndSlide();
	}
}
