using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carModifier : MonoBehaviour
{
    internal enum type
    {
        player,
        AI,
    }
    [SerializeField] private type playerType;

    [Header("Wheels")]
    [Range(0.2f, 0.7f)] public float wheelRadius = 0.36f;
    [Range(0.05f, 0.2f)] public float suspensionDistance = 0.1f;
    [Range(0f, 0.1f)] public float suspensionOffset = 0.03f;
    [Range(0.4f, 1f)] public float sidewaysFriction;
    [Range(0.5f, 1f)] public float forwardFriction;

    /*private carController controller;*/
    private GameObject wheelsFolder;
    private GameObject[] wheels;
    [HideInInspector] public WheelCollider[] colliders;

    private Vector3 wheelPosition;
    private Quaternion wheelRotation;
}
