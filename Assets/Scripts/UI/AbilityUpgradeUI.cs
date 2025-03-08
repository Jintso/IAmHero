namespace UI
{
    using System;
    using Managers;
    using ScriptableObjects.Abilities;
    using UnityEngine;

    public class AbilityUpgradeUI : MonoBehaviour
    {
        [SerializeField] private GameObject abilitySlotContainer;
        [SerializeField] private GameObject abilitySlotPrefab;

        private void Start()
        {
            AbilityUpgradeManager.Instance.OnAbilityUpgrade += AbilityManagerOnAbilityUpgrade;
            gameObject.SetActive(false);
        }

        private void OnAbilitySelectedCallback(AbilityObjectData abilityObjectData)
        {
            // Give ability to Player
            Debug.Log($"Ability Selected [{abilityObjectData.Name}]");

            Hide();

            Time.timeScale = 1f;

            DestroyAbilitySlots();
        }

        private void DestroyAbilitySlots()
        {
            foreach (Transform child in abilitySlotContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void AbilityManagerOnAbilityUpgrade(object sender, EventArgs e)
        {
            foreach (var abilityObjectData in AbilityUpgradeManager.Instance.GetAvailableAbilities())
            {
                var abilitySlot = Instantiate(abilitySlotPrefab, abilitySlotContainer.transform);
                abilitySlot.GetComponent<AbilitySlotUI>().Setup(abilityObjectData, OnAbilitySelectedCallback);
            }

            Time.timeScale = 0f;

            Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}