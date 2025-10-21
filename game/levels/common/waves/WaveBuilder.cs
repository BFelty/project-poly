using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using LastPolygon.Enemies;

namespace LastPolygon.Game;

public partial class WaveBuilder : Resource
{
	private PackedScene _fastEnemy = GD.Load<PackedScene>(
		"uid://beruth1xqiyrr"
	);
	private PackedScene _squigglyEnemy = GD.Load<PackedScene>(
		"uid://d231d7w7bdcg0"
	);
	private PackedScene _standardEnemy = GD.Load<PackedScene>(
		"uid://bvi40cj37pqpc"
	);
	private PackedScene _tankEnemy = GD.Load<PackedScene>(
		"uid://cp1apyd5u3h5i"
	);

	// TODO - This procedural wave generation does not have a variable for
	// TODO   difficulty scaling. More enemies spawn each wave, but that only
	// TODO   makes it more difficult when there are a lot of tanks. To
	// TODO   increase the difficulty, I could implement a difficulty modifier
	// TODO   that scales off the current wave. This could make the enemies
	// TODO   spawn faster and/or have more health as the player progresses
	// TODO   through waves. This will probably offer enough challenge to the
	// TODO   player since the only scaling they have access to is increasing
	// TODO   the amount of troops they control by collecting PlayerPickups.
	public EnemyWave BuildEnemyWave(int currentWave)
	{
		int totalEnemies = currentWave * 10;
		// ! TEST - Check totalEnemies
		GD.Print($"Total enemies: {totalEnemies}");

		Random r = new();

		// Enemy dictionary with unnormalized ratio of enemies.
		//
		// Instead of using a double to represent a ratio, I use integers
		// 0 to 100. This allows me to later set the exact amount of enemies
		// that should spawn without needing to create a new dictionary.
		Dictionary<PackedScene, int> enemiesToSpawn = new()
		{
			{ _fastEnemy, r.Next(1, 100) },
			{ _squigglyEnemy, r.Next(1, 100) },
			{ _standardEnemy, r.Next(1, 100) },
			{ _tankEnemy, r.Next(1, 100) },
		};

		// ! TEST - Check enemiesToSpawn initialization
		GD.Print("\nPercentage of each enemy");
		foreach (var enemy in enemiesToSpawn)
		{
			GD.Print($"Key: {enemy.Key}, Value: {enemy.Value}");
		}

		double ratioSum = enemiesToSpawn.Values.Sum();

		// Normalize ratios and multiply by total enemies to get the amount of
		// each enemy to spawn. Round to the next int. This will NOT result in
		// wave sizes incrementing perfectly, but it's close enough.
		foreach (var enemy in enemiesToSpawn)
		{
			enemiesToSpawn[enemy.Key] = (int)
				Math.Ceiling(enemy.Value / ratioSum * totalEnemies);
		}

		// ! TEST - Check enemiesToSpawn wave normalization
		GD.Print("\nNormalized amount of each enemy");
		foreach (var enemy in enemiesToSpawn)
		{
			GD.Print($"Key: {enemy.Key}, Value: {enemy.Value}");
		}

		List<EnemySequence> enemySequences = [];

		while (enemiesToSpawn.Count > 0)
		{
			// Random enemy from enemiesToSpawn
			PackedScene nextEnemy = enemiesToSpawn
				.ElementAt(r.Next(0, enemiesToSpawn.Count))
				.Key;

			// Create new EnemySequence
			EnemySequence nextEnemySequence = new()
			{
				EnemyScene = nextEnemy,

				AmountToSpawn = r.Next(
					1,
					Math.Min(5, enemiesToSpawn[nextEnemy])
				),

				//// Fast enemies spawn slower, otherwise they'd be too difficult
				//// Slow enemies spawn in clusters, causing tank walls that need
				//// to be dealt with
				// ! This used to scale off the enemy speed, but that became
				// ! harder when I refactored waves to use PackedScenes instead
				// ! of EnemyResources. I may change this later.
				SpawnInterval = 0.5f,
			};

			// Decrement enemiesToSpawn, remove key-value pair if all enemies
			// of that type have been accounted for in new sequence
			enemiesToSpawn[nextEnemy] -= nextEnemySequence.AmountToSpawn;
			if (enemiesToSpawn[nextEnemy] <= 0)
			{
				enemiesToSpawn.Remove(nextEnemy);
			}

			// Append sequence until all enemies have been accounted for
			enemySequences.Add(nextEnemySequence);

			// ! TEST - Check the enemy sequence that was randomly generated
			GD.Print(
				$"Enemy: {nextEnemySequence.EnemyScene}, "
					+ $"Amount: {nextEnemySequence.AmountToSpawn}, "
					+ $"Spawn Interval: {nextEnemySequence.SpawnInterval}"
			);
		}

		return new() { EnemySequences = [.. enemySequences] };
	}
}
