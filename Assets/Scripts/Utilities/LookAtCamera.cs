using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool isInverted = false;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (isInverted)
        { 
            transform.forward = -_mainCamera.transform.forward;
        }
        else
        {
            transform.forward = _mainCamera.transform.forward;
        }
    }
}
