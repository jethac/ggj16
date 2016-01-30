using UnityEngine;

namespace jethac {

    public class DesiredHeadingHelper : MonoBehaviour {
        
        public bool DrawDebugInfo = true;

        [HideInInspector]
        public Vector3 DesiredHeading = Vector3.forward;

        float m_dot = 0.0f;
        Vector3 m_cross = Vector3.zero;

        public float Dot { get { return m_dot;  } }
        public Vector3 Cross { get { return m_cross; } }

	    void Start () {
            DesiredHeading = transform.forward;
	    }
	
        void FixedUpdate()
        {
            // cache cross and dot products of the forward vector and the
            // desired heading vector.
            m_dot = Vector3.Dot(transform.forward, DesiredHeading);
            m_cross = Vector3.Cross(transform.forward, DesiredHeading);
        }

        void OnDrawGizmos() {
            const float lineLength = 1.0f;

            Vector3 desiredLineEnd = (transform.position + DesiredHeading) * lineLength;
            Vector3 forwardLineEnd = (transform.position + transform.forward) * lineLength;

            Gizmos.color = Color.grey;
            Gizmos.DrawLine(
                transform.position,
                forwardLineEnd
            );

            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                transform.position,
                desiredLineEnd
            );
        }
    }
}