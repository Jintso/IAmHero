using DG.Tweening;
using Units;
using UnityEngine;

namespace Abilities
{
    public class SpinningAbility : BaseAbility
    {
        private const float Offset = 1f;

        private void Start()
        {
            transform.position = PlayerUnit.Instance.transform.position;
            transform.DOMove(PlayerUnit.Instance.transform.position + Offset * Vector3.right, .25f)
                .SetEase(Ease.OutBounce);
            transform.parent = PlayerUnit.Instance.transform;
            // transform.position = new Vector3(transform.position.x + Offset, transform.position.y, transform.position.z);
            // transform.DOMove(transform.position + Offset * Vector3.right, .25f).SetEase(Ease.OutBounce);
        }

        private void Update()
        {
            transform.RotateAround(transform.parent.position, Vector3.up, abilityObjectData.Speed * Time.deltaTime);
        }
    }
}