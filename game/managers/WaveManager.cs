using System.Collections.Generic;
using Godot;
using LastPolygon.Enemies;

namespace LastPolygon.Game;

public partial class WaveManager : Node
{
	private WaveBuilder _waveBuilder = new();
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
	];
	private int _currentWaveIndex = 0;
	public int CurrentWave
	{
		get { return _currentWaveIndex + 1; }
		private set { _currentWaveIndex = value; }
	}

	// Once all waves are completed, append a randomly generated wave
	private EnemyWave CurrentEnemyWave =>
		(_currentWaveIndex < _enemyWaves.Count)
			? _enemyWaves[_currentWaveIndex]
			: GetAndAppendNewEnemyWave(); // TODO - Append random wave

	private int _currentEnemySequenceIndex = 0;
	private EnemySequence CurrentEnemySequence =>
		CurrentEnemyWave.EnemySequences[_currentEnemySequenceIndex];

	private int _enemiesSpawned = 0; // From current enemy sequence

	private EnemyWave GetAndAppendNewEnemyWave()
	{
		_enemyWaves.Add(_waveBuilder.BuildEnemyWave(CurrentWave));
		return _enemyWaves[^1];
	}

	public (EnemyResource enemy, float spawnDelay) NextEnemyWithDelay()
	{
		EnemyResource enemy = CurrentEnemySequence.Enemy;
		float spawnDelay = CurrentEnemySequence.SpawnInterval;

		IncrementIndexes();

		return (enemy, spawnDelay);
	}

	private void IncrementIndexes()
	{
		_enemiesSpawned++;
		if (_enemiesSpawned >= CurrentEnemySequence.AmountToSpawn)
		{
			_enemiesSpawned = 0;
			_currentEnemySequenceIndex++;
			if (
				_currentEnemySequenceIndex
				>= CurrentEnemyWave.EnemySequences.Length
			)
			{
				_currentEnemySequenceIndex = 0;
				_currentWaveIndex++;
			}
		}
	}
}
