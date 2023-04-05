using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float radius = 1;

    public Vector2 regionSize = Vector2.one;
    public int rejectionSample = 30;
    public float displayRadius = 1;

    private List<Vector2> _points;

    private void OnValidate()
    {
        _points = PoissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSample);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(regionSize/2, regionSize);
        if (_points != null)
        {
            foreach (Vector2 point in _points)
            {
                Gizmos.DrawSphere(point, displayRadius);
            }
        }
    }
}
