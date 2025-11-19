using System;
using Godot;

namespace LastPolygon.UI.Credits;

public partial class CreditsMenu : CanvasLayer
{
	public event Action CreditsMenuClosed;

	private void OnBackButtonPressed()
	{
		Hide();
		CreditsMenuClosed?.Invoke();
	}
}
