using System.Collections.Generic;
using Godot;
using LastPolygon.Util;
using LastPolygon.Util.Tutorial;

public partial class TutorialView : VBoxContainer
{
	public override void _Ready()
	{
		string tutorialJson = FileAccess
			.Open("res://ui/tutorial/tutorial.json", FileAccess.ModeFlags.Read)
			?.GetAsText();

		List<TutorialSection> tutorial = JsonUtils.Deserialize<
			List<TutorialSection>
		>(tutorialJson);

		GenerateTutorialUi(tutorial);
	}

	public void GenerateTutorialUi(List<TutorialSection> tutorial)
	{
		// TODO
	}
}
