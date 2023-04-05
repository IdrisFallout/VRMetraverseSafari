using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPos : MonoBehaviour
{
public GameObject terrain;

private TerrainCollider _terrainCollider;
    // Start is called before the first frame update
    void Start()
    {
        _terrainCollider = terrain.GetComponent<TerrainCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        float terrainHeight = Terrain.activeTerrain.SampleHeight(transform.forward);
        Debug.Log(terrainHeight);
        var ray = new Ray (transform.position, Vector3.down); // check for slopes

        if (_terrainCollider.Raycast(ray, out RaycastHit hit, 1000)) {
            transform.rotation =Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.forward, hit.normal)*transform.rotation, Time.deltaTime*3); // adjust for slopes
            
            }
    }
}
