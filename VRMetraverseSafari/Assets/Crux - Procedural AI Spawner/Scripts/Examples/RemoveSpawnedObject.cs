using UnityEngine;
using System.Collections;

//An example script that removes this object from Crux. 
//When an AI is killed, Crux will automtically remove it
//after it has reached the despawn radius. This is useful 
//for AI that are not destroyed when killed 
//and you'd like the kill to be immediately detected by Crux.
//To use, attach thi script to your AI object and call the 
//RemoveObject from your AI script when your AI's health reaches 0.
public class RemoveSpawnedObject : MonoBehaviour 
{
	public void RemoveObject () 
	{
		Crux CruxSystem = FindObjectOfType(typeof(Crux)) as Crux;
		int index = CruxSystem._SpawnedObjects.IndexOf(this.gameObject);
		CruxSystem._SpawnedObjects[index] = null;
	}
}
