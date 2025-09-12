using Godot;

namespace LastPolygon.Globals;

public partial class SceneManager : Control
{
	public PackedScene MainMenuScene { get; private set; } =
		GD.Load<PackedScene>("uid://c7xj24s6k4m0g");
	public PackedScene GameScene { get; private set; } =
		GD.Load<PackedScene>("uid://druel3n8wmj3h");

	public static SceneManager Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}

	public void ChangeScene(PackedScene nextScene)
	{
		// This function will usually be called from a signal callback,
		// or some other function from the current scene.
		// Deleting the current scene at this point is
		// a bad idea, because it may still be executing code.
		// This will result in a crash or unexpected behavior.

		// The solution is to defer the load to a later time, when
		// we can be sure that no code from the current scene is running:

		CallDeferred(MethodName.DeferredChangeScene, nextScene);
	}

	private void DeferredChangeScene(PackedScene nextScene)
	{
		// It is now safe to remove the current scene.
		GetTree().CurrentScene.Free();

		// Instance the new scene.
		Node nextNode = nextScene.Instantiate();

		// Add it to the active scene, as child of root.
		GetTree().Root.AddChild(nextNode);

		// Optionally, to make it compatible with the SceneTree.change_scene_to_file() API.
		GetTree().CurrentScene = nextNode;
	}
}
