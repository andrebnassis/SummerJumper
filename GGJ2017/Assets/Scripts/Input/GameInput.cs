using UnityEngine;

namespace Assets.Scripts.Input
{
    public class GameInput : IGameInput
    {
        public bool HoldJump()
        {
            return UnityEngine.Input.GetButton("Jump") || TouchDown();
        }

        public bool ReleaseJump()
        {
            return UnityEngine.Input.GetButtonUp("Jump") || TouchRelease();
        }

        private bool TouchRelease()
        {
            bool b = false;
            for (int i = 0; i < UnityEngine.Input.touches.Length; i++)
            {
                b = UnityEngine.Input.touches[i].phase == TouchPhase.Ended;
                if (b)
                    break;
            }

            return b;
        }

        private bool TouchDown()
        {
            return UnityEngine.Input.touchCount > 0;
        }
    }
}
