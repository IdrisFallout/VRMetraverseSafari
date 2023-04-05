using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle = 45f;

    public WheelCollider wheelFR;
    public WheelCollider wheelFL;
    public WheelCollider wheelBR;
    public WheelCollider wheelBL;
    public float maxMotorTorque = 80f;
    public float maxBreakingTorque = 150f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;
    public bool isBraking = false;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backRightTransform;
    [SerializeField] Transform backLeftTransform;

    private List<Transform> nodes;
    private int currentNode = 0;

    [Header("Sensors")]
    public float sensorLength = 3f;
    public float frontSensorPosition = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        /*Sensors();*/
        ApplySteer();
        Drive();
        checkWaypointDistance();
        Braking();
    }

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos.z += frontSensorPosition;

        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawLine(sensorStartPos, hit.point);
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        relativeVector /= relativeVector.magnitude;
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        print(newSteer);
        /*magnitude is the distance of the vector*/
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if (currentSpeed < maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0f;
            wheelFR.motorTorque = 0f;
        }


        UpdateWheel(wheelFR, frontRightTransform);
        UpdateWheel(wheelFL, frontLeftTransform);
        UpdateWheel(wheelBR, backRightTransform);
        UpdateWheel(wheelBL, backLeftTransform);
    }

    private void checkWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 1f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }

    private void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;
    }

    public void Braking()
    {
        if (isBraking)
        {
            wheelBL.brakeTorque = maxBreakingTorque;
            wheelBR.brakeTorque = maxBreakingTorque;
        }
        else
        {
            wheelBL.brakeTorque = 0f;
            wheelBR.brakeTorque = 0f;
        }
    }
}