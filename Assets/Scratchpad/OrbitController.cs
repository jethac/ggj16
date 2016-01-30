using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class OrbitController : MonoBehaviour
{

    public enum eOrientationMode {
        TowardsOrbitPoint = 0,
        AlongTangent
    }
    public enum eWrapMode {
        Once,
        Loop
    }

    public GameObject OrbitPoint;
    public float Duration = 10;
    public eOrientationMode OrientationMode = eOrientationMode.TowardsOrbitPoint;
    public bool AutoStart = true;

    Vector3 initialPosition = Vector3.zero;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if (OrbitPoint != null)
        {
            // calculate radius
            Vector3 orbit = OrbitPoint.transform.position;
            orbit.y = transform.position.y;
            Vector3 initial2D = (transform.position - orbit);
            Vector3 initial2Dn = initial2D.normalized;
            radius = initial2D.magnitude;

            // calculate initial angle
            Vector3 cross = Vector3.Cross(Vector3.forward, initial2Dn);
            float f = Mathf.Acos(Vector3.Dot(Vector3.forward, initial2Dn)) * Mathf.Sign(Vector3.Dot(cross, Vector3.up));

            // calculate the next position
            float timefactor_raw = (Time.fixedDeltaTime / Duration);
            float timefactor = timefactor_raw - Mathf.Floor(timefactor_raw);
            float rotamount = f + (timefactor * (Mathf.PI * 2.0f));
            Vector3 newPosition = new Vector3(
                Mathf.Sin(rotamount) * radius,
                transform.position.y,
                Mathf.Cos(rotamount) * radius
            );
            transform.position = newPosition;
            
            // calculate rotation
            transform.rotation = Quaternion.LookRotation(
                OrbitPoint.transform.position - transform.position
            );
            /*
#if UNITY_EDITOR
            // precalculate segments
            for (int i = 0; i < SEGMENTS; i++)
            {
                float theta = f + (((float)i / (float)SEGMENTS) * (Mathf.PI * 2.0f));
                float x = Mathf.Sin(theta) * radius;// * Mathf.Sign(initial2D.x); 
                float z = Mathf.Cos(theta) * radius;
                segmentPositions[i].Set(
                    x,
                    transform.position.y,
                    z
                );
            }
#endif
            */
        }
    }

    const int SEGMENTS = 32;
    float radius = 1.0f;
    Vector3[] segmentPositions = new Vector3[SEGMENTS];

    void OnDrawGizmos()
    {
        /*
#if UNITY_EDITOR
        if (OrbitPoint != null)
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, OrbitPoint.transform.position);

            for (int i = 0; i < SEGMENTS - 1; i++)
            {
                Gizmos.color = Color.Lerp(Color.magenta, Color.cyan, (float)i / (float)SEGMENTS);// colors[i % 4];
                Gizmos.DrawLine(segmentPositions[i], segmentPositions[i+1]);
            }
            Gizmos.color = Color.Lerp(Color.magenta, Color.cyan, (float)(SEGMENTS - 1) / (float)SEGMENTS);// colors[i % 4];
            Gizmos.DrawLine(segmentPositions[SEGMENTS - 1], segmentPositions[0]);
            Gizmos.color = oldColor;
        }
    }
#endif
        */
    }
}
