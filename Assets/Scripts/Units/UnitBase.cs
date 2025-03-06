using Systems;

namespace Units
{
    using System;
    using ScriptableObjects.Units;
    using UnityEngine;
    using UnityEngine.Events;

    public class UnitBase : MonoBehaviour
    {
        protected const string EnemiesLayerName = "Enemies";
        protected const string EnemiesDeadLayerName = "EnemiesDead";

        protected static UnityEvent<int, Vector3> OnDeathBroadcast;

        [SerializeField] protected UnitObjectData unitObjectData;

        private bool _lastMovingDirectionWasRight;
        protected float AttackTimer;

        protected HealthSystem HealthSystem;
        protected bool IsDead;
        protected bool IsWalking;

        public event EventHandler OnAttack;
        public event EventHandler OnDeath;
        public event EventHandler<HealthSystem.HealthChangedArgs> OnHealthChanged;
        public event EventHandler<OnDirectionChangeEventArgs> OnDirectionChange;

        public bool IsCurrentlyWalking()
        {
            return IsWalking;
        }

        protected virtual void HealthSystemOnHealthChanged(object sender, HealthSystem.HealthChangedArgs e)
        {
            OnHealthChanged?.Invoke(this, e);
        }

        protected void HealthSystemOnDeath(object sender, EventArgs e)
        {
            OnDeath?.Invoke(this, e);
            OnDeathBroadcast?.Invoke(unitObjectData.Level, transform.position);
            IsDead = true;
        }

        protected void HandleAttack(HealthSystem healthSystem = null)
        {
            healthSystem?.TakeDamage(unitObjectData.Damage);

            OnAttack?.Invoke(this, EventArgs.Empty);
        }

        protected bool CanAttack()
        {
            if (AttackTimer <= 0 == false)
            {
                return false;
            }

            AttackTimer = unitObjectData.AttackSpeed;
            return true;
        }

        protected void DirectionChangeCheck(Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero)
            {
                return;
            }

            var movingRight = moveDirection.x > 0;
            switch (movingRight)
            {
                case true when _lastMovingDirectionWasRight == false:
                    _lastMovingDirectionWasRight = true;
                    OnDirectionChange?.Invoke(this, new OnDirectionChangeEventArgs { IsMovingRight = true });
                    break;
                case false when _lastMovingDirectionWasRight:
                    _lastMovingDirectionWasRight = false;
                    OnDirectionChange?.Invoke(this, new OnDirectionChangeEventArgs { IsMovingRight = false });
                    break;
            }
        }

        public class OnDirectionChangeEventArgs : EventArgs
        {
            public bool IsMovingRight;
        }
    }
}