using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Crux : MonoBehaviour 
{
	/*
	#if USING_UNISTORM
	UniStormWeatherSystem_C UniStormSystem;
	#endif
	*/

	public GameObject SpawnNode;
	public int SpawnNodeDeactivateDistance = 300;
	public GameObject ObjectPool;
	public int UseObjectPooling = 2;
	public int SpawnType = 1;
	public GameObject CurrentNode;
	public GameObject ChildrenHolder;
	public float SpawnNodeUpdateFrequency = 1;

	string AILayerMaskString;
	public LayerMask AILayerMask = 0;
	public int AILayerMaskDectectionDistance = 100;

	public Terrain _ActiveTerrain;
	float _UpdateTick;
	public float _UpdateTickFrequency = 1;

	public Color MinSpawnRadiusColor = new Color32(255,0,0,25);
	public Color MaxSpawnRadiusColor = new Color32(0,233,11,25);
	public Color DespawnRadiusColor = new Color32(0,76,0255,25);

	public Transform _PlayerObject;
	public GameObject _TerrainChecker;

	public int StartingAIAmount = 10;
	public int _CurrentSpawnedObjects;
	public int _MaxObjectsToSpawn = 20;
	public int _MinRadius = 150;
	public int _MaxRadius = 300;
	public float _SpawnOffset = 1;
	public float _DespawnRadius = 450;
	TerrainInfo _TI;
	GameObject _InstancedChecker;
	Vector3 _PositionToSpawn;
	Vector3 pos;
	public int minSteepness = 0;
	public int maxSteepness = 40;
	public int GroupAmount;

	public int TabNumberTop = 0;
	public int CategoryTabNumber = 0;
	public int PlayerTransformType = 1;
	public string PlayerTransformName = "Enter Player Name";

	public List<GameObject> _SpawnedObjects = new List<GameObject>();
	public List<string> SpawnIDList = new List<string>();

	public string SpawnIDsFilePath = "D:/CruxSpawnIDs.txt";
	public string SpawnIDsFileName = "CruxSpawnIDs";
	public List<string> SpawnIDs = new List<string>();
	public string EnteredSpawnID;
	public bool SpawnIDsEnabled = false;
	public int ObjectsPlaceInList;
	public bool useLayerDetection = false;
	public bool writeSpawnIDsToTxtFile = false;
	public bool useVisualRadii = true;

	//UI
	public Text InputText;
	public bool SpawnIDSystem = false;
	public GameObject SpawnIDMenu;
	public bool MenuToggle = false;
	public bool ObjectSkipped = false;

	GameObject _InstancedObject;
	int GetIndex;
	string GetSpawnID;

	[System.Serializable]
	public class CruxClass
	{
		public List<GameObject> WildlifeSpawnNodes = new List<GameObject>();

		public string CategoryName = "New Biome";

		public bool collapse;
		public List<Texture> TerrainTextures = new List<Texture>();

		public List<GameObject> WildlifeToSpawn = new List<GameObject>();
		public int WildlifeOdds = 75;
		public List<int> WildlifeRarityEnumNumber = new List<int>();
		public List<bool> WildlifeFoldOuts = new List<bool>();
		public List<string> WildlifeFoldOutNames = new List<string>();
		public List<int> WildlifeMinGroupAmount = new List<int>();
		public List<int> WildlifeMaxGroupAmount = new List<int>();
		public List<int> WildlifeSpawnRadius = new List<int>();
		public List<GameObject> CommonWildlife = new List<GameObject>();
		public List<GameObject> UncommonWildlife = new List<GameObject>();
		public List<GameObject> RareWildlife = new List<GameObject>();
		public List<GameObject> UltraRareWildlife = new List<GameObject>();
		public List<string> WildlifeSpawnId = new List<string>();
		public List<int> WildlifeMinSteepness = new List<int>();
		public List<int> WildlifeMaxSteepness = new List<int>();
		public int WildlifeGenerationType = 1;
		public int WildlifeCounter = 0;
		public GameObject WildlifeObjectToSpawn;
		public List<int> WildlifeGroupEnumNumber = new List<int>();
		public List<int> WildlifeCurrentPopulation = new List<int>();
		public List<int> WildlifeMaxPopulation = new List<int>();
		public List<GameObject> WildlifeCurrentGameObjects = new List<GameObject>();
		public Vector3 WildlifePositionToSpawn;
		public List<int> WildlifeSpawnHeightMin = new List<int>();
		public List<int> WildlifeSpawnHeightMax = new List<int>();
		public List<int> WildlifeHeightTypeEnumNumber = new List<int>();

		public List<int> WildlifeSpawnUniStormTime = new List<int>();

		public List<GameObject> CreaturesToSpawn = new List<GameObject>();
		public int CreaturesOdds = 20;
		public List<int> CreaturesRarityEnumNumber = new List<int>();
		public List<bool> CreaturesFoldOuts = new List<bool>();
		public List<string> CreaturesFoldOutNames = new List<string>();
		public List<int> CreaturesMinGroupAmount = new List<int>();
		public List<int> CreaturesMaxGroupAmount = new List<int>();
		public List<int> CreaturesSpawnRadius = new List<int>();
		public List<GameObject> CommonCreatures = new List<GameObject>();
		public List<GameObject> UncommonCreatures = new List<GameObject>();
		public List<GameObject> RareCreatures = new List<GameObject>();
		public List<GameObject> UltraRareCreatures = new List<GameObject>();
		public List<string> CreaturesSpawnId = new List<string>();
		public int CreaturesGenerationType = 1;
		public int CreaturesCounter = 0;
		public GameObject CreaturesObjectToSpawn;
		public List<int> CreaturesGroupEnumNumber = new List<int>();
		public List<int> CreaturesMaxPopulation = new List<int>();
		public List<int> CreaturesCurrentPopulation = new List<int>();
		public Vector3 CreaturesPositionToSpawn;
		public List<int> CreaturesSpawnHeightMin = new List<int>();
		public List<int> CreaturesSpawnHeightMax = new List<int>();
		public List<int> CreaturesHeightTypeEnumNumber = new List<int>();

		public List<GameObject> NPCToSpawn = new List<GameObject>();
		public int NPCOdds = 5;
		public List<int> NPCRarityEnumNumber = new List<int>();
		public List<bool> NPCFoldOuts = new List<bool>();
		public List<string> NPCFoldOutNames = new List<string>();
		public List<int> NPCMinGroupAmount = new List<int>();
		public List<int> NPCMaxGroupAmount = new List<int>();
		public List<int> NPCSpawnRadius = new List<int>();
		public List<GameObject> CommonNPC = new List<GameObject>();
		public List<GameObject> UncommonNPC = new List<GameObject>();
		public List<GameObject> RareNPC = new List<GameObject>();
		public List<GameObject> UltraRareNPC = new List<GameObject>();
		public List<string> NPCSpawnId = new List<string>();
		public int NPCGenerationType = 1;
		public int NPCCounter = 0;
		public GameObject NPCObjectToSpawn;
		public List<int> NPCGroupEnumNumber = new List<int>();
		public List<int> NPCMaxPopulation = new List<int>();
		public List<int> NPCCurrentPopulation = new List<int>();
		public List<int> NPCSpawnHeightMin = new List<int>();
		public List<int> NPCSpawnHeightMax = new List<int>();
		public List<int> NPCHeightTypeEnumNumber = new List<int>();

		public int TabNumber = 0;
		public enum RarityEnum {Common, Uncommon, Rare, UltraRare};
		public enum HeightTypeEnum {Land = 1, Air = 2, Water = 3};
		public enum GroupEnum {Yes, No};
		public enum UniStormSpawnTimeEnum {Morning = 1, Day, Evening, Night};

		public int RandomObject;
	}

	//This is our list we want to use to represent our class as an array.
	public List<CruxClass> Biome = new List<CruxClass>(1);

	//Create our Terrain Checker component. This object is what gets and sends all the spawning information.
	void Awake ()
	{
		_InstancedChecker = (GameObject)Instantiate (Resources.Load("Terrain Info"), new Vector3(0, 0, 0),  Quaternion.identity);
		SpawnNode = Resources.Load("Crux Spawn Node") as GameObject;

		_TI = _InstancedChecker.GetComponent<TerrainInfo>();

		if (Terrain.activeTerrain == null)
		{
			Debug.LogError("No terrain could be found, Crux has been disabled. Please ensure you have an active terrain in your scene. If your terrain is being procedurally built, keep Crux inactive until it has finished building.");
			_TI = null;
		}

		if (Biome.Count == 0)
		{
			Debug.LogError("You currently have 0 biomes. Crux needs at least 1 biome in order to work. To creat one, go to the Biome Options, press the 'Add Biome' button, and create an object for it. Ensure that you have also assigned textures to define your biome.");
		}

		for(int i = 0; i < Biome.Count; i++)
		{
			if (Biome[i].TerrainTextures.Count == 0)
			{
				Debug.LogError("Your " + Biome[i].CategoryName + " biome has 0 textures. Please assign at least 1 texture to this biome by pressing the 'Add Texture' button and assigning a texture from your terrain to it. This will be used to spawn objects to the portions of your terrain with this texture and any others you apply.");
			}
		}

		if (UseObjectPooling == 1){
			ObjectPool = new GameObject();
			ObjectPool.transform.SetParent(transform);
			ObjectPool.name = "(Crux) Object Pool";
		}

		if (_TI != null)
		{
			_TI.terrain = Terrain.activeTerrain;
			_TI.terrainData = _TI.terrain.terrainData;
			_TI.terrainPos = _TI.terrain.transform.position;
			_InstancedChecker.transform.parent = transform;
		}

		AILayerMaskString = LayerMask.LayerToName(AILayerMask.value);

		if (AILayerMaskString == "Default" && useLayerDetection)
		{
			useLayerDetection = false;
			Debug.LogWarning("AI Detection has been disabled because the AI Detection Layer must be a layer other than Default.");
		}

		if (_TI != null)
		{
			_SpawnOffset = _TI.terrain.transform.position.y + _SpawnOffset;
		}
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Y) && SpawnIDsEnabled)
		{
			MenuToggle = !MenuToggle;
			SpawnIDMenu.SetActive(MenuToggle);
		}
	}
		
	IEnumerator SpawnCheck ()
	{
		while (true) 
		{
			yield return new WaitForSeconds(_UpdateTickFrequency);
			Create();
		}
	}

	void Start () 
	{
		StartCoroutine(InitializeCrux());

		/*
		#if USING_UNISTORM
		UniStormSystem = FindObjectOfType<UniStormWeatherSystem_C>();
		#endif
		*/
	}

	IEnumerator InitializeCrux ()
	{
		AssignRarityCategories();

		//If using spawn IDs, create our UI components.
		//If you'd like to have your own custom UI for spawn IDs, this portion can be removed.
		if (SpawnIDsEnabled)
		{
			GameObject CruxUI = Instantiate(Resources.Load("Crux UI")) as GameObject;

			foreach (Text T in CruxUI.GetComponentsInChildren<Text>())
			{
				if (T.gameObject.name == "CruxInputText")
				{
					InputText = T;
				}
			}

			Button SpawnButton = CruxUI.GetComponentInChildren<Button>();
			SpawnButton.onClick.AddListener(delegate {SpawnByID();});

			if (GameObject.Find("EventSystem") == null)
			{
				Instantiate(Resources.Load("CruxUIEventSystem"));
			}

			CruxUI.SetActive(false);
			SpawnIDMenu = CruxUI;
		}

		if (PlayerTransformType == 1)
		{
			if (_PlayerObject == null)
			{
				_TI = null;
				Debug.LogError("Your Player Transform object has not been assigned, Crux has been disabled. You need to assign your player object to the Player Transform slot of the Crux Editor under Spawning Options. If your player is instantiated, use the Instantiated Player Transform Type.");
			}
		}

		if (PlayerTransformType == 2)
		{
			//If the instantiated player option is enabled, add a slight delay when looking for our instantiated player to ensure it has been spawned.
			yield return new WaitForSeconds(0.1f);
			_PlayerObject = GameObject.Find(PlayerTransformName).transform;
		}

		if (_TI != null)
		{
			for (int i = 0; i < StartingAIAmount; i++)
			{
				Create();
			}
		}
			
		StartCoroutine(SpawnCheck());
	}

	//Assigns all of our spawnanle objects to rarity categories using lists.
	//These lists are then used to spawn AI according to the generated odds and biomes.
	void AssignRarityCategories ()
	{
		for(int i = 0; i < Biome.Count; i++)
		{
			foreach (int Rarity in Biome[i].WildlifeRarityEnumNumber)
			{
				if (Rarity == 0)
				{
					if (!Biome[i].CommonWildlife.Contains(Biome[i].WildlifeToSpawn[Biome[i].WildlifeCounter]))
					{
						Biome[i].CommonWildlife.Add(Biome[i].WildlifeToSpawn[Biome[i].WildlifeCounter]);
					}
				}
					
				if (Rarity == 1)
				{
					if (!Biome[i].UncommonWildlife.Contains(Biome[i].WildlifeToSpawn[Biome[i].WildlifeCounter]))
					{
						Biome[i].UncommonWildlife.Add(Biome[i].WildlifeToSpawn[Biome[i].WildlifeCounter]);
					}
				}

				if (Rarity == 2)
				{
					if (!Biome[i].RareWildlife.Contains(Biome[i].WildlifeToSpawn[Biome[i].WildlifeCounter]))
					{
						Biome[i].RareWildlife.Add(Biome[i].WildlifeToSpawn[Biome[i].WildlifeCounter]);
					}
				}

				if (Rarity == 3)
				{
					if (!Biome[i].UltraRareWildlife.Contains(Biome[i].WildlifeToSpawn[Biome[i].WildlifeCounter]))
					{
						Biome[i].UltraRareWildlife.Add(Biome[i].WildlifeToSpawn[Biome[i].WildlifeCounter]);
					}
				}

				Biome[i].WildlifeCounter++;
			}

			foreach (int Rarity in Biome[i].CreaturesRarityEnumNumber)
			{
				if (Rarity == 0)
				{
					if (!Biome[i].CommonCreatures.Contains(Biome[i].CreaturesToSpawn[Biome[i].CreaturesCounter]))
					{
						Biome[i].CommonCreatures.Add(Biome[i].CreaturesToSpawn[Biome[i].CreaturesCounter]);
					}
				}

				if (Rarity == 1)
				{
					if (!Biome[i].UncommonCreatures.Contains(Biome[i].CreaturesToSpawn[Biome[i].CreaturesCounter]))
					{
						Biome[i].UncommonCreatures.Add(Biome[i].CreaturesToSpawn[Biome[i].CreaturesCounter]);
					}
				}

				if (Rarity == 2)
				{
					if (!Biome[i].RareCreatures.Contains(Biome[i].CreaturesToSpawn[Biome[i].CreaturesCounter]))
					{
						Biome[i].RareCreatures.Add(Biome[i].CreaturesToSpawn[Biome[i].CreaturesCounter]);
					}
				}

				if (Rarity == 3)
				{
					if (!Biome[i].UltraRareCreatures.Contains(Biome[i].CreaturesToSpawn[Biome[i].CreaturesCounter]))
					{
						Biome[i].UltraRareCreatures.Add(Biome[i].CreaturesToSpawn[Biome[i].CreaturesCounter]);
					}
				}

				Biome[i].CreaturesCounter++;
			}

			foreach (int Rarity in Biome[i].NPCRarityEnumNumber)
			{
				if (Rarity == 0)
				{
					if (!Biome[i].CommonNPC.Contains(Biome[i].NPCToSpawn[Biome[i].NPCCounter]))
					{
						Biome[i].CommonNPC.Add(Biome[i].NPCToSpawn[Biome[i].NPCCounter]);
					}
				}

				if (Rarity == 1)
				{
					if (!Biome[i].UncommonNPC.Contains(Biome[i].NPCToSpawn[Biome[i].NPCCounter]))
					{
						Biome[i].UncommonNPC.Add(Biome[i].NPCToSpawn[Biome[i].NPCCounter]);
					}
				}

				if (Rarity == 2)
				{
					if (!Biome[i].RareNPC.Contains(Biome[i].NPCToSpawn[Biome[i].NPCCounter]))
					{
						Biome[i].RareNPC.Add(Biome[i].NPCToSpawn[Biome[i].NPCCounter]);
					}
				}

				if (Rarity == 3)
				{
					if (!Biome[i].UltraRareNPC.Contains(Biome[i].NPCToSpawn[Biome[i].NPCCounter]))
					{
						Biome[i].UltraRareNPC.Add(Biome[i].NPCToSpawn[Biome[i].NPCCounter]);
					}
				}

				Biome[i].NPCCounter++;
			}
		}
	}

	//This function handles all of our spawning logic. 
	//It gets all biomes' info, spawning info, and spawns AI accordingly
	void Create () 
	{
		if (_TI != null)
		{
			if (_CurrentSpawnedObjects < _MaxObjectsToSpawn)
			{
				_PositionToSpawn = (new Vector3(_PlayerObject.position.x, _PlayerObject.position.y, _PlayerObject.position.z)) + UnityEngine.Random.insideUnitSphere * _MaxRadius;
				_InstancedChecker.transform.position = new Vector3(_PositionToSpawn.x, _PlayerObject.position.y + 50, _PositionToSpawn.z);
				_TI.UpdateTerrainInfo();
				pos.y = _TI.terrain.SampleHeight(_PositionToSpawn);
				_PositionToSpawn = new Vector3 (_InstancedChecker.transform.position.x, pos.y,_InstancedChecker.transform.position.z);

				if(Vector3.Distance(_PlayerObject.position, _PositionToSpawn) >= _MinRadius && !Physics.CheckSphere(_PositionToSpawn, AILayerMaskDectectionDistance, LayerMask.GetMask(AILayerMaskString)) && useLayerDetection || Vector3.Distance(_PlayerObject.position, _PositionToSpawn) >= _MinRadius && !useLayerDetection)
				{
					if (!_TI.positionInvalid && _TI.terrainAngle >= minSteepness && _TI.terrainAngle <= maxSteepness)
					{
						Texture _Texture = _TI.terrainData.splatPrototypes[_TI.surfaceIndex].texture;

						for(int i = 0; i < Biome.Count; i++)
						{
							//Check the texture and find the matching Biome. Spawn AI according to the Biome
							if (Biome[i].TerrainTextures.Contains(_Texture)) 
							{
								float random = UnityEngine.Random.value;
								float Sum = Biome[i].WildlifeOdds + Biome[i].CreaturesOdds + Biome[i].NPCOdds;
								float generatedWildlifeOdds = Biome[i].WildlifeOdds/Sum;
								float generatedCreaturesOdds = (Biome[i].CreaturesOdds/Sum) + generatedWildlifeOdds;
								float generatedNPCOdds = (Biome[i].NPCOdds/Sum) + generatedWildlifeOdds + generatedCreaturesOdds;

								//Spawn our Wildlife objects
								if (Biome[i].WildlifeToSpawn.Count > 0 && random < generatedWildlifeOdds)
								{
									float GeneratedOdds = UnityEngine.Random.Range(1,101);

									//If using the random wildlife setting, randomly pick an object from the whole list of wildlife objects.
									if (Biome[i].WildlifeGenerationType == 0)
									{
										if (Biome[i].WildlifeToSpawn.Count > 0)
										{
											Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].WildlifeToSpawn.Count);
											Biome[i].WildlifeObjectToSpawn = Biome[i].WildlifeToSpawn[Biome[i].RandomObject]; 
										}
									}

									//If using the odds based generation type, pick the spawn category according to rarity of the generated odds.
									//When the rarity has beed reached, randomly pick an object within that rarity category
									if (Biome[i].WildlifeGenerationType == 1)
									{
										//Common - 55%
										if (GeneratedOdds >= 45)
										{
											if (Biome[i].CommonWildlife.Count > 0)
											{
												Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].CommonWildlife.Count);
												Biome[i].WildlifeObjectToSpawn = Biome[i].CommonWildlife[Biome[i].RandomObject]; 
											}
										}
										//Uncommon - 30%
										else if (GeneratedOdds < 45 && GeneratedOdds >= 15)
										{
											if (Biome[i].UncommonWildlife.Count > 0)
											{
												Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].UncommonWildlife.Count);
												Biome[i].WildlifeObjectToSpawn = Biome[i].UncommonWildlife[Biome[i].RandomObject]; 
											}
										}
										//Rare - 10%
										else if (GeneratedOdds < 15 && GeneratedOdds >= 5)
										{
											if (Biome[i].RareWildlife.Count > 0)
											{
												Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].RareWildlife.Count);
												Biome[i].WildlifeObjectToSpawn = Biome[i].RareWildlife[Biome[i].RandomObject]; 
											}
										}
										//Ultra Rare - 5%
										else if (GeneratedOdds < 5)
										{
											if (Biome[i].UltraRareWildlife.Count > 0)
											{
												Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].UltraRareWildlife.Count);
												Biome[i].WildlifeObjectToSpawn = Biome[i].UltraRareWildlife[Biome[i].RandomObject]; 
											}
										}
									}

									if (Biome[i].WildlifeToSpawn.Count > 0 && Biome[i].WildlifeObjectToSpawn != null)
									{
										ObjectsPlaceInList = Biome[i].WildlifeToSpawn.IndexOf(Biome[i].WildlifeObjectToSpawn);
									}

									if (Biome[i].WildlifeObjectToSpawn != null)
									{
										if (Biome[i].WildlifeGroupEnumNumber.Count > 0)
										{
											if (Biome[i].WildlifeGroupEnumNumber[ObjectsPlaceInList] == 0)
											{
												GroupAmount = UnityEngine.Random.Range(Biome[i].WildlifeMinGroupAmount[ObjectsPlaceInList], Biome[i].WildlifeMaxGroupAmount[ObjectsPlaceInList]+1);
											}
										}

										if (Biome[i].WildlifeObjectToSpawn != null)
										{
											if (Biome[i].WildlifeGroupEnumNumber[ObjectsPlaceInList] == 1)
											{
												GroupAmount = 1;
											}
										}
											
										//Spawn the generated object, but only if the population cap for that specific object hasn't been reached
										if (Biome[i].WildlifeObjectToSpawn != null)
										{
											//Spawn Nodes
											//When using the Spawning Node setting, set all spawned AI as children to a node. This node will be set as inactive until a player is within range.
											if (SpawnType == 1 && Biome[i].WildlifeCurrentPopulation[ObjectsPlaceInList] < Biome[i].WildlifeMaxPopulation[ObjectsPlaceInList])
											{
												if (UseObjectPooling == 2){
													CurrentNode = new GameObject();
												}
												else if (UseObjectPooling == 1){
													CurrentNode = CruxPool.Spawn(SpawnNode, transform.position, Quaternion.identity);
												}

												CurrentNode.name = "Crux Spawn Node";
												Vector3 NodeSpawn = _PositionToSpawn;
												float height =_TI.terrain.SampleHeight(NodeSpawn);
												NodeSpawn = new Vector3(NodeSpawn.x, height + _SpawnOffset + _TI.terrain.transform.position.y, NodeSpawn.z);
												CurrentNode.transform.parent = transform;
												CurrentNode.transform.position = NodeSpawn;

												if (UseObjectPooling == 2){
													CurrentNode.AddComponent<SpawnNode>();
													ChildrenHolder = new GameObject();
													ChildrenHolder.name = "Object Parent";
													ChildrenHolder.transform.parent = CurrentNode.transform;
													ChildrenHolder.SetActive(false);
												}
												else if (UseObjectPooling == 1){
													ChildrenHolder = CurrentNode.transform.GetChild (0).gameObject; 
													ChildrenHolder.SetActive(false);
												}

												//Gets the spawn point system and applies the needed settings
												SpawnNode SpawnNodeSystem = CurrentNode.GetComponent<SpawnNode>();
												SpawnNodeSystem.UpdateFrequency = SpawnNodeUpdateFrequency;
												SpawnNodeSystem.Initialize();
												SpawnNodeSystem.Player = _PlayerObject.gameObject;
												SpawnNodeSystem.ChildrenHolder = ChildrenHolder;
												SpawnNodeSystem.despawnDistance = (int)_DespawnRadius;
												SpawnNodeSystem.deactivateDistance = SpawnNodeDeactivateDistance;

												if (UseObjectPooling == 2){
													SpawnNodeSystem.UsingObjectPooling = false;
												}
												else if (UseObjectPooling == 1){
													SpawnNodeSystem.UsingObjectPooling = true;
												}
											}

											for (int o = 0; o < GroupAmount; o++)
											{
												if (Biome[i].WildlifeCurrentPopulation[ObjectsPlaceInList] < Biome[i].WildlifeMaxPopulation[ObjectsPlaceInList])
												{
													if (UseObjectPooling == 2){
														_InstancedObject = (GameObject)Instantiate (Biome[i].WildlifeObjectToSpawn, new Vector3(_PositionToSpawn.x, _PositionToSpawn.y + _SpawnOffset, _PositionToSpawn.z), Quaternion.Euler(_TI.terrainData.GetInterpolatedNormal(_TI.normalizedPos.x, _TI.normalizedPos.y)));
													}
													else if (UseObjectPooling == 1){
														_InstancedObject = CruxPool.Spawn(Biome[i].WildlifeObjectToSpawn, new Vector3(_PositionToSpawn.x, _PositionToSpawn.y + _SpawnOffset, _PositionToSpawn.z), Quaternion.Euler(_TI.terrainData.GetInterpolatedNormal(_TI.normalizedPos.x, _TI.normalizedPos.y)));
													}

													_InstancedObject.transform.parent = transform;
													_InstancedObject.transform.position = UnityEngine.Random.insideUnitSphere * Biome[i].WildlifeSpawnRadius[ObjectsPlaceInList] + new Vector3 (_PositionToSpawn.x, _PositionToSpawn.y + _SpawnOffset, _PositionToSpawn.z);
													_InstancedObject.gameObject.name = Biome[i].WildlifeFoldOutNames[ObjectsPlaceInList];

													//Recalculate our AI's Y position after the group spawning position has been created
													//If using the Air or Water AI Type, randomize the height.
													float height =_TI.terrain.SampleHeight(_InstancedObject.transform.position); 

													//Spawn height
													if (Biome[i].WildlifeHeightTypeEnumNumber[ObjectsPlaceInList] == 1){
														_InstancedObject.transform.position = new Vector3(_InstancedObject.transform.position.x, height + _SpawnOffset, _InstancedObject.transform.position.z);
													}
													else if (Biome[i].WildlifeHeightTypeEnumNumber[ObjectsPlaceInList] == 2 || Biome[i].WildlifeHeightTypeEnumNumber[ObjectsPlaceInList] == 3){
														_InstancedObject.transform.position = new Vector3(_InstancedObject.transform.position.x, height + _SpawnOffset + UnityEngine.Random.Range(Biome[i].WildlifeSpawnHeightMin[ObjectsPlaceInList],Biome[i].WildlifeSpawnHeightMax[ObjectsPlaceInList]+1), _InstancedObject.transform.position.z);
													}

													//Assign the spawned AI to the current spawn node
													if (SpawnType == 1)
													{
														_InstancedObject.transform.parent = ChildrenHolder.transform;
													}

													Biome[i].WildlifeCurrentPopulation[ObjectsPlaceInList]++;
													_CurrentSpawnedObjects++;
													_SpawnedObjects.Add(_InstancedObject);
													SpawnIDList.Add(Biome[i].WildlifeSpawnId[ObjectsPlaceInList]);

												}
											}
										}
									}
								}

								//Spawn our Creature objects
								else if (Biome[i].CreaturesToSpawn.Count > 0 && random < generatedCreaturesOdds)
								{
									float GeneratedOdds = UnityEngine.Random.Range(1,101);

									if (Biome[i].CreaturesGenerationType == 0)
									{
										if (Biome[i].CreaturesToSpawn.Count > 0)
										{
											Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].CreaturesToSpawn.Count);
											Biome[i].CreaturesObjectToSpawn = Biome[i].CreaturesToSpawn[Biome[i].RandomObject]; 
										}
									}

									//If using the odds based generation type, pick the spawn category according to rarity of the generated odds.
									//When the rarity has beed reached, randomly pick an object within that rarity category
									if (Biome[i].CreaturesGenerationType == 1)
									{
										//Common - 55%
										if (GeneratedOdds >= 45)
										{
											if (Biome[i].CommonCreatures.Count > 0)
											{
												Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].CommonCreatures.Count);
												Biome[i].CreaturesObjectToSpawn = Biome[i].CommonCreatures[Biome[i].RandomObject]; 
											}
										}
										//Uncommon - 30%
										else if (GeneratedOdds < 45 && GeneratedOdds >= 15)
										{
											if (Biome[i].UncommonCreatures.Count > 0)
											{
												Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].UncommonCreatures.Count);
												Biome[i].CreaturesObjectToSpawn = Biome[i].UncommonCreatures[Biome[i].RandomObject]; 
											}
										}
										//Rare - 10%
										else if (GeneratedOdds < 15 && GeneratedOdds >= 5)
										{
											if (Biome[i].RareCreatures.Count > 0)
											{
												Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].RareCreatures.Count);
												Biome[i].CreaturesObjectToSpawn = Biome[i].RareCreatures[Biome[i].RandomObject]; 
											}
										}
										//Ultra Rare - 5%
										else if (GeneratedOdds < 5)
										{
											if (Biome[i].UltraRareCreatures.Count > 0)
											{
												Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].UltraRareCreatures.Count);
												Biome[i].CreaturesObjectToSpawn = Biome[i].UltraRareCreatures[Biome[i].RandomObject]; 
											}
										}
									}

									if (Biome[i].CreaturesToSpawn.Count > 0 && Biome[i].CreaturesObjectToSpawn != null)
									{
										ObjectsPlaceInList = Biome[i].CreaturesToSpawn.IndexOf(Biome[i].CreaturesObjectToSpawn);
									}

									if (Biome[i].CreaturesObjectToSpawn != null)
									{
										if (Biome[i].CreaturesGroupEnumNumber.Count > 0 && Biome[i].CreaturesGroupEnumNumber != null)
										{
											if (Biome[i].CreaturesGroupEnumNumber[ObjectsPlaceInList] == 0)
											{
												GroupAmount = UnityEngine.Random.Range(Biome[i].CreaturesMinGroupAmount[ObjectsPlaceInList], Biome[i].CreaturesMaxGroupAmount[ObjectsPlaceInList]+1);
											}
										}

										if (Biome[i].CreaturesObjectToSpawn != null)
										{
											if (Biome[i].CreaturesGroupEnumNumber[ObjectsPlaceInList] == 1)
											{
												GroupAmount = 1;
											}
										}

										if (Biome[i].CreaturesObjectToSpawn != null)
										{
											//Spawn Nodes
											//When using the Spawning Node setting, set all spawned AI as children to a node. This node will be set as inactive until a player is within range.
											if (SpawnType == 1 && Biome[i].CreaturesCurrentPopulation[ObjectsPlaceInList] < Biome[i].CreaturesMaxPopulation[ObjectsPlaceInList])
											{
												if (UseObjectPooling == 2){
													CurrentNode = new GameObject();
												}
												else if (UseObjectPooling == 1){
													CurrentNode = CruxPool.Spawn(SpawnNode, transform.position, Quaternion.identity);
												}
												
												CurrentNode.name = "Crux Spawn Node";
												Vector3 NodeSpawn = _PositionToSpawn;
												float height =_TI.terrain.SampleHeight(NodeSpawn);
												NodeSpawn = new Vector3(NodeSpawn.x, height + _SpawnOffset + _TI.terrain.transform.position.y, NodeSpawn.z);
												CurrentNode.transform.parent = transform;
												CurrentNode.transform.position = NodeSpawn;
												
												if (UseObjectPooling == 2){
													CurrentNode.AddComponent<SpawnNode>();
													ChildrenHolder = new GameObject();
													ChildrenHolder.name = "Object Parent";
													ChildrenHolder.transform.parent = CurrentNode.transform;
													ChildrenHolder.SetActive(false);
												}
												else if (UseObjectPooling == 1){
													ChildrenHolder = CurrentNode.transform.GetChild (0).gameObject; 
													ChildrenHolder.SetActive(false);
												}
												
												//Gets the spawn point system and applies the needed settings
												SpawnNode SpawnNodeSystem = CurrentNode.GetComponent<SpawnNode>();
												SpawnNodeSystem.UpdateFrequency = SpawnNodeUpdateFrequency;
												SpawnNodeSystem.Initialize();
												SpawnNodeSystem.Player = _PlayerObject.gameObject;
												SpawnNodeSystem.ChildrenHolder = ChildrenHolder;
												SpawnNodeSystem.despawnDistance = (int)_DespawnRadius;
												SpawnNodeSystem.deactivateDistance = SpawnNodeDeactivateDistance;
												
												if (UseObjectPooling == 2){
													SpawnNodeSystem.UsingObjectPooling = false;
												}
												else if (UseObjectPooling == 1){
													SpawnNodeSystem.UsingObjectPooling = true;
												}
											}

											for (int o = 0; o < GroupAmount; o++)
											{
												if (Biome[i].CreaturesCurrentPopulation[ObjectsPlaceInList] < Biome[i].CreaturesMaxPopulation[ObjectsPlaceInList])
												{								
													if (UseObjectPooling == 2){
														_InstancedObject = (GameObject)Instantiate (Biome[i].CreaturesObjectToSpawn, new Vector3(_PositionToSpawn.x, _PositionToSpawn.y + _SpawnOffset, _PositionToSpawn.z), Quaternion.Euler(_TI.terrainData.GetInterpolatedNormal(_TI.normalizedPos.x, _TI.normalizedPos.y)));
													}
													else if (UseObjectPooling == 1){
														_InstancedObject = CruxPool.Spawn(Biome[i].CreaturesObjectToSpawn, new Vector3(_PositionToSpawn.x, _PositionToSpawn.y + _SpawnOffset, _PositionToSpawn.z), Quaternion.Euler(_TI.terrainData.GetInterpolatedNormal(_TI.normalizedPos.x, _TI.normalizedPos.y)));
													}
													_InstancedObject.transform.parent = transform;
													_InstancedObject.transform.position = UnityEngine.Random.insideUnitSphere * Biome[i].CreaturesSpawnRadius[ObjectsPlaceInList] + new Vector3 (_PositionToSpawn.x, _PositionToSpawn.y + _SpawnOffset, _PositionToSpawn.z);
													_InstancedObject.gameObject.name = Biome[i].CreaturesFoldOutNames[ObjectsPlaceInList];

													//Recalculate our AI's Y position after the group spawning position has been created. 
													//If using the Air or Water AI Type, randomize the height.
													float height =_TI.terrain.SampleHeight(_InstancedObject.transform.position);

													//Spawn height
													if (Biome[i].CreaturesHeightTypeEnumNumber[ObjectsPlaceInList] == 1){
														_InstancedObject.transform.position = new Vector3(_InstancedObject.transform.position.x, height + _SpawnOffset, _InstancedObject.transform.position.z);
													}
													else if (Biome[i].CreaturesHeightTypeEnumNumber[ObjectsPlaceInList] == 2 || Biome[i].CreaturesHeightTypeEnumNumber[ObjectsPlaceInList] == 3){
														_InstancedObject.transform.position = new Vector3(_InstancedObject.transform.position.x, height + _SpawnOffset + UnityEngine.Random.Range(Biome[i].CreaturesSpawnHeightMin[ObjectsPlaceInList],Biome[i].CreaturesSpawnHeightMax[ObjectsPlaceInList]+1), _InstancedObject.transform.position.z);
													}

													//Assign the spawned AI to the current spawn node
													if (SpawnType == 1)
													{
														_InstancedObject.transform.parent = ChildrenHolder.transform;
													}

													Biome[i].CreaturesCurrentPopulation[ObjectsPlaceInList]++;
													_CurrentSpawnedObjects++;
													_SpawnedObjects.Add(_InstancedObject);
													SpawnIDList.Add(Biome[i].CreaturesSpawnId[ObjectsPlaceInList]);
												}
											}
										}
									}
								}

								//Spawn our NPC objects
								else if (Biome[i].NPCToSpawn.Count > 0 && random < generatedNPCOdds)
								{
									float GeneratedOdds = UnityEngine.Random.Range(1,101);

									if (Biome[i].NPCGenerationType == 0)
									{
										if (Biome[i].NPCToSpawn.Count > 0)
										{
											Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].NPCToSpawn.Count);
											Biome[i].NPCObjectToSpawn = Biome[i].NPCToSpawn[Biome[i].RandomObject]; 
										}
									}

									//If using the odds based generation type, pick the spawn category according to rarity of the generated odds.
									//When the rarity has beed reached, randomly pick an object within that rarity category
									if (Biome[i].NPCGenerationType == 1)
									{
										//Common - 55%
										if (GeneratedOdds >= 45)
										{
											if (Biome[i].CommonNPC.Count > 0)
											{
												Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].CommonNPC.Count);
												Biome[i].NPCObjectToSpawn = Biome[i].CommonNPC[Biome[i].RandomObject]; 
											}
										}
										//Uncommon - 30%
										else if (GeneratedOdds < 45 && GeneratedOdds >= 15)
										{
											if (Biome[i].UncommonNPC.Count > 0)
											{
												Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].UncommonNPC.Count);
												Biome[i].NPCObjectToSpawn = Biome[i].UncommonNPC[Biome[i].RandomObject]; 
											}
										}
										//Rare - 10%
										else if (GeneratedOdds < 15 && GeneratedOdds >= 5)
										{
											if (Biome[i].RareNPC.Count > 0)
											{
												Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].RareNPC.Count);
												Biome[i].NPCObjectToSpawn = Biome[i].RareNPC[Biome[i].RandomObject]; 
											}
										}
										//Ultra Rare - 5%
										else if (GeneratedOdds < 5)
										{
											if (Biome[i].UltraRareNPC.Count > 0)
											{
												Biome[i].RandomObject = UnityEngine.Random.Range(0, Biome[i].UltraRareNPC.Count);
												Biome[i].NPCObjectToSpawn = Biome[i].UltraRareNPC[Biome[i].RandomObject]; 
											}
										}
									}

									if (Biome[i].NPCToSpawn.Count > 0 && Biome[i].NPCObjectToSpawn != null)
									{
										ObjectsPlaceInList = Biome[i].NPCToSpawn.IndexOf(Biome[i].NPCObjectToSpawn);
									}

									if (Biome[i].NPCObjectToSpawn != null)
									{
										if (Biome[i].NPCGroupEnumNumber.Count > 0 && Biome[i].NPCGroupEnumNumber != null)
										{
											if (Biome[i].NPCGroupEnumNumber[ObjectsPlaceInList] == 0)
											{
												GroupAmount = UnityEngine.Random.Range(Biome[i].NPCMinGroupAmount[ObjectsPlaceInList], Biome[i].NPCMaxGroupAmount[ObjectsPlaceInList]+1);
											}
										}

										if (Biome[i].NPCObjectToSpawn != null)
										{
											if (Biome[i].NPCGroupEnumNumber[ObjectsPlaceInList] == 1)
											{
												GroupAmount = 1;
											}
										}

										if (Biome[i].NPCObjectToSpawn != null)
										{
											//Spawn Nodes
											//When using the Spawning Node setting, set all spawned AI as children to a node. This node will be set as inactive until a player is within range.
											if (SpawnType == 1 && Biome[i].NPCCurrentPopulation[ObjectsPlaceInList] < Biome[i].NPCMaxPopulation[ObjectsPlaceInList])
											{
												if (UseObjectPooling == 2){
													CurrentNode = new GameObject();
												}
												else if (UseObjectPooling == 1){
													CurrentNode = CruxPool.Spawn(SpawnNode, transform.position, Quaternion.identity);
												}
												
												CurrentNode.name = "Crux Spawn Node";
												Vector3 NodeSpawn = _PositionToSpawn;
												float height =_TI.terrain.SampleHeight(NodeSpawn);
												NodeSpawn = new Vector3(NodeSpawn.x, height + _SpawnOffset + _TI.terrain.transform.position.y, NodeSpawn.z);
												CurrentNode.transform.parent = transform;
												CurrentNode.transform.position = NodeSpawn;
												
												if (UseObjectPooling == 2){
													CurrentNode.AddComponent<SpawnNode>();
													ChildrenHolder = new GameObject();
													ChildrenHolder.name = "Object Parent";
													ChildrenHolder.transform.parent = CurrentNode.transform;
													ChildrenHolder.SetActive(false); 
												}
												else if (UseObjectPooling == 1){
													ChildrenHolder = CurrentNode.transform.GetChild (0).gameObject; 
													ChildrenHolder.SetActive(false);
												}
												
												//Gets the spawn point system and applies the needed settings
												SpawnNode SpawnNodeSystem = CurrentNode.GetComponent<SpawnNode>();
												SpawnNodeSystem.UpdateFrequency = SpawnNodeUpdateFrequency;
												SpawnNodeSystem.Initialize();
												SpawnNodeSystem.Player = _PlayerObject.gameObject;
												SpawnNodeSystem.ChildrenHolder = ChildrenHolder;
												SpawnNodeSystem.despawnDistance = (int)_DespawnRadius;
												SpawnNodeSystem.deactivateDistance = SpawnNodeDeactivateDistance;
												
												if (UseObjectPooling == 2){
													SpawnNodeSystem.UsingObjectPooling = false;
												}
												else if (UseObjectPooling == 1){
													SpawnNodeSystem.UsingObjectPooling = true;
												}
											}

											for (int o = 0; o < GroupAmount; o++)
											{
												if (Biome[i].NPCCurrentPopulation[ObjectsPlaceInList] < Biome[i].NPCMaxPopulation[ObjectsPlaceInList])
												{
													if (UseObjectPooling == 2){
														_InstancedObject = (GameObject)Instantiate (Biome[i].NPCObjectToSpawn, new Vector3(_PositionToSpawn.x, _PositionToSpawn.y + _SpawnOffset, _PositionToSpawn.z), Quaternion.Euler(_TI.terrainData.GetInterpolatedNormal(_TI.normalizedPos.x, _TI.normalizedPos.y)));
													}
													else if (UseObjectPooling == 1){
														_InstancedObject = CruxPool.Spawn(Biome[i].NPCObjectToSpawn, new Vector3(_PositionToSpawn.x, _PositionToSpawn.y + _SpawnOffset, _PositionToSpawn.z), Quaternion.Euler(_TI.terrainData.GetInterpolatedNormal(_TI.normalizedPos.x, _TI.normalizedPos.y)));
													}

													_InstancedObject.transform.parent = transform;
													_InstancedObject.transform.position = UnityEngine.Random.insideUnitSphere * Biome[i].NPCSpawnRadius[ObjectsPlaceInList] + new Vector3 (_PositionToSpawn.x, _PositionToSpawn.y + _SpawnOffset, _PositionToSpawn.z);
													_InstancedObject.gameObject.name = Biome[i].NPCFoldOutNames[ObjectsPlaceInList];

													//Recalculate our AI's Y position after the group spawning position has been created
													float height =_TI.terrain.SampleHeight(_InstancedObject.transform.position);

													//Spawn height
													if (Biome[i].NPCHeightTypeEnumNumber[ObjectsPlaceInList] == 1){
														_InstancedObject.transform.position = new Vector3(_InstancedObject.transform.position.x, height + _SpawnOffset, _InstancedObject.transform.position.z);
													}
													else if (Biome[i].NPCHeightTypeEnumNumber[ObjectsPlaceInList] == 2 || Biome[i].NPCHeightTypeEnumNumber[ObjectsPlaceInList] == 3){
														_InstancedObject.transform.position = new Vector3(_InstancedObject.transform.position.x, height + _SpawnOffset + UnityEngine.Random.Range(Biome[i].NPCSpawnHeightMin[ObjectsPlaceInList],Biome[i].NPCSpawnHeightMax[ObjectsPlaceInList]+1), _InstancedObject.transform.position.z);
													}
			
													//Assign the spawned AI to the current spawn node
													if (SpawnType == 1)
													{
														_InstancedObject.transform.parent = ChildrenHolder.transform;
													}

													Biome[i].NPCCurrentPopulation[ObjectsPlaceInList]++;
													_CurrentSpawnedObjects++;
													_SpawnedObjects.Add(_InstancedObject);
													SpawnIDList.Add(Biome[i].NPCSpawnId[ObjectsPlaceInList]);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		//This portion handles all of our despawning and population tracking.
		//This is accomplished by checking the distance every spawn interval.
		//If an object is within the despawn radius, or if it has been destroyed, 
		//remove it from the lists and reduce the population for that specific object.
		if (_SpawnedObjects.Count > 0)
		{
			foreach (GameObject G in _SpawnedObjects.ToArray())
			{
				if (G != null)
				{
					if (Vector3.Distance(G.transform.position, _PlayerObject.position) > _DespawnRadius)
					{
						GetIndex = _SpawnedObjects.IndexOf(G);
						GetSpawnID = SpawnIDList[GetIndex];

						for(int i = 0; i < Biome.Count; i++)
						{
							if (Biome[i].WildlifeCurrentPopulation.Count > 0)
							{
								if (SpawnType == 0 && Biome[i].WildlifeSpawnId.Contains(GetSpawnID)){
									Biome[i].WildlifeCurrentPopulation[Biome[i].WildlifeSpawnId.IndexOf(GetSpawnID)]--;
									SpawnIDList.RemoveAt(GetIndex);	
								}

								if (SpawnType == 1 && Biome[i].WildlifeSpawnId.Contains(GetSpawnID)){
									Biome[i].WildlifeCurrentPopulation[Biome[i].WildlifeSpawnId.IndexOf(GetSpawnID)]--;
									SpawnIDList.RemoveAt(GetIndex);	
									_CurrentSpawnedObjects--;
									_SpawnedObjects.Remove(G);
								}
							}

							if (Biome[i].CreaturesCurrentPopulation.Count > 0)
							{
								if (SpawnType == 0 && Biome[i].CreaturesSpawnId.Contains(GetSpawnID))
								{
									Biome[i].CreaturesCurrentPopulation[Biome[i].CreaturesSpawnId.IndexOf(GetSpawnID)]--;
									SpawnIDList.RemoveAt(GetIndex); 
								}

								if (SpawnType == 1 && Biome[i].CreaturesSpawnId.Contains(GetSpawnID)){
									Biome[i].CreaturesCurrentPopulation[Biome[i].CreaturesSpawnId.IndexOf(GetSpawnID)]--;
									SpawnIDList.RemoveAt(GetIndex);	
									_CurrentSpawnedObjects--;
									_SpawnedObjects.Remove(G);
								}
							}

							if (Biome[i].NPCCurrentPopulation.Count > 0)
							{
								if (SpawnType == 0 && Biome[i].NPCSpawnId.Contains(GetSpawnID))
								{
									Biome[i].NPCCurrentPopulation[Biome[i].NPCSpawnId.IndexOf(GetSpawnID)]--;
									SpawnIDList.RemoveAt(GetIndex); 
								}

								if (SpawnType == 1 && Biome[i].NPCSpawnId.Contains(GetSpawnID)){
									Biome[i].NPCCurrentPopulation[Biome[i].NPCSpawnId.IndexOf(GetSpawnID)]--;
									SpawnIDList.RemoveAt(GetIndex);	
									_CurrentSpawnedObjects--;
									_SpawnedObjects.Remove(G);
								}
							}
						}

						if (UseObjectPooling == 2){
							Destroy(G);
						}
						else if (UseObjectPooling == 1){
							if (SpawnType == 1){
								foreach (Transform T in G.transform.parent.transform){
									CruxPool.Despawn(T.gameObject);
									T.parent = ObjectPool.transform;
								}
							}

							if (SpawnType == 0){
								CruxPool.Despawn(G);
							}
						}

						if (SpawnType == 0){ 
							_SpawnedObjects.Remove(G);
							_CurrentSpawnedObjects--;
						}
					}
				}
				else if (G == null)
				{
					GetIndex = _SpawnedObjects.IndexOf(G);
					GetSpawnID = SpawnIDList[GetIndex];

					for(int i = 0; i < Biome.Count; i++)
					{
						if (Biome[i].WildlifeCurrentPopulation.Count > 0)
						{
							if (Biome[i].WildlifeSpawnId.Contains(GetSpawnID)){
								Biome[i].WildlifeCurrentPopulation[Biome[i].WildlifeSpawnId.IndexOf(GetSpawnID)]--;
								SpawnIDList.RemoveAt(GetIndex);	
							}
						}
						
						if (Biome[i].CreaturesCurrentPopulation.Count > 0)
						{
							if (Biome[i].CreaturesSpawnId.Contains(GetSpawnID))
							{
								Biome[i].CreaturesCurrentPopulation[Biome[i].CreaturesSpawnId.IndexOf(GetSpawnID)]--;
								SpawnIDList.RemoveAt(GetIndex); 
							}
						}
						
						if (Biome[i].NPCCurrentPopulation.Count > 0)
						{
							if (Biome[i].NPCSpawnId.Contains(GetSpawnID))
							{
								Biome[i].NPCCurrentPopulation[Biome[i].NPCSpawnId.IndexOf(GetSpawnID)]--;
								SpawnIDList.RemoveAt(GetIndex); 
							}
						}
					}

					_SpawnedObjects.Remove(G);
					_CurrentSpawnedObjects--;
				}
			}
		}

	}

	//This function handles the Spawn by ID feature for Crux.
	//All objects that have been created with Crux receive a Spawn ID.
	//These Spawn IDs can be used to spawn AI to the player's location for easy testing and development
	public void SpawnByID ()
	{
		EnteredSpawnID = InputText.text;

		for(int i = 0; i < Biome.Count; i++)
		{
			if (Biome[i].WildlifeSpawnId.Contains(EnteredSpawnID)) 
			{
				int IDIndex = Biome[i].WildlifeSpawnId.IndexOf(EnteredSpawnID);
				Instantiate (Biome[i].WildlifeToSpawn[IDIndex], _PlayerObject.transform.position + UnityEngine.Random.onUnitSphere * 10,  Quaternion.Euler(0,UnityEngine.Random.Range(0,360),0)); 
			}
		}

		for(int i = 0; i < Biome.Count; i++)
		{
			if (Biome[i].CreaturesSpawnId.Contains(EnteredSpawnID))
			{
				int IDIndex = Biome[i].CreaturesSpawnId.IndexOf(EnteredSpawnID);
				Instantiate (Biome[i].CreaturesToSpawn[IDIndex], _PlayerObject.transform.position + UnityEngine.Random.onUnitSphere * 10,  Quaternion.Euler(0,UnityEngine.Random.Range(0,360),0)); 
			}
		}

		for(int i = 0; i < Biome.Count; i++)
		{
			if (Biome[i].NPCSpawnId.Contains(EnteredSpawnID))
			{
				int IDIndex = Biome[i].NPCSpawnId.IndexOf(EnteredSpawnID);
				Instantiate (Biome[i].NPCToSpawn[IDIndex], _PlayerObject.transform.position + UnityEngine.Random.insideUnitSphere * 10,  Quaternion.Euler(0,UnityEngine.Random.Range(0,360),0)); 
			}
		}
	}
}