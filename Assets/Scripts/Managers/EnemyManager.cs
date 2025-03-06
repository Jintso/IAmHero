namespace Managers
{
    using System.Collections.Generic;
    using ScriptableObjects.Units;
    using Units;
    using Unity.Mathematics;
    using UnityEngine;

    public class EnemyManager : MonoBehaviour
    {
        private const int SpawnAmount = 10;
        private const float SpawnRadius = 10f;
        private const float SpawnRate = 10f;

        [SerializeField] private List<UnitObjectData> enemyUnitObjectList;
        private GameObject _currentEnemyType;

        private float _spawnTimer;
        public static EnemyManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;

            _spawnTimer = SpawnRate;

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            var enemyUnitObjectData = enemyUnitObjectList.Find(enemy => enemy.Level == 1);
            _currentEnemyType = enemyUnitObjectData.EnemyPrefab;
        }

        private void Update()
        {
            if (WorldManager.Instance.IsGamePlayState() == false)
            {
                return;
            }

            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer <= 0 == false)
            {
                return;
            }

            var playerPosition = PlayerUnit.Instance.transform.position;
            SpawnEnemies(_currentEnemyType, playerPosition);

            _spawnTimer = SpawnRate;
        }

        private void SpawnEnemies(GameObject enemyType, Vector3 spawnPositionCenter)
        {
            const float slice = 2 * math.PI / SpawnAmount;
            for (var i = 0; i < SpawnAmount; i++)
            {
                var angle = slice * i;
                var spawnPosition = new Vector3(
                    spawnPositionCenter.x + SpawnRadius * math.cos(angle),
                    0f,
                    spawnPositionCenter.z + SpawnRadius * math.sin(angle));

                Instantiate(enemyType, spawnPosition, quaternion.identity, transform);
            }
        }

        private GameObject GetCurrentEnemyType()
        {
            return _currentEnemyType;
        }

        public void SetCurrentEnemyType(int gameTimeInSeconds)
        {
            const int minuteInSeconds = 60;
            var enemyLevelBasedOnTime = gameTimeInSeconds / minuteInSeconds;

            var enemyUnitObjectData = enemyUnitObjectList.Find(enemy => enemy.Level == enemyLevelBasedOnTime);

            if (enemyUnitObjectData == null)
            {
                return;
            }

            _currentEnemyType = enemyUnitObjectData.EnemyPrefab;
        }
    }
}