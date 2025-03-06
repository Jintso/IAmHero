namespace Managers
{
    using System.Collections.Generic;
    using ScriptableObjects.Loot;
    using UnityEngine;

    public class LootManager : MonoBehaviour
    {
        public static LootManager Instance;

        [SerializeField] private List<LootObjectData> lootObjectDataList;
        [SerializeField] private List<LootObjectData> coinObjectDataList;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void EnemyDied(int enemyLevel, Vector3 position)
        {
            var lootItemList = lootObjectDataList.FindAll(obj => obj.MinEnemyDropLevel <= enemyLevel);

            var coin = enemyLevel switch
            {
                < 6 =>
                    coinObjectDataList.Find(obj => obj.ItemName == "Emerald"),
                < 11 =>
                    coinObjectDataList.Find(obj => obj.ItemName == "Sapphire"),
                >= 11 =>
                    coinObjectDataList.Find(obj => obj.ItemName == "Ruby"),
            };

            lootItemList.Add(coin);

            if (lootItemList.Count <= 0)
            {
                return;
            }

            foreach (var lootItem in lootItemList)
            {
                if (WillDropChanceCheck(lootItem.DropChance))
                {
                    Instantiate(lootItem.Prefab, position, Quaternion.identity, transform);
                }
            }
        }

        private static bool WillDropChanceCheck(float dropChance)
        {
            return Random.value > 1 - dropChance;
        }
    }
}