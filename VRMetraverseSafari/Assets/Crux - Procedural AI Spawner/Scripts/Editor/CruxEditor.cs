using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Crux))]
public class CruxEditor : Editor 
{
	enum GenerationType
	{
		Random = 0,
		Odds = 1
	}

	enum PlayerTransformType
	{
		Stardard = 1,
		Instantiated = 2
	}

	enum UseObjectPooling
	{
		Yes = 1,
		No = 2
	}

	enum SpawnType
	{
		Standard = 0,
		Nodes = 1
	}

	Editor gameObjectEditor;

	enum displayFieldType {DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields}
	displayFieldType DisplayFieldType;

	Crux t;
	SerializedObject GetTarget;
	SerializedProperty Biome;
	int ListSize;
	bool RegenerateSpawnIDs = false;
	bool DisplayMessage = false;

	SerializedProperty TabNumberTop_SP;
	SerializedProperty CategoryTabNumber_SP;

	public string[] TabName = new string[] {"Wildlife", "Creatures", "NPCs"};
	public string[] TabNameTop = new string[] {"Biome Options", "Spawning Options"};
	public string[] CategoryNames = new string[]{};
	public string[] ObjectNames = new string[]{};
	public string[] SpawnIds = new string[]{};
	public string NewSpawnID;
	public bool FoldOuts = true;

	GenerationType GenerationTypeEnum = GenerationType.Odds;
	PlayerTransformType editorPlayerTransformType = PlayerTransformType.Stardard;
	UseObjectPooling editorUseObjectPooling = UseObjectPooling.Yes;
	SpawnType editorSpawnType = SpawnType.Nodes;

	void OnEnable()
	{
		t = (Crux)target;
		GetTarget = new SerializedObject(t);
		Biome = GetTarget.FindProperty("Biome"); 
		TabNumberTop_SP = GetTarget.FindProperty ("TabNumberTop");
		CategoryTabNumber_SP = GetTarget.FindProperty ("CategoryTabNumber");
	}

	//Generates our Spawn IDs by picking three random characters and 3 random numbers
	//void GenerateAllSpawnIDs ()
	void SaveSpawnIDsToTxt ()
	{
		for(int l = 0; l < t.Biome.Count; l++)
		{
			for (int i = 0; i < t.Biome[l].WildlifeSpawnId.Count; i++)
			{
				t.SpawnIDs.Add("Name: " + t.Biome[l].WildlifeFoldOutNames[i] + "  -  Category: " + TabName[0] + "  -  Biome: " + t.Biome[l].CategoryName + "  -  Spawn ID: " + t.Biome[l].WildlifeSpawnId[i]);
			}
			
			for (int i = 0; i < t.Biome[l].CreaturesSpawnId.Count; i++)
			{
				t.SpawnIDs.Add("Name: " + t.Biome[l].CreaturesFoldOutNames[i] + "  -  Category: " + TabName[1] + "  -  Biome: " + t.Biome[l].CategoryName + "  -  Spawn ID: " + t.Biome[l].CreaturesSpawnId[i]);
			}

			for (int i = 0; i < t.Biome[l].NPCSpawnId.Count; i++)
			{
				t.SpawnIDs.Add("Name: " + t.Biome[l].NPCFoldOutNames[i] + "  -  Category: " + TabName[2] + "  -  Biome: " + t.Biome[l].CategoryName + "  -  Spawn ID: " + t.Biome[l].NPCSpawnId[i]);
			}

			if (l == t.Biome.Count - 1)
			{
				//If enabled, write all of our generated Spawn IDs to a txt file. This is convenient and helpful for testing.
				//This allows developers, or players, to spawn AI at their position according to the spawn ID.
				System.IO.File.WriteAllLines(t.SpawnIDsFilePath, t.SpawnIDs.ToArray());

				//Clear the SpawnID list
				t.SpawnIDs.Clear();

				Repaint();
				RegenerateSpawnIDs = false;
			}
		}
	}

	void RegenerateAllSpawnIDs ()
	{
		for(int l = 0; l < t.Biome.Count; l++)
		{
			for (int i = 0; i < t.Biome[l].WildlifeSpawnId.Count; i++)
			{
				string GeneratedID = "";
				string RandomLetter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

				//Generate 3 random letters
				for (int b = 0; b < 3; b++)
				{
					GeneratedID = RandomLetter[Random.Range(0,RandomLetter.Length)] + GeneratedID;
				}

				//Generate 3 random numbers between 0 and 9
				for (int j = 0; j < 3; j++)
				{
					GeneratedID = GeneratedID + Random.Range(0,10).ToString();
				}

				t.Biome[l].WildlifeSpawnId[i] = GeneratedID;
			}
				
			for (int i = 0; i < t.Biome[l].CreaturesSpawnId.Count; i++)
			{
				string GeneratedID = "";
				string RandomLetter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

				//Generate 3 random letters
				for (int b = 0; b < 3; b++)
				{
					GeneratedID = RandomLetter[Random.Range(0,RandomLetter.Length)] + GeneratedID;
				}

				//Generate 3 random numbers between 0 and 9
				for (int j = 0; j < 3; j++)
				{
					GeneratedID = GeneratedID + Random.Range(0,10).ToString();
				}

				t.Biome[l].CreaturesSpawnId[i] = GeneratedID;
			}

			for (int i = 0; i < t.Biome[l].NPCSpawnId.Count; i++)
			{
				string GeneratedID = "";
				string RandomLetter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
				
				//Generate 3 random letters
				for (int b = 0; b < 3; b++)
				{
					GeneratedID = RandomLetter[Random.Range(0,RandomLetter.Length)] + GeneratedID;
				}
				
				//Generate 3 random numbers between 0 and 9
				for (int j = 0; j < 3; j++)
				{
					GeneratedID = GeneratedID + Random.Range(0,10).ToString();
				}
				
				t.Biome[l].NPCSpawnId[i] = GeneratedID;
			}
		}
	}

	void GenerateNewSpawnID ()
	{
		string GeneratedID = "";
		string RandomLetter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		//Generate 3 random letters
		for (int b = 0; b < 3; b++)
		{
			GeneratedID = RandomLetter[Random.Range(0,RandomLetter.Length)] + GeneratedID;
		}

		//Generate 3 random numbers between 0 and 9
		for (int j = 0; j < 3; j++)
		{
			GeneratedID = GeneratedID + Random.Range(0,10).ToString();
		}

		NewSpawnID = GeneratedID;
	}

	public override void OnInspectorGUI()
	{
		GetTarget.Update();
		GameObject _gameObject = null;
		ListSize = Biome.arraySize;
	
		if(ListSize != Biome.arraySize)
		{
			while(ListSize > Biome.arraySize)
			{
				Biome.InsertArrayElementAtIndex(Biome.arraySize);
			}
			while(ListSize < Biome.arraySize)
			{
				Biome.DeleteArrayElementAtIndex(Biome.arraySize - 1);
			}
		}

		TabNumberTop_SP.intValue = GUILayout.SelectionGrid (TabNumberTop_SP.intValue, TabNameTop, 2);

		if (TabNumberTop_SP.intValue == 1)
		{
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Spawning Options", EditorStyles.boldLabel);
			EditorGUILayout.HelpBox("Below you can adjust various settings and conditions for spawn and maintaing your objects.", MessageType.None, true);

			EditorGUILayout.Space ();
			EditorGUILayout.Space ();

			EditorGUILayout.HelpBox("Controls whether or not Crux will use Object Pooling. This is helpful for increasing performance by reusing already spawned objects. When using this feature, ensure your AI system has support for resetting an AI after it has been set as inactive.", MessageType.None, true);

			editorUseObjectPooling = (UseObjectPooling)t.UseObjectPooling;
			editorUseObjectPooling = (UseObjectPooling)EditorGUILayout.EnumPopup("Use Object Pooling", editorUseObjectPooling);
			t.UseObjectPooling = (int)editorUseObjectPooling;

			EditorGUILayout.Space ();
			EditorGUILayout.Space ();

			EditorGUILayout.HelpBox("The Spawn Type controls the type of spawning technique Crux will use.", MessageType.None, true);

			editorSpawnType = (SpawnType)t.SpawnType;
			editorSpawnType = (SpawnType)EditorGUILayout.EnumPopup("Spawn Type", editorSpawnType);
			t.SpawnType = (int)editorSpawnType;

			if (t.SpawnType == 1){
				EditorGUILayout.HelpBox("The Nodes Spawn Type option will spawn all AI groups to nodes. If the player is out of the customizable range, the AI within the said node will be deactivated until the player is within range. This allows AI to stay inactive when not close enough to help increase performance.", MessageType.None, true);
			
				EditorGUILayout.Space ();
				EditorGUILayout.HelpBox("The distance that the AI will deactivate when using spawning nodes. Anything less than the Deactivate Distance will set the AI as inactive. When the AI have reached Crux's Despawn Radius, they will be despawned.", MessageType.None, true);
				t.SpawnNodeDeactivateDistance = EditorGUILayout.IntSlider ("Deactivate Distance", t.SpawnNodeDeactivateDistance, 10, (int)t._DespawnRadius-25);

				EditorGUILayout.Space ();
				EditorGUILayout.HelpBox("The Node Update Frequency controls how often the Spawn Nodes are updated to check for the player's distance to be activated.", MessageType.None, true);
				t.SpawnNodeUpdateFrequency = EditorGUILayout.Slider ("Node Update Frequency", t.SpawnNodeUpdateFrequency, 0.1f, 2.0f);
			}
			else if (t.SpawnType == 0){
				EditorGUILayout.HelpBox("The Standard Spawn Type option will simply spawn AI around the player. They will remain active as long as they don't leave Crux's despawn radius. This option is helpful for those who want AI to continue to be active even though they might not be visible or near the player.", MessageType.None, true);
			}
			
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();

			EditorGUILayout.HelpBox("The Player Transform Type is the transform that Crux will use as a reference point for spawning objects.", MessageType.None, true);

			editorPlayerTransformType = (PlayerTransformType)t.PlayerTransformType;
			editorPlayerTransformType = (PlayerTransformType)EditorGUILayout.EnumPopup("Player Transform Type", editorPlayerTransformType);
			t.PlayerTransformType = (int)editorPlayerTransformType;

			EditorGUILayout.Space ();

			if (t.PlayerTransformType == 1)
			{
				EditorGUILayout.HelpBox("The Standard Player Transform Type will allow you to manually set the player transform.", MessageType.None, true);
				t._PlayerObject = (Transform)EditorGUILayout.ObjectField ("Player Transform", t._PlayerObject, typeof(Transform), true);

				if (t._PlayerObject == null)
				{
					EditorGUILayout.HelpBox("You need to assign your Player's tranform to the Player Transform slot. Crux will be disabled during runtime if no transform is assigned.", MessageType.Error, true);
				}
			}

			if (t.PlayerTransformType == 2)
			{
				EditorGUILayout.HelpBox("The Instantiated Player Transform Type will have Crux automatically assign the player transform using your player's game object name. This option is intended for players that are created on Start. You can assign the player's name in the text box below.", MessageType.None, true);
				t.PlayerTransformName = EditorGUILayout.TextField ("Player Name", t.PlayerTransformName);
			}

			EditorGUILayout.Space ();
			EditorGUILayout.Space ();

			EditorGUILayout.HelpBox("The AI Detection is used to better spawn AI by searching for previously spawned AI and avoiding spawning new AI too close. This layer can be any layer that is desired other than the Default layer. If the Default layer is used, this feature will be disabled.", MessageType.None, true);
			t.useLayerDetection = EditorGUILayout.Toggle("Use AI Detection", t.useLayerDetection);

			if (t.useLayerDetection)
			{
				EditorGUILayout.Space ();
				EditorGUILayout.Space ();

				EditorGUILayout.HelpBox("The AI Detection Layer is the layer that the AI Detection will use to avoid other AI by getting their layer. Note: The AI Detection Layer cannot be Default.", MessageType.None, true);
				t.AILayerMask = EditorGUILayout.LayerField("AI Detection Layer", t.AILayerMask);

				if (LayerMask.LayerToName(t.AILayerMask.value) == "Default")
				{
					EditorGUILayout.HelpBox("The AI Detection Layer cannot be Default. This is because the layer detection needs to be limited to only the AI layer.", MessageType.Warning, true);
				}

				EditorGUILayout.Space();

				EditorGUILayout.HelpBox("The AI Layer Detection Distance is the distance that the AI Layer Detection uses. If a spawn attempt hits an AI that is below the AI Layer Detection Distance value, it will be skipped. This is to avoid AI from spawning too close to each other. Note: The higher the value, the less chance there is of a successful spawn. However, this will result in more evenly distributed spawning.", MessageType.None, true);
				t.AILayerMaskDectectionDistance = EditorGUILayout.IntSlider ("AI Layer Detection Distance", t.AILayerMaskDectectionDistance, 25, 300);
			}

			EditorGUILayout.Space ();
			EditorGUILayout.Space ();

			EditorGUILayout.HelpBox("The Update Frequency is how often the system will attempt to spawn an object.", MessageType.None, true);
			t._UpdateTickFrequency = EditorGUILayout.Slider ("Update Frequency", t._UpdateTickFrequency, 0.1f, 30.0f);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("Use Visual Radii will renderer the Min, Max, and despawn radii in the Scene View.", MessageType.None, true);
			t.useVisualRadii = EditorGUILayout.Toggle("Use Visual Radii", t.useVisualRadii);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("The Min Radius is the closest allowed spawning position with the player's radius.", MessageType.None, true);
			t._MinRadius = EditorGUILayout.IntSlider ("Min Radius", t._MinRadius, 10, 600);

			EditorGUILayout.HelpBox("The Max Radius is the closest allowed spawning position with the player's radius.", MessageType.None, true);
			t._MaxRadius = EditorGUILayout.IntSlider ("Max Radius", t._MaxRadius, 100, 1200);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("The Despawn Radius determines when the system will despawn an AI.", MessageType.None, true);
			t._DespawnRadius = EditorGUILayout.Slider ("Despawn Radius", t._DespawnRadius, 150.0f, 2500.0f);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("The Max Objects determines the max amount of objects the system can spawn at one time. If the max value is reached, the system will not spawn another object until an object has been despanwed dropping the current amount of objects spawned.", MessageType.None, true);
			t._MaxObjectsToSpawn = EditorGUILayout.IntSlider ("Max Objects", t._MaxObjectsToSpawn, 0, 50);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("The Starting Amount determines how many object will be spawned on start around your player.", MessageType.None, true);
			t.StartingAIAmount = EditorGUILayout.IntSlider ("Starting Amount", t.StartingAIAmount, 0, 50);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("The Y Spawn Offset determines the offset amount an AI will spawn above the terrain.", MessageType.None, true);
			t._SpawnOffset = EditorGUILayout.Slider ("Y Spawn Offset", t._SpawnOffset, -5.0f, 5.0f);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("The Min and Max Steepness determine the minimum and maximum terrain steepness your objecys can spawn at. Anything above or below the two values will be skipped as a spawning point.", MessageType.None, true);
			EditorGUILayout.BeginHorizontal ();
			//GUILayout.Space(75);
			EditorGUILayout.LabelField("Min Steepness",GUILayout.Width(100));
			t.minSteepness = EditorGUILayout.IntField(t.minSteepness);
			EditorGUILayout.LabelField("Max Steepness",GUILayout.Width(100));
			t.maxSteepness = EditorGUILayout.IntField(t.maxSteepness);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Spawn ID Options", EditorStyles.boldLabel);
			EditorGUILayout.HelpBox("The Spawn ID system generates Spawn IDs for each object created with Crux. These Spawn IDs can then be used for spawning objects at the player's location. This is useful for testing and development. These spawning IDs can also be written to a txt file or regenerated using the buttons below.", MessageType.None, true);

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("Enable Spawn IDs controls whether or not users will have the ability to spawn AI around the player using the object's spawn IDs.", MessageType.None, true);
			t.SpawnIDsEnabled = EditorGUILayout.Toggle("Enable Spawn IDs", t.SpawnIDsEnabled);


			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("Save all IDs to txt will save all objects' IDs to a txt file along with their Name, Category, and Biome.", MessageType.None, true);

			if (GUILayout.Button("Save all IDs to txt")){
				if (t.SpawnIDsFileName.Contains(".txt")){
					t.SpawnIDsFileName = t.SpawnIDsFileName.Replace(".txt", "");
				}

				t.SpawnIDsFilePath = EditorUtility.SaveFilePanelInProject("Save as txt", "Crux Spawn IDs", "txt", "Please enter a name to save the file to");

				if (t.SpawnIDsFilePath != string.Empty){
					SaveSpawnIDsToTxt();
					AssetDatabase.Refresh();
				}
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("Regenerate Spawn IDs will regenerate all Spawn IDs for all objects in all Biomes and Categories", MessageType.None, true);

			//Randomly generate spawn IDs for each category and biome
			if (GUILayout.Button("Regenerate Spawn IDs"))
			{
				DisplayMessage = true;
			}

			if (DisplayMessage && EditorUtility.DisplayDialog("Regenerate Spawn IDs?","Are you sure you want to regenerate all Spawn IDs? This proccess will regenerate all Spawn IDs for all objects in all Biomes and Categories. This process cannot be undone.", "Regenerate", "Do Not Regenerate"))
			{
				RegenerateSpawnIDs = true;
				DisplayMessage = false;
			}
			else
			{
				RegenerateSpawnIDs = false;
				DisplayMessage = false;
			}
				
			if (t.writeSpawnIDsToTxtFile)
			{
				if (RegenerateSpawnIDs)
				{
					RegenerateAllSpawnIDs();
				}
			}

			GUILayout.Space(15);
		}


		EditorGUILayout.Space ();
		EditorGUILayout.Space ();



		if (TabNumberTop_SP.intValue == 0)
		{
			EditorGUILayout.LabelField("Create Biome", EditorStyles.boldLabel);
			EditorGUILayout.HelpBox("Below you can create a Biome for each environment type. These Biomes allow you to choose exactly what Wildlife, Creatures, and NPCs will spawn according to the textures for the environment.", MessageType.None, true);

			EditorGUILayout.Space ();

			EditorGUILayout.HelpBox("Add New Biome will create a new Biome. You will need to assign the textures from your terrain that defines the newly created Biome.", MessageType.None, true);
			if(GUILayout.Button("Add New Biome"))
			{
				t.Biome.Add(new Crux.CruxClass());
			}

			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();

			EditorGUILayout.LabelField("Biome List", EditorStyles.boldLabel);

			CategoryNames = new string[t.Biome.Count];

			for(int l = 0; l < t.Biome.Count; l++)
			{
				CategoryNames[l] = t.Biome[l].CategoryName;
			}


			CategoryTabNumber_SP.intValue = GUILayout.SelectionGrid (CategoryTabNumber_SP.intValue, CategoryNames, 2);

			for(int i = 0; i < Biome.arraySize; i++)
			{
				SerializedProperty MyListRef = Biome.GetArrayElementAtIndex(i);
				Crux.CruxClass _MyClass = t.Biome[i];
				SerializedProperty TabNumber_SP = MyListRef.FindPropertyRelative("TabNumber");

				if (CategoryTabNumber_SP.intValue == i)
				{
					EditorGUILayout.Space ();
					EditorGUILayout.Space ();
					_MyClass.CategoryName = EditorGUILayout.TextField("Biome Name ", _MyClass.CategoryName);
					EditorGUILayout.HelpBox("The Biome Name defines the name of this Biome.", MessageType.None);

					EditorGUILayout.Space ();
					EditorGUILayout.Space ();

					//Terrain Textures
					if(_MyClass.TerrainTextures != null)
					{
						if(_MyClass.TerrainTextures.Count > 0)
						{
							int totalTextures = 0;

							EditorGUILayout.LabelField("Texture Options", EditorStyles.boldLabel);
							EditorGUILayout.HelpBox("The Texture Options allow you to pick which textures define a Biome. When the system detects these textures, it will spawn an object accordingly.", MessageType.None, true);

							EditorGUILayout.Space ();
							EditorGUILayout.Space ();

							for (int j = 0; j < _MyClass.TerrainTextures.Count; ++j)
							{
								if(totalTextures == 0)
								{
									GUILayout.BeginHorizontal(GUILayout.MinHeight(100));
									GUILayout.Space(50);
								}

								GUILayout.BeginVertical(GUILayout.MinHeight(100));
								_MyClass.TerrainTextures[j] = (Texture)EditorGUILayout.ObjectField(_MyClass.TerrainTextures[j], typeof(Texture), false, GUILayout.MinWidth(50), GUILayout.MaxWidth(75), GUILayout.MinHeight(50), GUILayout.MaxHeight(75));

								if (GUILayout.Button("Remove", GUILayout.MinWidth(50), GUILayout.MaxWidth(75)))
								{
									_MyClass.TerrainTextures.RemoveAt(j);
									--j;
								}
										
								GUILayout.EndVertical();
								totalTextures++;

								if(totalTextures == 4 || j == _MyClass.TerrainTextures.Count - 1)
								{
									GUILayout.Space(30);

									GUILayout.EndHorizontal();
									totalTextures = 0;
								}
							}
						}


						if(_MyClass.TerrainTextures.Count == 0)
						{
							GUILayout.BeginHorizontal();
							GUILayout.Space(50);
							EditorGUILayout.HelpBox("There are currently no textures for this Biome.", MessageType.Info);
							GUILayout.Space(20);
							GUILayout.EndHorizontal();
						}

						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						if (GUILayout.Button("Add Texture", GUILayout.MinWidth(90), GUILayout.MaxWidth(90)))
						{
							Texture2D newTexture = new Texture2D(128, 128);
							_MyClass.TerrainTextures.Add(newTexture);
						}
						GUILayout.EndHorizontal();
					}


					EditorGUILayout.Space ();
					EditorGUILayout.Space ();
					EditorGUILayout.Space ();

					EditorGUILayout.LabelField("Object Options", EditorStyles.boldLabel);
					EditorGUILayout.HelpBox("The Object Options allow you to pick which objects spawn for this Biome. If the system detects any of the above textures, it will spawn an object according to the conditions below.", MessageType.None, true);

					EditorGUILayout.Space ();
					EditorGUILayout.Space ();

					TabNumber_SP.intValue = GUILayout.SelectionGrid (TabNumber_SP.intValue, TabName, 3);

					EditorGUILayout.Space ();

					//Wildlife Objects
					if (TabNumber_SP.intValue == 0)
					{
						EditorGUILayout.Space ();
						EditorGUILayout.LabelField("Wildlife Options", EditorStyles.boldLabel);
						EditorGUILayout.HelpBox("Wildlife objects are typically animals and can be passive or aggressive. Below you can adjust the settings for any object within this category as well as adjusting settings for each individual object.", MessageType.None, true);
						EditorGUILayout.Space ();

						_MyClass.WildlifeOdds = EditorGUILayout.IntSlider ("Wildlife Odds", _MyClass.WildlifeOdds, 0, 100);
						EditorGUILayout.HelpBox("Wildlife Odds controls the odds of the Wildlife Category objects spawning in the " + _MyClass.CategoryName + " Biome.", MessageType.None, true);
						GUILayout.Space(10);

						GenerationTypeEnum = (GenerationType)_MyClass.WildlifeGenerationType;
						GenerationTypeEnum = (GenerationType)EditorGUILayout.EnumPopup("Generation Type", GenerationTypeEnum);
						_MyClass.WildlifeGenerationType = (int)GenerationTypeEnum;

						EditorGUILayout.HelpBox("The Generation Type determines if your object is generated by odds or by random. The Random setting will generate any object within the list below by random. The Odds setting will allow users to choose the odds of each object spawning.", MessageType.None);

						GUILayout.Space(15);

						EditorGUILayout.LabelField("Objects", EditorStyles.boldLabel);
						EditorGUILayout.HelpBox("By pressing the Add Object button, you can create an endless amount of potential objects to spawn for this biome each with their own options and conditions.", MessageType.None, true);

						GUILayout.Space(10);

						GUILayout.BeginHorizontal();
						GUILayout.Space(75);

						if (GUILayout.Button("Add Object"))
						{
							GenerateNewSpawnID();
							_MyClass.WildlifeToSpawn.Add(_gameObject);
							_MyClass.WildlifeMinGroupAmount.Add(1);
							_MyClass.WildlifeMaxGroupAmount.Add(3);
							_MyClass.WildlifeFoldOuts.Add(new bool());
							_MyClass.WildlifeFoldOutNames.Add("Object Name");
							_MyClass.WildlifeRarityEnumNumber.Add(new int());
							_MyClass.WildlifeHeightTypeEnumNumber.Add(1);
							_MyClass.WildlifeSpawnRadius.Add(5);

							//_MyClass.WildlifeSpawnUniStormTime.Add(2);

							_MyClass.WildlifeSpawnId.Add(NewSpawnID); 
							_MyClass.WildlifeGroupEnumNumber.Add(1); 
							_MyClass.WildlifeCurrentPopulation.Add(0); 
							_MyClass.WildlifeMaxPopulation.Add(3); 
							_MyClass.WildlifeSpawnHeightMin.Add(0);
							_MyClass.WildlifeSpawnHeightMax.Add(0);

							EditorUtility.SetDirty(t);

						}
						GUILayout.Space(75);
						GUILayout.EndHorizontal();
						GUILayout.Space(10);

						if(_MyClass.WildlifeToSpawn != null)
						{
							if(_MyClass.WildlifeToSpawn.Count > 0)
							{
								for (int j = 0; j < _MyClass.WildlifeToSpawn.Count; ++j)
								{

									_MyClass.WildlifeFoldOuts[j] = EditorGUILayout.Foldout(_MyClass.WildlifeFoldOuts[j], _MyClass.WildlifeFoldOutNames[j].ToString());

									if (_MyClass.WildlifeFoldOuts[j])
									{
										GUILayout.Space(10);


										_MyClass.WildlifeFoldOutNames[j] = EditorGUILayout.TextField("Object Name ", _MyClass.WildlifeFoldOutNames[j]);
										EditorGUILayout.HelpBox("The Object Name is used to name this object category as well as when the object spawns.", MessageType.None);

										GUILayout.Space(10);

										EditorGUILayout.LabelField("Spawn ID", _MyClass.WildlifeSpawnId[j]);
										EditorGUILayout.HelpBox("The Spawn ID is used to manually spawn this object using the in-game spawning system. This is helpful for development and testing purposes.", MessageType.None);

										GUILayout.Space(20);

										GUILayout.BeginHorizontal();
										_MyClass.WildlifeToSpawn[j] = (GameObject)EditorGUILayout.ObjectField("Object to Spawn", _MyClass.WildlifeToSpawn[j], typeof(GameObject), false);
										GUILayout.EndHorizontal();

										GUILayout.Space(20);

										_MyClass.WildlifeMaxPopulation[j] = EditorGUILayout.IntSlider ("Population Cap", _MyClass.WildlifeMaxPopulation[j], 0, 25);
										EditorGUILayout.HelpBox("The Population Cap will help keep too many of the same objects from spawning. Once the Population Cap has been reached, no more of this object will spawn until it's been despawned or destroyed.", MessageType.None, true);

										GUILayout.Space(20);

										if (_MyClass.WildlifeGroupEnumNumber != null && j > -1)
										{
											if (_MyClass.WildlifeGroupEnumNumber.Count > 0)
											{
												Crux.CruxClass.GroupEnum GroupEnum = Crux.CruxClass.GroupEnum.No;

												GroupEnum = (Crux.CruxClass.GroupEnum)_MyClass.WildlifeGroupEnumNumber[j];
												GroupEnum = (Crux.CruxClass.GroupEnum)EditorGUILayout.EnumPopup("Enable Group Spawning", GroupEnum);
												_MyClass.WildlifeGroupEnumNumber[j] = (int)GroupEnum;

												EditorGUILayout.HelpBox("Enable Group Spawning enables the ability to spawn a randomized number of this object based on the minimum and maximum groups number of this object.", MessageType.None, true);
												GUILayout.Space(10);
											}
										}

										GUILayout.Space(10);

										if (_MyClass.WildlifeMinGroupAmount != null && j > -1 && _MyClass.WildlifeGroupEnumNumber[j] == 0)
										{
											if (_MyClass.WildlifeMinGroupAmount.Count > 0 && _MyClass.WildlifeMaxGroupAmount.Count > 0)
											{
												EditorGUILayout.BeginHorizontal ();
												GUILayout.Space(30);
												EditorGUILayout.LabelField("Min Group",GUILayout.Width(75));
												_MyClass.WildlifeMinGroupAmount[j] = EditorGUILayout.IntField(_MyClass.WildlifeMinGroupAmount[j],GUILayout.Width(45));
												EditorGUILayout.LabelField("Max Group",GUILayout.Width(75));
												_MyClass.WildlifeMaxGroupAmount[j] = EditorGUILayout.IntField(_MyClass.WildlifeMaxGroupAmount[j],GUILayout.Width(45));
												EditorGUILayout.EndHorizontal ();
											}

											EditorGUILayout.HelpBox("The Min and Max Group determines the number of times this object can be spawned in one area during a successful spawn.", MessageType.None, true);

											GUILayout.Space(20);
										}

										if (_MyClass.WildlifeRarityEnumNumber != null && j > -1)
										{
											if (_MyClass.WildlifeRarityEnumNumber.Count > 0)
											{
												Crux.CruxClass.RarityEnum RarityEnum = Crux.CruxClass.RarityEnum.Common;

												if (_MyClass.WildlifeGenerationType == 1)
												{
													GUILayout.Space(10);
													RarityEnum = (Crux.CruxClass.RarityEnum)_MyClass.WildlifeRarityEnumNumber[j];
													RarityEnum = (Crux.CruxClass.RarityEnum)EditorGUILayout.EnumPopup("Chance to Spawn", RarityEnum);
													_MyClass.WildlifeRarityEnumNumber[j] = (int)RarityEnum;

													EditorGUILayout.HelpBox("The Chance to Spawn determines how often this object will spawn when the proper conditions are met.", MessageType.None, true);
													GUILayout.Space(10);
												}
											}
										}

										GUILayout.Space(10);

										//Users who are updating to version 1.1 will need Crux to update missing elements for newly added lists to avoid errors.
										//This does code does this automatically.
										if (_MyClass.WildlifeHeightTypeEnumNumber.Count == 0 && _MyClass.WildlifeSpawnId.Count > 0)
										{
											for (int c = 0; c < _MyClass.WildlifeSpawnId.Count; c++){
												_MyClass.WildlifeHeightTypeEnumNumber.Add(1);
												_MyClass.WildlifeSpawnHeightMin.Add(0);
												_MyClass.WildlifeSpawnHeightMax.Add(0);
											}
										}
										//

										if (_MyClass.WildlifeHeightTypeEnumNumber != null && j > -1)
										{
											if (_MyClass.WildlifeHeightTypeEnumNumber.Count > 0)
											{
												Crux.CruxClass.HeightTypeEnum HeightTypeEnum = Crux.CruxClass.HeightTypeEnum.Land;

												if (_MyClass.WildlifeGenerationType == 1)
												{
													GUILayout.Space(10);
													HeightTypeEnum = (Crux.CruxClass.HeightTypeEnum)_MyClass.WildlifeHeightTypeEnumNumber[j];
													HeightTypeEnum = (Crux.CruxClass.HeightTypeEnum)EditorGUILayout.EnumPopup("AI Type", HeightTypeEnum);
													_MyClass.WildlifeHeightTypeEnumNumber[j] = (int)HeightTypeEnum;
													
													EditorGUILayout.HelpBox("The AI Type determines the height type that will be used for spawning this AI. The Air and Water option will allow you to customizable the spawning heights that will then be randomized when this AI is spawned.", MessageType.None, true);
													GUILayout.Space(10);
												}
											}
										}
										
										GUILayout.Space(10);

										if (_MyClass.WildlifeHeightTypeEnumNumber[j] == 2 || _MyClass.WildlifeHeightTypeEnumNumber[j] == 3){
											if (_MyClass.WildlifeSpawnHeightMin.Count > 0)
											{
												EditorGUILayout.BeginHorizontal ();
												GUILayout.Space(8);
												EditorGUILayout.LabelField("Min Spawn Height",GUILayout.Width(105));
												_MyClass.WildlifeSpawnHeightMin[j] = EditorGUILayout.IntField(_MyClass.WildlifeSpawnHeightMin[j],GUILayout.Width(45));
												EditorGUILayout.LabelField("Max Spawn Height",GUILayout.Width(109));
												_MyClass.WildlifeSpawnHeightMax[j] = EditorGUILayout.IntField(_MyClass.WildlifeSpawnHeightMax[j],GUILayout.Width(45));
												EditorGUILayout.EndHorizontal ();
											}
											
											EditorGUILayout.HelpBox("The Min and Max Spawn Height determines the randomized height that your AI will spawn. This is useful for flying and water AI types.", MessageType.None, true);

											GUILayout.Space(20);
										}

										/*
										#if USING_UNISTORM
										GUILayout.Space(10);

										if (_MyClass.WildlifeSpawnUniStormTime != null && j > -1)
										{
											if (_MyClass.WildlifeSpawnUniStormTime.Count > 0)
											{
												Crux.CruxClass.UniStormSpawnTimeEnum UniStormSpawnTimeEnum = Crux.CruxClass.UniStormSpawnTimeEnum.Day;
												GUILayout.Space(10);
												UniStormSpawnTimeEnum = (Crux.CruxClass.UniStormSpawnTimeEnum)_MyClass.WildlifeSpawnUniStormTime[j];
												UniStormSpawnTimeEnum = (Crux.CruxClass.UniStormSpawnTimeEnum)EditorGUILayout.EnumPopup("UniStorm Spawn Time", UniStormSpawnTimeEnum);
												_MyClass.WildlifeSpawnUniStormTime[j] = (int)UniStormSpawnTimeEnum;
													
												EditorGUILayout.HelpBox("The Chance to Spawn determines how often this object will spawn when the proper conditions are met.", MessageType.None, true);
												GUILayout.Space(10);
											}
										}
										#endif
										*/

										_MyClass.WildlifeSpawnRadius[j] = EditorGUILayout.IntSlider ("Spawn Radius", _MyClass.WildlifeSpawnRadius[j], 1, 50);
										EditorGUILayout.HelpBox("The Spawn Radius controls how close the groups for this object will spawn.", MessageType.None, true);

										GUILayout.Space(10);

										GUILayout.BeginHorizontal();
										GUILayout.Space(75);
										if (GUILayout.Button("Remove this Object"))
										{
											_MyClass.WildlifeToSpawn.RemoveAt(j);
											_MyClass.WildlifeMinGroupAmount.RemoveAt(j);
											_MyClass.WildlifeMaxGroupAmount.RemoveAt(j);
											_MyClass.WildlifeFoldOuts.RemoveAt(j);
											_MyClass.WildlifeFoldOutNames.RemoveAt(j);
											_MyClass.WildlifeRarityEnumNumber.RemoveAt(j);
											_MyClass.WildlifeHeightTypeEnumNumber.RemoveAt(j);
											_MyClass.WildlifeSpawnRadius.RemoveAt(j);

											//_MyClass.WildlifeSpawnUniStormTime.RemoveAt(j);

											_MyClass.WildlifeSpawnId.RemoveAt(j); 
											_MyClass.WildlifeGroupEnumNumber.RemoveAt(j); 
											_MyClass.WildlifeCurrentPopulation.RemoveAt(j); 
											_MyClass.WildlifeMaxPopulation.RemoveAt(j); 
											_MyClass.WildlifeSpawnHeightMin.RemoveAt(j);
											_MyClass.WildlifeSpawnHeightMax.RemoveAt(j);

											EditorUtility.SetDirty(t);

											--j;
										}
										GUILayout.Space(75);
										GUILayout.EndHorizontal();

										GUILayout.Space(20);
									}

									GUILayout.Space(10);
								}
							}


							if(_MyClass.WildlifeToSpawn.Count == 0)
							{
								GUILayout.BeginHorizontal();
								GUILayout.Space(50);
								EditorGUILayout.HelpBox("There are no Wildlife objects for this Biome. To create one, press the Add Object button.", MessageType.Info);
								GUILayout.Space(20);
								GUILayout.EndHorizontal();
								GUILayout.Space(10);
							}


						}
					}


					if (TabNumber_SP.intValue == 1)
					{
						EditorGUILayout.Space ();
						EditorGUILayout.LabelField("Creature Options", EditorStyles.boldLabel);
						EditorGUILayout.HelpBox("Creature objects are typically non-animal and are usually aggressive. Below you can adjust the settings for any object within this category as well as adjusting settings for each individual object.", MessageType.None, true);
						EditorGUILayout.Space ();

						_MyClass.CreaturesOdds = EditorGUILayout.IntSlider ("Creature Odds", _MyClass.CreaturesOdds, 0, 100);
						EditorGUILayout.HelpBox("Creature Odds controls the odds of the Creature Category objects spawning in the " + _MyClass.CategoryName + " Biome.", MessageType.None, true);
						GUILayout.Space(10);

						GenerationTypeEnum = (GenerationType)_MyClass.CreaturesGenerationType;
						GenerationTypeEnum = (GenerationType)EditorGUILayout.EnumPopup("Generation Type", GenerationTypeEnum);
						_MyClass.CreaturesGenerationType = (int)GenerationTypeEnum;

						EditorGUILayout.HelpBox("The Generation Type determines if your object is generated by odds or by random. The Random setting will generate any object within the list below by random. The Odds setting will allow users to choose the odds of each object spawning.", MessageType.None);

						GUILayout.Space(15);

						EditorGUILayout.LabelField("Objects", EditorStyles.boldLabel);
						EditorGUILayout.HelpBox("By pressing the Add Object button, you can create an endless amount of potential objects to spawn for this biome each with their own options and conditions.", MessageType.None, true);

						GUILayout.Space(10);

						GUILayout.BeginHorizontal();
						GUILayout.Space(75);

						if (GUILayout.Button("Add Object"))
						{
							GenerateNewSpawnID();
							_MyClass.CreaturesToSpawn.Add(_gameObject);
							_MyClass.CreaturesMinGroupAmount.Add(1);
							_MyClass.CreaturesMaxGroupAmount.Add(3);
							_MyClass.CreaturesFoldOuts.Add(new bool());
							_MyClass.CreaturesFoldOutNames.Add("Object Name");
							_MyClass.CreaturesRarityEnumNumber.Add(new int());
							_MyClass.CreaturesSpawnRadius.Add(5);
							_MyClass.CreaturesHeightTypeEnumNumber.Add(1);

							_MyClass.CreaturesSpawnId.Add(NewSpawnID);
							_MyClass.CreaturesGroupEnumNumber.Add(1);
							_MyClass.CreaturesCurrentPopulation.Add(0);
							_MyClass.CreaturesMaxPopulation.Add(3);
							_MyClass.CreaturesSpawnHeightMin.Add(0);
							_MyClass.CreaturesSpawnHeightMax.Add(0);

							EditorUtility.SetDirty(t);

						}
						GUILayout.Space(75);
						GUILayout.EndHorizontal();
						GUILayout.Space(10);

						if(_MyClass.CreaturesToSpawn != null)
						{
							if(_MyClass.CreaturesToSpawn.Count > 0)
							{
								for (int j = 0; j < _MyClass.CreaturesToSpawn.Count; ++j)
								{

									_MyClass.CreaturesFoldOuts[j] = EditorGUILayout.Foldout(_MyClass.CreaturesFoldOuts[j], _MyClass.CreaturesFoldOutNames[j].ToString());

									if (_MyClass.CreaturesFoldOuts[j])
									{
										GUILayout.Space(10);


										_MyClass.CreaturesFoldOutNames[j] = EditorGUILayout.TextField("Object Name ", _MyClass.CreaturesFoldOutNames[j]);
										EditorGUILayout.HelpBox("The Object Name is used to name this object category as well as when the object spawns.", MessageType.None);

										GUILayout.Space(10);

										EditorGUILayout.LabelField("Spawn Id", _MyClass.CreaturesSpawnId[j]);
										EditorGUILayout.HelpBox("The Spawn Id is used to manually spawn this object using the in-game spawning system. This is helpful for development and testing purposes.", MessageType.None);

										GUILayout.Space(20);

										GUILayout.BeginHorizontal();
										_MyClass.CreaturesToSpawn[j] = (GameObject)EditorGUILayout.ObjectField("Object to Spawn", _MyClass.CreaturesToSpawn[j], typeof(GameObject), false);
										GUILayout.EndHorizontal();

										GUILayout.Space(20);

										_MyClass.CreaturesMaxPopulation[j] = EditorGUILayout.IntSlider ("Population Cap", _MyClass.CreaturesMaxPopulation[j], 0, 25);
										EditorGUILayout.HelpBox("The Population Cap will help keep too many of the same objects from spawning. Once the Population Cap has been reached, no more of this object will spawn until it's been despawned or destroyed.", MessageType.None, true);

										GUILayout.Space(20);

										if (_MyClass.CreaturesGroupEnumNumber != null && j > -1)
										{
											if (_MyClass.CreaturesGroupEnumNumber.Count > 0)
											{
												Crux.CruxClass.GroupEnum GroupEnum = Crux.CruxClass.GroupEnum.No;

												GroupEnum = (Crux.CruxClass.GroupEnum)_MyClass.CreaturesGroupEnumNumber[j];
												GroupEnum = (Crux.CruxClass.GroupEnum)EditorGUILayout.EnumPopup("Enable Group Spawning", GroupEnum);
												_MyClass.CreaturesGroupEnumNumber[j] = (int)GroupEnum;

												EditorGUILayout.HelpBox("Enable Group Spawning enables the ability to spawn a randomized number of this object based on the minimum and maximum groups number of this object.", MessageType.None, true);
												GUILayout.Space(10);
											}
										}

										GUILayout.Space(10);

										if (_MyClass.CreaturesMinGroupAmount != null && j > -1 && _MyClass.CreaturesGroupEnumNumber[j] == 0)
										{
											if (_MyClass.CreaturesMinGroupAmount.Count > 0 && _MyClass.CreaturesMaxGroupAmount.Count > 0)
											{
												EditorGUILayout.BeginHorizontal ();
												GUILayout.Space(30);
												EditorGUILayout.LabelField("Min Group",GUILayout.Width(75));
												_MyClass.CreaturesMinGroupAmount[j] = EditorGUILayout.IntField(_MyClass.CreaturesMinGroupAmount[j],GUILayout.Width(75));
												EditorGUILayout.LabelField("Max Group",GUILayout.Width(75));
												_MyClass.CreaturesMaxGroupAmount[j] = EditorGUILayout.IntField(_MyClass.CreaturesMaxGroupAmount[j],GUILayout.Width(75));
												EditorGUILayout.EndHorizontal ();
											}

											EditorGUILayout.HelpBox("The Min and Max Group determines the number of times this object can be spawned in one area during a successful spawn.", MessageType.None, true);

											GUILayout.Space(20);
										}


										if (_MyClass.CreaturesRarityEnumNumber != null && j > -1)
										{
											if (_MyClass.CreaturesRarityEnumNumber.Count > 0)
											{
												Crux.CruxClass.RarityEnum RarityEnum = Crux.CruxClass.RarityEnum.Common;

												if (_MyClass.CreaturesGenerationType == 1)
												{
													GUILayout.Space(10);
													RarityEnum = (Crux.CruxClass.RarityEnum)_MyClass.CreaturesRarityEnumNumber[j];
													RarityEnum = (Crux.CruxClass.RarityEnum)EditorGUILayout.EnumPopup("Chance to Spawn", RarityEnum);
													_MyClass.CreaturesRarityEnumNumber[j] = (int)RarityEnum;

													EditorGUILayout.HelpBox("The Chance to Spawn determines how often this object will spawn when the proper conditions are met.", MessageType.None, true);
													GUILayout.Space(10);
												}
											}
										}

										GUILayout.Space(10);

										//Users who are updating to version 1.1 will need Crux to update missing elements for newly added lists to avoid errors.
										//This does code does this automatically.
										if (_MyClass.CreaturesHeightTypeEnumNumber.Count == 0 && _MyClass.CreaturesSpawnId.Count > 0)
										{
											for (int c = 0; c < _MyClass.CreaturesSpawnId.Count; c++){
												_MyClass.CreaturesHeightTypeEnumNumber.Add(1);
												_MyClass.CreaturesSpawnHeightMin.Add(0);
												_MyClass.CreaturesSpawnHeightMax.Add(0);
											}
										}
										//
										
										if (_MyClass.CreaturesHeightTypeEnumNumber != null && j > -1)
										{
											if (_MyClass.CreaturesHeightTypeEnumNumber.Count > 0)
											{
												Crux.CruxClass.HeightTypeEnum HeightTypeEnum = Crux.CruxClass.HeightTypeEnum.Land;

												if (_MyClass.CreaturesGenerationType == 1)
												{
													GUILayout.Space(10);
													HeightTypeEnum = (Crux.CruxClass.HeightTypeEnum)_MyClass.CreaturesHeightTypeEnumNumber[j];
													HeightTypeEnum = (Crux.CruxClass.HeightTypeEnum)EditorGUILayout.EnumPopup("AI Type", HeightTypeEnum);
													_MyClass.CreaturesHeightTypeEnumNumber[j] = (int)HeightTypeEnum;
													
													EditorGUILayout.HelpBox("The AI Type determines the height type that will be used for spawning this AI. The Air and Water option will allow you to customizable the spawning heights that will then be randomized when this AI is spawned.", MessageType.None, true);
													GUILayout.Space(10);
												}
											}
										}
										
										GUILayout.Space(10);

										if (_MyClass.CreaturesHeightTypeEnumNumber[j] == 2 || _MyClass.CreaturesHeightTypeEnumNumber[j] == 3){
											if (_MyClass.CreaturesSpawnHeightMin.Count > 0)
											{
												EditorGUILayout.BeginHorizontal ();
												GUILayout.Space(8);
												EditorGUILayout.LabelField("Min Spawn Height",GUILayout.Width(105));
												_MyClass.CreaturesSpawnHeightMin[j] = EditorGUILayout.IntField(_MyClass.CreaturesSpawnHeightMin[j],GUILayout.Width(45));
												EditorGUILayout.LabelField("Max Spawn Height",GUILayout.Width(109));
												_MyClass.CreaturesSpawnHeightMax[j] = EditorGUILayout.IntField(_MyClass.CreaturesSpawnHeightMax[j],GUILayout.Width(45));
												EditorGUILayout.EndHorizontal ();
											}
											
											EditorGUILayout.HelpBox("The Min and Max Spawn Height determines the randomized height that your AI will spawn. This is useful for flying and water AI types.", MessageType.None, true);
											
											GUILayout.Space(20);
										}

										_MyClass.CreaturesSpawnRadius[j] = EditorGUILayout.IntSlider ("Spawn Radius", _MyClass.CreaturesSpawnRadius[j], 1, 25);
										EditorGUILayout.HelpBox("The Spawn Radius controls how close the groups for this object will spawn.", MessageType.None, true);

										GUILayout.Space(10);

										GUILayout.BeginHorizontal();
										GUILayout.Space(75);
										if (GUILayout.Button("Remove this Object"))
										{
											_MyClass.CreaturesToSpawn.RemoveAt(j);
											_MyClass.CreaturesMinGroupAmount.RemoveAt(j);
											_MyClass.CreaturesMaxGroupAmount.RemoveAt(j);
											_MyClass.CreaturesFoldOuts.RemoveAt(j);
											_MyClass.CreaturesFoldOutNames.RemoveAt(j);
											_MyClass.CreaturesRarityEnumNumber.RemoveAt(j);
											_MyClass.CreaturesSpawnRadius.RemoveAt(j);
											_MyClass.CreaturesHeightTypeEnumNumber.RemoveAt(j);

											_MyClass.CreaturesSpawnId.RemoveAt(j);
											_MyClass.CreaturesGroupEnumNumber.RemoveAt(j); 
											_MyClass.CreaturesCurrentPopulation.RemoveAt(j); 
											_MyClass.CreaturesMaxPopulation.RemoveAt(j);
											_MyClass.CreaturesSpawnHeightMin.RemoveAt(j);
											_MyClass.CreaturesSpawnHeightMax.RemoveAt(j);

											EditorUtility.SetDirty(t);

											--j;
										}
										GUILayout.Space(75);
										GUILayout.EndHorizontal();

										GUILayout.Space(20);
									}

									GUILayout.Space(10);
								}
							}


							if(_MyClass.CreaturesToSpawn.Count == 0)
							{
								GUILayout.BeginHorizontal();
								GUILayout.Space(50);
								EditorGUILayout.HelpBox("There are no Creature objects for this Biome. To create one, press the Add Object button.", MessageType.Info);
								GUILayout.Space(20);
								GUILayout.EndHorizontal();
								GUILayout.Space(10);
							}
								
						}
					}


					//NPC
					if (TabNumber_SP.intValue == 2)
					{
						EditorGUILayout.Space ();
						EditorGUILayout.LabelField("NPC Options", EditorStyles.boldLabel);
						EditorGUILayout.HelpBox("NPC objects are typically hunmaniod and can be passive or aggressive. Below you can adjust the settings for any object within this category as well as adjusting settings for each individual object.", MessageType.None, true);
						EditorGUILayout.Space ();

						_MyClass.NPCOdds = EditorGUILayout.IntSlider ("NPC Odds", _MyClass.NPCOdds, 0, 100);
						EditorGUILayout.HelpBox("NPC Odds controls the odds of the NPC Category objects spawning in the " + _MyClass.CategoryName + " Biome.", MessageType.None, true);
						GUILayout.Space(10);

						GenerationTypeEnum = (GenerationType)_MyClass.NPCGenerationType;
						GenerationTypeEnum = (GenerationType)EditorGUILayout.EnumPopup("Generation Type", GenerationTypeEnum);
						_MyClass.NPCGenerationType = (int)GenerationTypeEnum;

						EditorGUILayout.HelpBox("The Generation Type determines if your object is generated by odds or by random. The Random setting will generate any object within the list below by random. The Odds setting will allow users to choose the odds of each object spawning.", MessageType.None);

						GUILayout.Space(15);

						EditorGUILayout.LabelField("Objects", EditorStyles.boldLabel);
						EditorGUILayout.HelpBox("By pressing the Add Object button, you can create an endless amount of potential objects to spawn for this biome each with their own options and conditions.", MessageType.None, true);

						GUILayout.Space(10);

						GUILayout.BeginHorizontal();
						GUILayout.Space(75);

						if (GUILayout.Button("Add Object"))
						{
							GenerateNewSpawnID();
							_MyClass.NPCToSpawn.Add(_gameObject);
							_MyClass.NPCMinGroupAmount.Add(1);
							_MyClass.NPCMaxGroupAmount.Add(3);
							_MyClass.NPCFoldOuts.Add(new bool());
							_MyClass.NPCFoldOutNames.Add("Object Name");
							_MyClass.NPCRarityEnumNumber.Add(new int());
							_MyClass.NPCSpawnRadius.Add(5);
							_MyClass.NPCHeightTypeEnumNumber.Add(1);

							_MyClass.NPCSpawnId.Add(NewSpawnID);
							_MyClass.NPCGroupEnumNumber.Add(1);
							_MyClass.NPCCurrentPopulation.Add(0);
							_MyClass.NPCMaxPopulation.Add(3);
							_MyClass.NPCSpawnHeightMin.Add(0);
							_MyClass.NPCSpawnHeightMax.Add(0);

							EditorUtility.SetDirty(t);

						}
						GUILayout.Space(75);
						GUILayout.EndHorizontal();
						GUILayout.Space(10);

						if(_MyClass.NPCToSpawn != null)
						{
							if(_MyClass.NPCToSpawn.Count > 0)
							{
								for (int j = 0; j < _MyClass.NPCToSpawn.Count; ++j)
								{

									_MyClass.NPCFoldOuts[j] = EditorGUILayout.Foldout(_MyClass.NPCFoldOuts[j], _MyClass.NPCFoldOutNames[j].ToString());

									if (_MyClass.NPCFoldOuts[j])
									{
										GUILayout.Space(10);


										_MyClass.NPCFoldOutNames[j] = EditorGUILayout.TextField("Object Name ", _MyClass.NPCFoldOutNames[j]);
										EditorGUILayout.HelpBox("The Object Name is used to name this object category as well as when the object spawns.", MessageType.None);

										GUILayout.Space(10);

										EditorGUILayout.LabelField("Spawn Id", _MyClass.NPCSpawnId[j]);
										EditorGUILayout.HelpBox("The Spawn Id is used to manually spawn this object using the in-game spawning system. This is helpful for development and testing purposes.", MessageType.None);

										GUILayout.Space(20);

										GUILayout.BeginHorizontal();
										_MyClass.NPCToSpawn[j] = (GameObject)EditorGUILayout.ObjectField("Object to Spawn", _MyClass.NPCToSpawn[j], typeof(GameObject), false);
										GUILayout.EndHorizontal();

										GUILayout.Space(20);

										_MyClass.NPCMaxPopulation[j] = EditorGUILayout.IntSlider ("Population Cap", _MyClass.NPCMaxPopulation[j], 0, 25);
										EditorGUILayout.HelpBox("The Population Cap will help keep too many of the same objects from spawning. Once the Population Cap has been reached, no more of this object will spawn until it's been despawned or destroyed.", MessageType.None, true);

										GUILayout.Space(20);

										if (_MyClass.NPCGroupEnumNumber != null && j > -1)
										{
											if (_MyClass.NPCGroupEnumNumber.Count > 0)
											{
												Crux.CruxClass.GroupEnum GroupEnum = Crux.CruxClass.GroupEnum.No;

												GroupEnum = (Crux.CruxClass.GroupEnum)_MyClass.NPCGroupEnumNumber[j];
												GroupEnum = (Crux.CruxClass.GroupEnum)EditorGUILayout.EnumPopup("Enable Group Spawning", GroupEnum);
												_MyClass.NPCGroupEnumNumber[j] = (int)GroupEnum;

												EditorGUILayout.HelpBox("Enable Group Spawning enables the ability to spawn a randomized number of this object based on the minimum and maximum groups number of this object.", MessageType.None, true);
												GUILayout.Space(10);
											}
										}

										GUILayout.Space(10);

										if (_MyClass.NPCMinGroupAmount != null && j > -1 && _MyClass.NPCGroupEnumNumber[j] == 0)
										{
											if (_MyClass.NPCMinGroupAmount.Count > 0 && _MyClass.NPCMaxGroupAmount.Count > 0)
											{
												EditorGUILayout.BeginHorizontal ();
												GUILayout.Space(30);
												EditorGUILayout.LabelField("Min Group",GUILayout.Width(75));
												_MyClass.NPCMinGroupAmount[j] = EditorGUILayout.IntField(_MyClass.NPCMinGroupAmount[j],GUILayout.Width(75));
												EditorGUILayout.LabelField("Max Group",GUILayout.Width(75));
												_MyClass.NPCMaxGroupAmount[j] = EditorGUILayout.IntField(_MyClass.NPCMaxGroupAmount[j],GUILayout.Width(75));
												EditorGUILayout.EndHorizontal ();
											}

											EditorGUILayout.HelpBox("The Min and Max Group determines the number of times this object can be spawned in one area during a successful spawn.", MessageType.None, true);
											
											GUILayout.Space(20);
										}

										if (_MyClass.NPCRarityEnumNumber != null && j > -1)
										{
											if (_MyClass.NPCRarityEnumNumber.Count > 0)
											{
												Crux.CruxClass.RarityEnum RarityEnum = Crux.CruxClass.RarityEnum.Common;

												if (_MyClass.NPCGenerationType == 1)
												{
													GUILayout.Space(10);
													RarityEnum = (Crux.CruxClass.RarityEnum)_MyClass.NPCRarityEnumNumber[j];
													RarityEnum = (Crux.CruxClass.RarityEnum)EditorGUILayout.EnumPopup("Chance to Spawn", RarityEnum);
													_MyClass.NPCRarityEnumNumber[j] = (int)RarityEnum;

													EditorGUILayout.HelpBox("The Chance to Spawn determines how often this object will spawn when the proper conditions are met.", MessageType.None, true);
													GUILayout.Space(10);
												}
											}
										}

										GUILayout.Space(10);

										//Users who are updating to version 1.1 will need Crux to update missing elements for newly added lists to avoid errors.
										//This does code does this automatically.
										if (_MyClass.NPCHeightTypeEnumNumber.Count == 0 && _MyClass.NPCSpawnId.Count > 0)
										{
											for (int c = 0; c < _MyClass.NPCSpawnId.Count; c++){
												_MyClass.NPCHeightTypeEnumNumber.Add(1);
												_MyClass.NPCSpawnHeightMin.Add(0);
												_MyClass.NPCSpawnHeightMax.Add(0);
											}
										}
										//
										
										if (_MyClass.NPCHeightTypeEnumNumber != null && j > -1)
										{
											if (_MyClass.NPCHeightTypeEnumNumber.Count > 0)
											{
												Crux.CruxClass.HeightTypeEnum HeightTypeEnum = Crux.CruxClass.HeightTypeEnum.Land;

												if (_MyClass.NPCGenerationType == 1)
												{
													GUILayout.Space(10);
													HeightTypeEnum = (Crux.CruxClass.HeightTypeEnum)_MyClass.NPCHeightTypeEnumNumber[j];
													HeightTypeEnum = (Crux.CruxClass.HeightTypeEnum)EditorGUILayout.EnumPopup("AI Type", HeightTypeEnum);
													_MyClass.NPCHeightTypeEnumNumber[j] = (int)HeightTypeEnum;
													
													EditorGUILayout.HelpBox("The AI Type determines the height type that will be used for spawning this AI. The Air and Water option will allow you to customizable the spawning heights that will then be randomized when this AI is spawned.", MessageType.None, true);
													GUILayout.Space(10);
												}
											}
										}
										
										GUILayout.Space(10);

										if (_MyClass.NPCHeightTypeEnumNumber[j] == 2 || _MyClass.NPCHeightTypeEnumNumber[j] == 3){
											if (_MyClass.NPCSpawnHeightMin.Count > 0)
											{
												EditorGUILayout.BeginHorizontal ();
												GUILayout.Space(8);
												EditorGUILayout.LabelField("Min Spawn Height",GUILayout.Width(105));
												_MyClass.NPCSpawnHeightMin[j] = EditorGUILayout.IntField(_MyClass.NPCSpawnHeightMin[j],GUILayout.Width(45));
												EditorGUILayout.LabelField("Max Spawn Height",GUILayout.Width(109));
												_MyClass.NPCSpawnHeightMax[j] = EditorGUILayout.IntField(_MyClass.NPCSpawnHeightMax[j],GUILayout.Width(45));
												EditorGUILayout.EndHorizontal ();
											}
											
											EditorGUILayout.HelpBox("The Min and Max Spawn Height determines the randomized height that your AI will spawn. This is useful for flying and water AI types.", MessageType.None, true);
											
											GUILayout.Space(20);
										}

										_MyClass.NPCSpawnRadius[j] = EditorGUILayout.IntSlider ("Spawn Radius", _MyClass.NPCSpawnRadius[j], 1, 25);
										EditorGUILayout.HelpBox("The Spawn Radius controls how close the groups for this object will spawn.", MessageType.None, true);

										GUILayout.Space(10);

										GUILayout.BeginHorizontal();
										GUILayout.Space(75);
										if (GUILayout.Button("Remove this Object"))
										{
											_MyClass.NPCToSpawn.RemoveAt(j);
											_MyClass.NPCMinGroupAmount.RemoveAt(j);
											_MyClass.NPCMaxGroupAmount.RemoveAt(j);
											_MyClass.NPCFoldOuts.RemoveAt(j);
											_MyClass.NPCFoldOutNames.RemoveAt(j);
											_MyClass.NPCRarityEnumNumber.RemoveAt(j);
											_MyClass.NPCSpawnRadius.RemoveAt(j);
											_MyClass.NPCHeightTypeEnumNumber.RemoveAt(j);

											_MyClass.NPCSpawnId.RemoveAt(j);
											_MyClass.NPCGroupEnumNumber.RemoveAt(j); 
											_MyClass.NPCCurrentPopulation.RemoveAt(j); 
											_MyClass.NPCMaxPopulation.RemoveAt(j);
											_MyClass.NPCSpawnHeightMin.RemoveAt(j);
											_MyClass.NPCSpawnHeightMax.RemoveAt(j);

											EditorUtility.SetDirty(t);

											--j;
										}
										GUILayout.Space(75);
										GUILayout.EndHorizontal();

										GUILayout.Space(20);
									}

									GUILayout.Space(10);
								}
							}


							if(_MyClass.NPCToSpawn.Count == 0)
							{
								GUILayout.BeginHorizontal();
								GUILayout.Space(50);
								EditorGUILayout.HelpBox("There are no NPC objects for this Biome. To create one, press the Add Object button.", MessageType.Info);
								GUILayout.Space(20);
								GUILayout.EndHorizontal();
								GUILayout.Space(10);
							}

						}
					}
						
					EditorGUILayout.Space ();

					EditorGUILayout.LabelField("Remove Biome", EditorStyles.boldLabel);
					EditorGUILayout.HelpBox("The button below will delete the " + _MyClass.CategoryName + " Biome.", MessageType.None, true);
					if(GUILayout.Button("Remove this Biome"))
					{
						Biome.DeleteArrayElementAtIndex(i);
					}

					EditorGUILayout.Space ();
					EditorGUILayout.Space ();
					EditorGUILayout.Space ();
					EditorGUILayout.Space ();
				}
			}
		}

		if (GUI.changed && !EditorApplication.isPlaying) 
		{
			EditorUtility.SetDirty(t); 
		}
			
		GetTarget.ApplyModifiedProperties();

	}

	void OnSceneGUI () 
	{
		Crux self = (Crux)target;

		if (self._PlayerObject != null && t.useVisualRadii)
		{
			Handles.color = self.MinSpawnRadiusColor;
			Handles.DrawSolidDisc(self._PlayerObject.position, Vector3.up, self._MinRadius);

			Handles.color = self.MaxSpawnRadiusColor;
			Handles.DrawSolidDisc(self._PlayerObject.position, Vector3.up, self._MaxRadius);

			Handles.color = self.DespawnRadiusColor;
			Handles.DrawSolidDisc(self._PlayerObject.position, Vector3.up, self._DespawnRadius);
		}
	
		SceneView.RepaintAll();
	}
}

