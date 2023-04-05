using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float currentTime;
    public float dayLenghtMinutes;
    private float rotationSpeed;
    public string displayTime;
    private bool enableLight = false;

    [Header("Car Headlights")]
    public GameObject rightSpotLight;
    public GameObject leftSpotLight;

    float midday;
    float translateTime;
    string AMPM = "PM";

    carHeadLights chl;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 360 / dayLenghtMinutes / 60;
        midday = dayLenghtMinutes * 60 / 2;
        TurnOffHeadLights();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        translateTime = (currentTime / (midday * 2));

        float t = translateTime * 24f;
        float hours = Mathf.Floor(t);
        float displayHours = hours;
        if(hours == 0)
        {
            displayHours = 12f;
        }
        if (hours > 12)
        {
            displayHours = hours - 12;
        }
        if(currentTime >= midday)
        {
            if(AMPM != "AM")
            {
                AMPM = "AM";
            }
        }
        if(currentTime >= midday * 2)
        {
            if (AMPM != "PM")
            {
                AMPM = "PM";
            }
            currentTime = 0;
        }
        t *= 60;
        float minutes = Mathf.Floor(t % 60);
        string displayMinutes = minutes.ToString();

        if(minutes < 10)
        {
            displayMinutes = "0"+minutes.ToString();
        }
        displayTime = displayHours.ToString() + ":" + displayMinutes + " " + AMPM;
        transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);
        handleCarHeadLights(displayHours, float.Parse(displayMinutes), AMPM);
    }

    private void handleCarHeadLights(float displayHours, float displayMinutes, string AMPM)
    {
        /*print(displayHours + ":"+ displayMinutes + AMPM);*/
        if(displayHours == 6 && displayMinutes >= 30 && AMPM == "PM")
        {
            TurnOnFullHeadLights();
        }
        if(displayHours == 7 && displayMinutes >= 45 && AMPM == "AM")
        {
            TurnOffHeadLights();
        }
    }

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
