using UnityEngine;

public class DebugPoint : MonoBehaviour {

    public Color MyColor = Color.white;

    void Start() { }
    void Update() { }

    void OnDrawGizmos()
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = MyColor;
        Gizmos.DrawSphere(transform.position, 0.05f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right);

        Gizmos.color = oldColor;
    }

}
