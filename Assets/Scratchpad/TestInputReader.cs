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

            DesiredHeadingHelper ddh = gameObject.GetComponent<DesiredHeadingHelper>();
            ddh.DesiredHeading = (_mvec_ls + _mvec_dp).normalized;
        }
    }
}
