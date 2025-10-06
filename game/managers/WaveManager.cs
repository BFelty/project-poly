using System.Collections.Generic;
using Godot;
using LastPolygon.Enemies;

namespace LastPolygon.Game;

public partial class WaveManager : Node
{
	private static List<EnemyWave> _enemyWaves =
	[
		GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_1.tres"),
		GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_2.tres"),
		GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_3.tres"),
		GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_4.tres"),
		GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_5.tres"),
		GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_6.tres"),
		GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_7.tres"),
		GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_8.tres"),
		GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_9.tres"),
		GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_10.tres"),
		//GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_endless.tres"),
	];
	private int _currentWaveIndex = 0;
	public int CurrentWave
	{
		get { return _currentWaveIndex + 1; }
		private set { _currentWaveIndex = value; }
	}

	// Once all waves are completed, always return the last wave
	private EnemyWave _currentEnemyWave =>
		(_currentWaveIndex < _enemyWaves.Count)
			? _enemyWaves[_currentWaveIndex]
			: _enemyWaves[^1];

	private int _currentEnemySequenceIndex = 0;
	private EnemySequence _currentEnemySequence =>
		_currentEnemyWave.EnemySequences[_currentEnemySequenceIndex];

	private int _enemiesSpawned = 0; // From current enemy sequence

	// TODO - Create endless wave logic. I haven't researched this yet, but it
	// TODO   will most likely involve programmatically creating an enemy wave.
	// TODO   Probably keep track of how many total enemies should spawn, choose
	// TODO   the ratio enemies spawn at, and randomly choose the next enemy.
	// TODO   Each enemy type will probably have a unique spawn delay since
	// TODO   spawning some units rapidly is difficult to deal with (fast enemy).
	// TODO   This spawn delay can then be scaled with a difficulty coefficient.
	// TODO   This will feel less structured than the hard-coded waves, but it
	// TODO   will prevent an entire group of enemies spawning all at once and
	// TODO   not reappearing for a long time. It will also occasionally cause
	// TODO   waves to lack a specific enemy type which could feel good in terms
	// TODO   of variety.
	public (EnemyResource enemy, float spawnDelay) NextEnemyWithDelay()
	{
		EnemyResource enemy = _currentEnemySequence.Enemy;
		float spawnDelay = _currentEnemySequence.SpawnInterval;

		IncrementIndexes();

		return (enemy, spawnDelay);
	}

	private void IncrementIndexes()
	{
		_enemiesSpawned++;
		if (_enemiesSpawned >= _currentEnemySequence.AmountToSpawn)
		{
			_enemiesSpawned = 0;
			_currentEnemySequenceIndex++;
			if (
				_currentEnemySequenceIndex
				>= _currentEnemyWave.EnemySequences.Length
			)
			{
				_currentEnemySequenceIndex = 0;
				_currentWaveIndex++;
			}
		}
	}
}
