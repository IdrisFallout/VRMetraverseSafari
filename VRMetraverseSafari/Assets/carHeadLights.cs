using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carHeadLights : MonoBehaviour
{
    public GameObject rightSpotLight;
    public GameObject leftSpotLight;
    // Start is called before the first frame update
    public void TurnOffHeadLights()
    {
        rightSpotLight.SetActive(false);
        leftSpotLight.SetActive(false);
    }    
    public void TurnOnFullHeadLights()
    {
        rightSpotLight.SetActive(true);
        leftSpotLight.SetActive(true);
    }
}
