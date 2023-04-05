using UnityEngine;
using System.Collections;

public class TerrainInfo : MonoBehaviour 
{
	[HideInInspector]
	public int surfaceIndex = 0;
	[HideInInspector]
	public Terrain terrain;
	[HideInInspector]
	public TerrainData terrainData;
	[HideInInspector]
	public Vector3 terrainPos;
	[HideInInspector]
	public RaycastHit hit;
	[HideInInspector]
	public float height;
	[HideInInspector]
	public bool positionInvalid = false;
	[HideInInspector]
	public float terrainAngle;
	[HideInInspector]
	public Vector2 normalizedPos;

	//Attempt to get the terrain information for Crux. This is done by doing a raycast to get the current terrain.
	//If the object that is grabbed is not the terrain, the position is invalid and will be skipped this spawn interval.
	public void UpdateTerrainInfo ()
	{
		if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 100, transform.position.z), -Vector3.up, out hit)) //Was out hit
		{
			if (hit.collider.gameObject.GetComponent<Terrain>())
			{
				positionInvalid = false;
				terrain = hit.collider.gameObject.GetComponent<Terrain>();
				terrainData = terrain.terrainData;
				height = terrain.SampleHeight(transform.position);
				transform.position = new Vector3(transform.position.x, height, transform.position.z); //Added
				terrainPos = terrain.transform.position; 
				surfaceIndex = GetDominateTexture(transform.position);

				Vector3 terrainLocalPos = transform.position - terrain.transform.position;
				normalizedPos = new Vector2(terrainLocalPos.x / terrain.terrainData.size.x, terrainLocalPos.z / terrain.terrainData.size.z);
				terrainAngle = terrain.terrainData.GetSteepness(normalizedPos.x, normalizedPos.y);
			}
			else if (!hit.collider.gameObject.GetComponent<Terrain>())
			{
				positionInvalid = true;
			}
		}
		else
		{
			positionInvalid = true;
		}
	}
		
	//Get the blend of textures given the position
	private float[] GetTextureBlend(Vector3 Pos)
	{
		int posX = (int)(((Pos.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
		int posZ = (int)(((Pos.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);
		float[,,] SplatmapData = terrainData.GetAlphamaps(posX, posZ, 1, 1);
		float[] blend = new float[SplatmapData.GetUpperBound(2) + 1];

		for(int i = 0; i < blend.Length; i++)
		{
			blend[i] = SplatmapData[0, 0, i];
		}

		return blend;
	}

	//Get the most dominate texture
	private int GetDominateTexture(Vector3 Pos)
	{
		float[] textureMix = GetTextureBlend(Pos);
		int greatestIndex = 0;
		float maxTextureMix = 0;

		for(int i = 0; i < textureMix.Length; i++)
		{
			if ( textureMix[i] > maxTextureMix)
			{
				greatestIndex = i;
				maxTextureMix = textureMix[i];
			}
		}

		return greatestIndex;
	}
}
