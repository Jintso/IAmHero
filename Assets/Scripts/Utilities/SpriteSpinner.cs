namespace Utilities
{
    using UnityEngine;

    public class SpriteSpinner : MonoBehaviour
    {
        private const float SpinnerSpeed = 200f;

        private void LateUpdate()
        {
            transform.Rotate(Vector3.up, SpinnerSpeed * Time.deltaTime);
        }
    }
}