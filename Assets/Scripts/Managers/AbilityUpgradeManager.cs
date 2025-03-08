namespace Managers
{
    using System;
    using System.Collections.Generic;
    using ScriptableObjects.Abilities;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class AbilityUpgradeManager : MonoBehaviour
    {
        [SerializeField] private AbilityObjectDataList abilityObjectDataList;
        private List<AbilityObjectData> _availableAbilities;

        public static AbilityUpgradeManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;

            _availableAbilities = new List<AbilityObjectData>();

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LevelingManager.Instance.OnLevelUp += LevelingManagerOnLevelUp;
        }

        public event EventHandler OnAbilityUpgrade;

        private void LevelingManagerOnLevelUp(object sender, LevelingManager.LevelArgs e)
        {
            _availableAbilities.Clear();

            _availableAbilities.Add(
                abilityObjectDataList.abilityObjectDataList[
                    Random.Range(0, abilityObjectDataList.abilityObjectDataList.Count)]);

            OnAbilityUpgrade?.Invoke(this, EventArgs.Empty);
        }

        public List<AbilityObjectData> GetAvailableAbilities()
        {
            return _availableAbilities;
        }
    }
}