using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElephants : MonoBehaviour
{
    public GameObject animal;
    /*public int numOfAnimals = 20;*/
    public int animalSize = 100;

    private List<GameObject> _animals = new List<GameObject>();
    private List<Vector3> _positions = new List<Vector3>();

    private GameObject _newAnimal;

    private Vector3 _position;

    public Vector2 boundary = Vector2.one;
    public bool spawn = false;
    public bool deSpawn = false;

    private List<Vector2> _points;
    // Start is called before the first frame update
    void Start()
    {
        _position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            SpawnEs();
            spawn = false;
        }
        if (deSpawn)
        {
            Despawn();
            deSpawn = false;
        }
    }

    void SpawnEs()
    {
        _points = PoissonDiscSampling.GeneratePoints(animalSize, boundary);

/*        Debug.Log("spawn: " + _points[0].x);
*/        //int n = Random.Range(7, numOfAnimals);
        foreach (Vector2 point in _points)
        {
            Debug.Log(point);
            //newAnimal = Instantiate(animal, new Vector3((Random.Range(0,20) + _position.x), 0, (Random.Range(0,20) + _position.z) ), Quaternion.Euler(-90,30,0)) ;
            _newAnimal = Instantiate(animal, new Vector3(point.x + _position.x, 0, point.y + _position.z), Quaternion.Euler(-90, 30, 0));
            float rScale = (Random.Range(0.4f, 1.1f));
            _newAnimal.transform.localScale = new Vector3(rScale, rScale, rScale);
            if (_newAnimal) _animals.Add(_newAnimal);
            //_positions()
        }
    }
    void Despawn()
    {
        Debug.Log("Despawn");
        int lastIndex = _animals.Count;
        for (int i = 0; i < lastIndex; i++)
        {
            Destroy(_animals[i]);
            _animals.RemoveAt(i);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("brian"))
        {
            spawn = true;
            Debug.Log("region entered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("brian"))
        {
            spawn = false;
            Debug.Log("region exited");
        }
    }
}
