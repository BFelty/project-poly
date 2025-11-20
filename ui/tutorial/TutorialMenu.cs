using System;
using Godot;

namespace LastPolygon.UI.Tutorial;

public partial class TutorialMenu : CanvasLayer
{
	public event Action TutorialMenuClosed;

	private void OnBackButtonPressed()
	{
		Hide();
		TutorialMenuClosed?.Invoke();
	}
}
