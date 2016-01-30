using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugLineParent : MonoBehaviour {

    public Color LineColor = Color.red;

    void Start() { }
    void Update() { }

    void OnDrawGizmos()
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = LineColor;
        DebugPoint[] points = transform.GetComponentsInChildren<DebugPoint>();
        for (int i = 0; i < points.Length - 1; i++)
        {
            Gizmos.DrawLine(points[i].transform.position, points[i + 1].transform.position);
        }
        Gizmos.color = oldColor;
    }
}
