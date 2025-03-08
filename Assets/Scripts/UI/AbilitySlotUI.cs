namespace UI
{
    using System;
    using ScriptableObjects.Abilities;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class AbilitySlotUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI abilityName;
        [SerializeField] private Button selectButton;

        private void OnDestroy()
        {
            selectButton.onClick.RemoveAllListeners();
        }

        public void Setup(AbilityObjectData abilityObjectData, Action<AbilityObjectData> selectAction)
        {
            icon.sprite = abilityObjectData.Sprite;
            abilityName.text = abilityObjectData.Name;
            selectButton.onClick.AddListener(() => selectAction(abilityObjectData));
        }
    }
}