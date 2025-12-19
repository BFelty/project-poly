using Godot;

public partial class Settings : Node
{
	public static Settings Instance { get; private set; }

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("fullscreen_toggle"))
		{
			ToggleFullscreen();
		}
	}

	public void ToggleFullscreen()
	{
		bool isFullscreen = GetTree().Root.Mode == Window.ModeEnum.Fullscreen;

		if (isFullscreen)
		{
			GetTree().Root.Mode = Window.ModeEnum.Windowed;
		}
		else
		{
			GetTree().Root.Mode = Window.ModeEnum.Fullscreen;
		}
	}
}
