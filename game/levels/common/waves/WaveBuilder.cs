using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using LastPolygon.Enemies;

namespace LastPolygon.Game;

public partial class WaveBuilder : Resource
{
	private EnemyResource _fastEnemy = GD.Load<EnemyResource>(
		"uid://bi0ua2cyvm5de"
	);
	private EnemyResource _squigglyEnemy = GD.Load<EnemyResource>(
		"uid://dvmgdo4hy8kva"
	);
	private EnemyResource _standardEnemy = GD.Load<EnemyResource>(
		"uid://nkjwo8at1k5"
	);
	private EnemyResource _tankEnemy = GD.Load<EnemyResource>(
		"uid://dee0ubqwluivp"
	);

	// TODO - This procedural wave generation does not scale in difficulty. It
	// TODO   spawns more enemies per wave, but that doesn't really make it
	// TODO   more difficult; It just makes each wave longer. To increase the
	// TODO   difficulty, I could implement a difficulty modifier that scales
	// TODO   off the current wave. This could make the enemies spawn faster as
	// TODO   the player beats each wave. This will probably be enough of a
	// TODO   challenge since I don't intend on making PlayerPickups spawn
	// TODO   more frequently.
	public EnemyWave BuildEnemyWave(int currentWave)
	{
		// Start at 80 enemies, increment by 10 every wave
		int totalEnemies = currentWave * 10; //- 30;
		// ! Check totalEnemies; Delete later
		GD.Print($"Total enemies: {totalEnemies}");

		// Create default dictionary with unnormalized ratio of enemies
		Random r = new();

		Dictionary<EnemyResource, int> enemiesToSpawn = new()
		{
			{ _fastEnemy, r.Next(0, 100) },
			{ _squigglyEnemy, r.Next(0, 100) },
			{ _standardEnemy, r.Next(0, 100) },
			{ _tankEnemy, r.Next(0, 100) },
		};

		// ! Test enemiesToSpawn initialization; Delete later
		GD.Print("\nPercentage of each enemy");
		foreach (var enemy in enemiesToSpawn)
		{
			GD.Print($"Key: {enemy.Key}, Value: {enemy.Value}");
		}

		// Normalize ratios and multiply by total enemies to get the amount of
		// each enemy to spawn. Round to the next int. This will NOT result in
		// wave sizes incrementing perfectly, but it's close enough.
		double ratioSum = enemiesToSpawn.Values.Sum();

		foreach (var enemy in enemiesToSpawn)
		{
			enemiesToSpawn[enemy.Key] = (int)
				Math.Ceiling(enemy.Value / ratioSum * totalEnemies);
		}

		// ! Test enemiesToSpawn wave normalization; Delete later
		GD.Print("\nNormalized percentage of each enemy");
		foreach (var enemy in enemiesToSpawn)
		{
			GD.Print($"Key: {enemy.Key}, Value: {enemy.Value}");
		}

		List<EnemySequence> enemySequences = [];

		while (enemiesToSpawn.Count > 0)
		{
			// Random enemy from enemiesToSpawn
			EnemyResource nextEnemy = enemiesToSpawn
				.ElementAt(r.Next(0, enemiesToSpawn.Count))
				.Key;

			// Create new EnemySequence
			EnemySequence nextEnemySequence = new()
			{
				Enemy = nextEnemy,

				AmountToSpawn = r.Next(
					1,
					Math.Min(10, enemiesToSpawn[nextEnemy])
				),

				// Set spawn delay for each type of enemy:
				//   default delay / enemy type modifier * wave difficulty modifier
				//   Temp spawn delay: enemy speed / 200
				SpawnInterval = nextEnemy.Speed / 200,
			};

			// Decrement enemiesToSpawn, remove key-value pair if all enemies
			// have been accounted for in new sequence
			enemiesToSpawn[nextEnemy] -= nextEnemySequence.AmountToSpawn;
			if (enemiesToSpawn[nextEnemy] <= 0)
			{
				enemiesToSpawn.Remove(nextEnemy);
			}

			// Append sequence to wave until all enemies are accounted for
			enemySequences.Add(nextEnemySequence);

			// ! Check the enemy sequence that was randomly generated
			GD.Print(
				$"Enemy: {nextEnemySequence.Enemy}, Amount: {nextEnemySequence.AmountToSpawn}, Spawn Interval: {nextEnemySequence.SpawnInterval}"
			);
		}

		return new() { EnemySequences = [.. enemySequences] };
	}
}
