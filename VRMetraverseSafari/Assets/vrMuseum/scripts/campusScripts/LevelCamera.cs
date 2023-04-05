using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelCamera : MonoBehaviour {

    [SerializeField] GameObject m_Camera;


	// Update is called once per frame
    [ExecuteInEditMode]
	void Update () {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
                    m_Camera.transform.rotation * Vector3.up);
    }
}
