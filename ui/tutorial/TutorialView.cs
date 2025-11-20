using System.Collections.Generic;
using System.Linq;
using Godot;
using LastPolygon.Util;
using LastPolygon.Util.Tutorial;

namespace LastPolygon.UI.Tutorial;

public partial class TutorialView : Tree
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
		TreeItem root = CreateItem();

		foreach (TutorialSection section in tutorial)
		{
			// Display SectionName appropriately
			TreeItem sectionHeader = CreateItem(root);
			sectionHeader.SetText(0, $"{section.SectionName}");
			sectionHeader.SetCustomFontSize(0, 14);
			sectionHeader.SetSelectable(0, false);
			sectionHeader.Collapsed = true;

			foreach (TutorialEntry entry in section.Entries)
			{
				TreeItem entryContent = CreateItem(sectionHeader);

				entryContent.SetText(
					0,
					string.Join(
						"\n\n",
						entry
							.GetType()
							.GetProperties()
							.Select(p => p.GetValue(entry, null)?.ToString())
							.Where(v => v is not null)
					)
				);

				entryContent.SetCustomFontSize(0, 10);
				entryContent.SetAutowrapMode(
					0,
					TextServer.AutowrapMode.WordSmart
				);
				entryContent.SetSelectable(0, false);
			}
		}
	}
}
