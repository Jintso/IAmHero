using Systems;

namespace Animation
{
    using System;
    using Units;
    using UnityEngine;

    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int IsWalking = Animator.StringToHash("Run");
        private static readonly int TriggerAttack = Animator.StringToHash("Attack");
        private static readonly int TriggerAttackTwo = Animator.StringToHash("Attack 2");
        private static readonly int TriggerAttackThree = Animator.StringToHash("Attack 3");
        private static readonly int TriggerDamaged = Animator.StringToHash("Hit");
        private static readonly int TriggerDeath = Animator.StringToHash("Death");
        private static readonly int TriggerAbility = Animator.StringToHash("Ability");

        [SerializeField] private Animator animator;
        private UnitBase _unit;

        private void Start()
        {
            _unit = GetComponentInParent<UnitBase>();
            _unit.OnAttack += UnitOnAttack;
            _unit.OnDeath += UnitOnDeath;
            _unit.OnHealthChanged += UnitOnHealthChanged;
        }

        private void Update()
        {
            animator.SetBool(IsWalking, _unit.IsCurrentlyWalking());
        }

        private void UnitOnHealthChanged(object sender, HealthSystem.HealthChangedArgs e)
        {
            if (e.ChangeValue < 0)
            {
                animator.SetTrigger(TriggerDamaged);
            }
        }

        private void UnitOnDeath(object sender, EventArgs e)
        {
            animator.SetTrigger(TriggerDeath);
        }

        private void UnitOnAttack(object sender, EventArgs e)
        {
            animator.SetTrigger(TriggerAttack);
        }
    }
}