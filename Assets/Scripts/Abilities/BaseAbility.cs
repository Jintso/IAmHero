using ScriptableObjects.Abilities;
using Systems;
using Units;
using UnityEngine;

namespace Abilities
{
    public class BaseAbility : MonoBehaviour
    {
        [SerializeField] protected AbilityObjectData abilityObjectData;
        [SerializeField] private GameObject visual;

        private void Awake()
        {
            var spriteRenderer = visual.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = abilityObjectData.Sprite;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PlayerUnit.TagName))
            {
                return;
            }

            if (other.TryGetComponent<HealthSystem>(out var unitHealthSystem) == false)
            {
                return;
            }

            if (unitHealthSystem.IsDead())
            {
                return;
            }

            unitHealthSystem.TakeDamage(abilityObjectData.Damage);
        }
    }
}