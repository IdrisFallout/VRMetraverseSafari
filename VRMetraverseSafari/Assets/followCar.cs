using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class followCar : MonoBehaviour
{
    public GameObject car;
    public bool isDriving = true;

    // public InputActionProperty showButton;
    public Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        // if (showButton.action.WasPressedThisFrame())
        // {
        //     if (isDriving)
        //     {
        //         isDriving = false;
        //         rb.drag = 10000;
        //     }
        //     else
        //     {
        //         isDriving = true;
        //         rb.drag = 0;
        //     }
        // }
        if (isDriving)
        {
            transform.position = car.transform.position;
        }
    }
}
