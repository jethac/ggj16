using UnityEngine;

namespace jethac
{

    [RequireComponent(typeof(DesiredHeadingHelper))]
    public class TestController : MonoBehaviour
    {
        const float _invPI = 1.0f / Mathf.PI;
        const float _minSpeed = 0.05f;
        public float RotationSpeed = 3.0f;

        public float Speed = 10.0f;

        void Start() { }

        void LateUpdate() {
            
            DesiredHeadingHelper ddh = gameObject.GetComponent<DesiredHeadingHelper>();

            if (ddh.bUseHeading)
            {
                // calculate rotation speed scale.
                float RotationSpeedScale = Mathf.Abs(((ddh.Dot * _invPI) - 0.5f) * 2.0f) * RotationSpeed;
            
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(ddh.DesiredHeading),
                    Time.time * RotationSpeedScale
                );

                // we have a heading, so we must have an acceleration.
                Vector3 translation =
                    Vector3.forward * ddh.DesiredAcceleration * Speed * Time.deltaTime;
                transform.Translate(translation, Space.Self);

            }

        }

    }
}
