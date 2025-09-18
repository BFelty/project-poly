using System.Collections.Generic;
using Godot;
using LastPolygon.Globals;

namespace LastPolygon.Players;

public partial class PlayerManager : Node
{
	private PackedScene _playerSpawnerScene = GD.Load<PackedScene>(
		"uid://ib7e2mistls3"
	);
	private PlayerSpawner _playerSpawner;

	private List<Player> _playerList = new();
	private int _currentPlayerIndex = 0;
	private bool canShoot = true;

	private Timer _fireRateTimer;

	// Seconds until the same Player can shoot again
	[Export]
	public float PlayerFireRate { get; set; }
	private float _sharedFireRate; // _playerFireRate / _playerList.Count

	public override void _Ready()
	{
		// Connect to global signals
		SignalBus.Instance.PlayerPickupCollected += OnPlayerPickupCollected;
		SignalBus.Instance.PlayerDamaged += KillPlayer;

		_playerSpawner = _playerSpawnerScene.Instantiate() as PlayerSpawner;
		AddChild(_playerSpawner);

		_fireRateTimer = FindChild("FireRateTimer") as Timer;
		_fireRateTimer.WaitTime = PlayerFireRate;
	}

	public override void _ExitTree()
	{
		// Disconnect from global signals to prevent disposed object errors
		SignalBus.Instance.PlayerPickupCollected -= OnPlayerPickupCollected;
		SignalBus.Instance.PlayerDamaged -= KillPlayer;
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleShooting();
	}

	public void SpawnPlayer(Vector2 spawnPoint)
	{
		Player newPlayer = _playerSpawner.SpawnPlayer(spawnPoint);
		_playerList.Add(newPlayer); // Track newly-spawned player
		GD.Print("Updated player count: " + _playerList.Count);
		RecalculateFireRateTimerWaitTime();
	}

	public void KillPlayer(Player playerToKill)
	{
		// Ensures a Player's death is not counted multiple times
		if (playerToKill.IsDead)
			return;

		playerToKill.IsDead = true;
		playerToKill.Kill();
		_playerList.Remove(playerToKill);
		GD.Print("Updated player count: " + _playerList.Count);
		RecalculateFireRateTimerWaitTime();

		// Signal that a player died, include _playerCount
		SignalBus.Instance.EmitSignal(
			SignalBus.SignalName.PlayerDied,
			_playerList.Count
		);
	}

	private void OnPlayerPickupCollected(Vector2 collidedPlayerPosition)
	{
		GD.Print("Player at " + collidedPlayerPosition + " collected pickup!");

		Vector2 offset = new(5, 0);
		CallDeferred(MethodName.SpawnPlayer, collidedPlayerPosition + offset);
	}

	private void HandleShooting()
	{
		if (Input.IsActionPressed("shoot"))
		{
			if (canShoot && _playerList.Count > 0)
			{
				canShoot = false;
				_fireRateTimer.Start();

				// Increment the player that is allowed to shoot
				// Make current player shoot
				_currentPlayerIndex =
					(_currentPlayerIndex + 1) % _playerList.Count;
				GD.Print("Current player index: " + _currentPlayerIndex);
				_playerList[_currentPlayerIndex].Shoot();
			}
		}
	}

	private void OnFireRateTimerTimeout()
	{
		canShoot = true;
	}

	// ! According to Godot documentation, Timers are inconsistent when the
	// ! wait time is below 0.05 seconds. This happens when the Player count
	// ! goes above 20. This means I will probably have to implement a
	// ! different method for calculating when a Player can shoot, but I'll
	// ! fix that later.
	private void RecalculateFireRateTimerWaitTime()
	{
		// Each individual Player's fire rate should be roughly equal to
		// the _playerFireRate variable. This equation ensures that
		// Player shooting is evenly spaced
		//
		// Ex. Player fire rate of 1 second
		//     1 Player will shoot every second
		//     2 Players will alternate shooting every 0.5 seconds
		//     4 Players will rotate shooting every 0.25 seconds
		//
		//     In each of these examples, each Player is still only
		//     shooting once every second
		//
		// The shared fire rate will be calculated as positive infinity
		// when the last Player dies, i.e _playerList.Count = 0. I think
		// this is okay since the player shouldn't be shooting when
		// no Player exists within the scene tree

		_sharedFireRate = PlayerFireRate / _playerList.Count;
		_fireRateTimer.WaitTime = _sharedFireRate;
		GD.Print("Current fire rate: " + _fireRateTimer.WaitTime);
	}
}
