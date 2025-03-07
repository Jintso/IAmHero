namespace Abilities
{
    using Units;
    using UnityEngine;

    public class SpinningAbility : BaseAbility
    {
        private const float Offset = 1f;

        private void Start()
        {
            transform.parent = PlayerUnit.Instance.transform;
            transform.position = PlayerUnit.Instance.transform.position + Offset * Vector3.right;
        }

        private void Update()
        {
            transform.RotateAround(transform.parent.position, Vector3.up, abilityObjectData.Speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);
        }
    }
}