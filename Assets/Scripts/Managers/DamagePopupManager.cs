namespace Managers
{
    using UnityEngine;
    using Utilities;

    public class DamagePopupManager : MonoBehaviour
    {
        [SerializeField] private GameObject damagePopupPrefab;

        public static DamagePopupManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void Create(Vector3 position, int damage)
        {
            var damageNumberPopup = Instantiate(damagePopupPrefab, position, Quaternion.identity, transform);
            var damagePopup = damageNumberPopup.GetComponent<DamagePopup>();
            damagePopup.Initialize(damage);
        }
    }
}