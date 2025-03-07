namespace Utilities
{
    using UnityEngine;

    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private bool isInverted;
        [SerializeField] private bool isYLocked;

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            var cameraForward = _mainCamera.transform.forward;
            if (isYLocked)
            {
                cameraForward.y = 0;
            }

            if (isInverted)
            {
                transform.forward = -cameraForward;
            }
            else
            {
                transform.forward = cameraForward;
            }
        }
    }
}