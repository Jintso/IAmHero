namespace ScriptableObjects.Loot
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Scriptable Objects/Loot Object Data")]
    public class LootObjectData : ScriptableObject
    {
        public float DropChance;
        public int ExperienceAmount;
        public string ItemName;
        public int MinEnemyDropLevel;
        public GameObject Prefab;
        public Sprite Sprite;
        public int HealthAmount;
    }
}