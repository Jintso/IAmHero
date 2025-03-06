using Systems;

namespace Animation
{
    using System;
    using Units;
    using UnityEngine;

    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int TriggerAttack = Animator.StringToHash("TriggerAttack");
        private static readonly int TriggerDamaged = Animator.StringToHash("TriggerDamaged");
        private static readonly int TriggerDeath = Animator.StringToHash("TriggerDeath");
        private static readonly int TriggerOther = Animator.StringToHash("TriggerOther");
        private static readonly int IsDebuffed = Animator.StringToHash("IsDebuffed");
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        [SerializeField] private Animator animator;

        private void Start()
        {
            PlayerUnit.Instance.OnAttack += UnitOnAttack;
            PlayerUnit.Instance.OnDeath += UnitOnDeath;
            PlayerUnit.Instance.OnHealthChanged += UnitOnHealthChanged;
        }

        private void Update()
        {
            animator.SetBool(IsWalking, PlayerUnit.Instance.IsCurrentlyWalking());
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
            animator.SetBool(IsDead, true);
        }

        private void UnitOnAttack(object sender, EventArgs e)
        {
            animator.SetTrigger(TriggerAttack);
        }
    }
}