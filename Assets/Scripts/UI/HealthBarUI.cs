using Systems;

namespace UI
{
    using Units;
    using UnityEngine;
    using UnityEngine.UI;

    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private UnitBase unit;
        [SerializeField] private Image healthBarImage;

        private void Awake()
        {
            healthBarImage.fillAmount = 1f;
        }

        private void Start()
        {
            unit.OnHealthChanged += UnitOnHealthChanged;
        }

        private void UnitOnHealthChanged(object sender, HealthSystem.HealthChangedArgs e)
        {
            healthBarImage.fillAmount = e.CurrentHealth / e.MaxHealth;
        }
    }
}