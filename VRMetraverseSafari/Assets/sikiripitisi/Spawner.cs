using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private List<GameObject> animalsToSpawn = new List<GameObject>();
    public int maxNumberOfAnimalsToSpawn = 3;
    private GameObject newAnimal;
    public GameObject[] animals;
    SphereCollider sphereCollider;
    int randomNumberOfAnimalsToSpawn;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("brian"))
        {
            SpawnAnimal();
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("brian"))
        {
            DispawnAnimal();
        }
    }
    private void SpawnAnimal()
    {
        randomNumberOfAnimalsToSpawn = Random.Range(Mathf.RoundToInt(0.75f * maxNumberOfAnimalsToSpawn), maxNumberOfAnimalsToSpawn);
        for (int i = 0; i < randomNumberOfAnimalsToSpawn; i++)
        {
            int randomAnimal = Random.Range(0, animals.Length);
            float x = Random.Range(-5, 5);
            float z = Random.Range(-5, 5);
            Vector3 position = new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
            newAnimal = Instantiate(animals[randomAnimal], transform.position + position, Quaternion.Euler(0, Random.Range(-160, 160), 0));
            if (newAnimal.tag == "small-animal")
            {
                float rScale = (Random.Range(0.4f, 0.8f));
                newAnimal.transform.localScale = new Vector3(rScale, rScale, rScale);
            }
            else
            {
                float rScale = (Random.Range(0.9f, 1.1f));
                newAnimal.transform.localScale = new Vector3(rScale, rScale, rScale);
            }
            if (newAnimal) animalsToSpawn.Add(newAnimal);
        }
    }
    private void DispawnAnimal()
    {
        for (int i = 0; i < randomNumberOfAnimalsToSpawn; i++)
        {
            Destroy(animalsToSpawn[0]);
            animalsToSpawn.RemoveAt(0);
        }
        
    }
}
