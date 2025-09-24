using System;
using Godot;
using LastPolygon.Interfaces;

namespace LastPolygon.Components;

public partial class HealthComponent : Resource, IDamageable
{
	public event Action ActorDied;

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
		// Update health and invoke event if dead
		CurrentHealth -= damageTaken;
		if (!IsAlive)
		{
			ActorDied?.Invoke();
		}
	}
}
