using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class CruxMenu : MonoBehaviour
{
	[MenuItem ("Window/Crux/Create/Create New Crux System", false, 1)]
	static void CreateNewSystem () 
	{
		GameObject CruxSystem = (GameObject)Instantiate (Resources.Load("New Crux System"), new Vector3(0, 0, 0),  Quaternion.identity);
		CruxSystem.transform.position = new Vector3 (0, 0, 0);
		CruxSystem.gameObject.name = "Crux";
	}

	[MenuItem ("Window/Crux/Create/Create Demo Crux System", false, 1)]
	static void CreateDemoSystem () 
	{
		GameObject CruxSystem = (GameObject)Instantiate (Resources.Load("Crux Demo System"), new Vector3(0, 0, 0),  Quaternion.identity);
		CruxSystem.transform.position = new Vector3 (0, 0, 0);
		CruxSystem.gameObject.name = "Crux Demo System";
	}

	[MenuItem ("Window/Crux/Documentation/Documentation", false, 100)]
	static void CruxDocumentationHome ()
	{
		Application.OpenURL("http://crux-procedural-ai-spawner.wikia.com/wiki/Documentation");
	}

	[MenuItem ("Window/Crux/Documentation/Wiki", false, 100)]
	static void CruxDocumentationWiki ()
	{
		Application.OpenURL("http://crux-procedural-ai-spawner.wikia.com/wiki/Crux_-_Procedural_AI_Spawner_Wiki");
	}

	[MenuItem ("Window/Crux/Documentation/Tutorials", false, 100)]
	static void CruxDocumentationTutorials ()
	{
		Application.OpenURL("http://crux-procedural-ai-spawner.wikia.com/wiki/Tutorials");
	}

	[MenuItem ("Window/Crux/Documentation/Solutions to Possible Issues", false, 100)]
	static void CruxDocumentationSTPI ()
	{
		Application.OpenURL("http://crux-procedural-ai-spawner.wikia.com/wiki/Solutions_to_Possible_Issues");
	}
}
