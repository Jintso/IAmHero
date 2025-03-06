namespace Systems
{
    using ScriptableObjects.Loot;
    using Units;
    using UnityEngine;

    public class LootSystem : MonoBehaviour
    {
        [SerializeField] private LootObjectData lootObjectData;
        [SerializeField] private GameObject visual;

        private void Awake()
        {
            var spriteRenderer = visual.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = lootObjectData.Sprite;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PlayerUnit.TagName) == false)
            {
                return;
            }

            if (other.TryGetComponent<LevelSystem>(out var levelSystem))
            {
                levelSystem.GiveExperience(lootObjectData.ExperienceAmount);
            }

            if (other.TryGetComponent<HealthSystem>(out var healthSystem))
            {
                healthSystem.Heal(lootObjectData.HealthAmount);
            }

            Destroy(gameObject);
        }
    }
}