using Godot;

namespace LastPolygon.Components;

public partial class HealthComponent : Resource
{
	// TODO - Move health-related variables and functions here
	[Signal]
	public delegate void ActorDiedEventHandler();

	public int MaxHealth { get; set; }
	public int CurrentHealth { get; private set; }

	public bool IsAlive => CurrentHealth > 0;

	public HealthComponent(int maxHealth = 1)
	{
		MaxHealth = maxHealth;
		CurrentHealth = maxHealth;
	}

	public void TakeDamage(int damageTaken)
	{
		// Update health and signal if dead
		CurrentHealth -= damageTaken;
		if (!IsAlive)
		{
			EmitSignal(SignalName.ActorDied);
		}
	}
}
