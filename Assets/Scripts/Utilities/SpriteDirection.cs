namespace Utilities
{
    using Units;
    using UnityEngine;

    public class SpriteDirection : MonoBehaviour
    {
        [SerializeField] private UnitBase unit;

        private void Start()
        {
            unit.OnDirectionChange += OnDirectionChange;
        }

        private void OnDirectionChange(object sender, UnitBase.OnDirectionChangeEventArgs e)
        {
            transform.rotation = e.IsMovingRight ? Quaternion.Euler(-90f, -90f, -90f) : Quaternion.Euler(90f, 0f, 0f);
        }
    }
}