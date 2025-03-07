using System;
using DG.Tweening;
using Managers;
using Systems;
using UnityEngine;
using UnityEngine.Events;

namespace Units
{
    public class EnemyUnit : UnitBase
    {
        private void Start()
        {
            OnDeathBroadcast = new UnityEvent<int, Vector3>();
            OnDeathBroadcast.AddListener(LootManager.Instance.EnemyDied);

            HealthSystem = GetComponent<HealthSystem>();
            HealthSystem.OnDeath += HealthSystemOnDeath;
            HealthSystem.OnHealthChanged += HealthSystemOnHealthChanged;

            WorldManager.Instance.OnGameOver += WorldManagerOnGameOver;
        }

        private void Update()
        {
            if (IsDead)
            {
                IsWalking = false;
                gameObject.layer = LayerMask.NameToLayer(EnemiesDeadLayerName);

                transform.DOScale(0f, 0.5f).SetDelay(1f);
                Destroy(gameObject, 5f);

                return;
            }

            AttackTimer -= Time.deltaTime;

            HandleChasePlayer();
        }

        private void OnDestroy()
        {
            _ = transform.DOKill(true);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(PlayerUnit.TagName) == false || IsDead)
            {
                return;
            }

            if (other.TryGetComponent<HealthSystem>(out var unitHealthSystem) == false)
            {
                return;
            }

            if (CanAttack())
            {
                HandleAttack(unitHealthSystem);
            }
        }

        private void WorldManagerOnGameOver(object sender, EventArgs e)
        {
            IsDead = true;
        }

        private void HandleChasePlayer()
        {
            var moveDirection = PlayerUnit.Instance.transform.position - transform.position;

            const float stopDistanceSquared = 0.45f;
            if (moveDirection.magnitude < stopDistanceSquared)
            {
                moveDirection = Vector3.zero;
            }

            moveDirection.Normalize();
            transform.position += moveDirection * (unitObjectData.MoveSpeed * Time.deltaTime);

            IsWalking = moveDirection != Vector3.zero;

            DirectionChangeCheck(-moveDirection);
        }
    }
}