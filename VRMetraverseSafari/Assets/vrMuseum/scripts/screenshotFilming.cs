using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshotFilming : MonoBehaviour {

    [SerializeField] Camera tsCamera;
    [SerializeField] string fileName;
    int imgnum;
    IEnumerator Snapshot()
    {
        yield return new WaitForSeconds(3.05f);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = tsCamera.targetTexture;
        tsCamera.Render();
        Texture2D image = new Texture2D(tsCamera.targetTexture.width, tsCamera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, tsCamera.targetTexture.width, tsCamera.targetTexture.height), 0, 0);
        image.Apply();
        string name = Application.persistentDataPath + "/" + fileName + Random.Range(0, 1000).ToString() +".png";
        System.IO.File.WriteAllBytes(name, image.EncodeToPNG());
        Debug.Log("Saved " + name);
        RenderTexture.active = currentRT;
    }

    // Use this for initialization
    void Start()
    {

        StartCoroutine( Snapshot());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
