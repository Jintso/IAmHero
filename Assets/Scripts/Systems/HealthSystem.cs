using System;
using Managers;
using ScriptableObjects.Units;
using UnityEngine;

namespace Systems
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private UnitObjectData unitObjectData;
        private float _healthCurrent;
        private float _healthMax;
        private bool _isDead;

        private void Awake()
        {
            _isDead = false;
            _healthCurrent = unitObjectData.Health;
            _healthMax = unitObjectData.Health;
        }

        public event EventHandler<HealthChangedArgs> OnHealthChanged;
        public event EventHandler OnDeath;

        public float GetHealth()
        {
            return _healthCurrent;
        }

        public bool IsDead()
        {
            return _isDead;
        }

        public void TakeDamage(float damage)
        {
            _healthCurrent -= damage;

            if (_healthCurrent <= 0)
            {
                _healthCurrent = 0f;
                _isDead = true;
                OnDeath?.Invoke(this, EventArgs.Empty);
            }

            DamagePopupManager.Instance.Create(transform.position, (int)damage);

            OnHealthChanged?.Invoke(this,
                new HealthChangedArgs
                    { CurrentHealth = _healthCurrent, ChangeValue = -damage, MaxHealth = _healthMax });
        }

        public void Heal(float heal)
        {
            _healthCurrent += heal;

            if (_healthCurrent >= _healthMax) _healthCurrent = _healthMax;

            OnHealthChanged?.Invoke(this,
                new HealthChangedArgs { CurrentHealth = _healthCurrent, ChangeValue = heal, MaxHealth = _healthMax });
        }

        public class HealthChangedArgs : EventArgs
        {
            public float ChangeValue;
            public float CurrentHealth;
            public float MaxHealth;
        }
    }
}