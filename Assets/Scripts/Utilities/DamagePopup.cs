namespace Utilities
{
    using DG.Tweening;
    using TMPro;
    using UnityEngine;

    public class DamagePopup : MonoBehaviour
    {
        private const float LifeTime = 1f;
        private const float LifeTimeHalf = LifeTime * 0.5f;
        private const float FloatDistance = 0.3f;

        [SerializeField] private TextMeshProUGUI damageNumberText;

        public void Initialize(int damage)
        {
            damageNumberText.text = damage.ToString();
            transform.DOMoveY(transform.position.y + FloatDistance, LifeTimeHalf);
            damageNumberText.DOFade(0f, LifeTimeHalf).SetDelay(LifeTimeHalf);

            Destroy(gameObject, LifeTime);
        }
    }
}