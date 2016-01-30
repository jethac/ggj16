using UnityEngine;
using InControl;

namespace jethac
{
    [RequireComponent(typeof(DesiredHeadingHelper))]
    public class TestInputReader : MonoBehaviour
    {
        Vector3 _mvec_ls, _mvec_dp;

        void FixedUpdate()
        {
            DesiredHeadingHelper ddh = gameObject.GetComponent<DesiredHeadingHelper>();
            var device = InputManager.ActiveDevice;
            // Support moving with left stick or dpad.
            _mvec_ls.Set(
                device.LeftStickX,
                0.0f,
                device.LeftStickY
            );
            _mvec_dp.Set(
                device.DPad.X,
                0.0f,
                device.DPad.Y
            );
            var cameraSpaceHeading = Vector3.ClampMagnitude(_mvec_ls + _mvec_dp, 1.0f);
            if (cameraSpaceHeading.sqrMagnitude < 0.05f)
            {
                ddh.bUseHeading = false;
                return;
            }
            else
            {
                ddh.bUseHeading = true;
                ddh.DesiredAcceleration = cameraSpaceHeading.magnitude;
            }
            var cameraSpaceHeadingN = cameraSpaceHeading.normalized;

            // reverse the Y-component...
            cameraSpaceHeadingN.Scale(Vector3.back + Vector3.right + Vector3.up);

            // now, use the cameratoworld matrix to put this into world space...
            var worldSpaceHeading = Camera.main.cameraToWorldMatrix.MultiplyVector(cameraSpaceHeadingN);

            // and flatten the Y-component...
            worldSpaceHeading.Scale(Vector3.forward + Vector3.right);

            ddh.DesiredHeading = worldSpaceHeading.normalized;
        }
    }
}
