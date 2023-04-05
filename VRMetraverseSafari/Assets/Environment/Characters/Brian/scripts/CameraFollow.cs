using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public GameObject Brian;

    // Start is called before the first frame update
    private void Start()
    {
        Brian = GameObject.FindGameObjectWithTag("brian");
    }
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        /*transform.LookAt(target);*/
        /*transform.RotateAround(Brian.transform.position, new Vector3(0, 1, 0), 0 * Time.deltaTime);*/

    }
}
