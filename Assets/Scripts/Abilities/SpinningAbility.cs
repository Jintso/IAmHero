using Units;
using UnityEngine;

namespace Abilities
{
    public class SpinningAbility : BaseAbility
    {
        private const float Offset = 1f;

        private void Start()
        {
            transform.parent = PlayerUnit.Instance.transform;
            transform.position = new Vector3(transform.position.x + Offset, transform.position.y, transform.position.z);
        }

        private void Update()
        {
            transform.RotateAround(transform.parent.position, Vector3.up, abilityObjectData.Speed * Time.deltaTime);
        }
    }
}