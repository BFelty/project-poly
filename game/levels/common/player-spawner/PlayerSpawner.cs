using Godot;

namespace LastPolygon.Players;

public partial class PlayerSpawner : Node2D
{
	private PackedScene _playerScene = GD.Load<PackedScene>(
		"uid://osokqpha3208"
	);

	public void SpawnPlayer(Vector2 spawnPoint)
	{
		Player player = _playerScene.Instantiate() as Player;
		player.GlobalPosition = spawnPoint;
		GetParent().AddChild(player);
	}
}
