namespace Units
{
    using System;
    using Managers;
    using RotaryHeart.Lib.PhysicsExtension;
    using Systems;
    using UnityEngine;
    using Physics = UnityEngine.Physics;

    public class PlayerUnit : UnitBase
    {
        public const string TagName = "Player";

        [SerializeField] private bool hasAutoAttack;

        private LevelSystem _levelSystem;

        public static PlayerUnit Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Player is already instantiated!");
            }

            Instance = this;
        }

        private void Start()
        {
            HealthSystem = GetComponent<HealthSystem>();
            HealthSystem.OnDeath += HealthSystemOnDeath;
            HealthSystem.OnHealthChanged += HealthSystemOnHealthChanged;

            _levelSystem = GetComponent<LevelSystem>();
            _levelSystem.OnLevelUp += LevelSystemOnLevelUp;
            _levelSystem.OnExperienceGained += LevelSystemOnExperienceGained;

            GameInputManager.Instance.OnAttack += GameInputOnAttack;
            WorldManager.Instance.OnGameOver += (_, _) => { IsDead = true; };
        }

        private void Update()
        {
            if (IsDead)
            {
                WorldManager.Instance.PlayerIsDead();
                return;
            }

            HandleMovement();

            if (hasAutoAttack)
            {
                //HandleAttack(UnitHealth); // Should be enemy unitHealth
            }

            AttackTimer -= Time.deltaTime;
        }

        protected override void HealthSystemOnHealthChanged(object sender, HealthSystem.HealthChangedArgs e)
        {
            if (e.ChangeValue > 0)
            {
                VfxManager.RunVfx(VfxManager.Instance.HealingVfx, transform);
            }

            base.HealthSystemOnHealthChanged(sender, e);
        }

        private static void LevelSystemOnExperienceGained(object sender, LevelingManager.LevelArgs e)
        {
            LevelingManager.Instance.PlayerExperienceGained(sender, e);
        }

        private void LevelSystemOnLevelUp(object sender, LevelingManager.LevelArgs e)
        {
            VfxManager.RunVfx(VfxManager.Instance.LevelUpVfx, transform);
            LevelingManager.Instance.PlayerLevelUp(sender, e);
        }

        private void GameInputOnAttack(object sender, EventArgs e)
        {
            if (IsDead || hasAutoAttack || CanAttack() == false)
            {
                return;
            }

            const float attackDistance = 1f;
            //var attackDirection = MouseWorldPosition.GetPosition() - transform.position;
            //var attackOffset = attackDirection.normalized * attackDistance;
            //var attackCenter = transform.position + attackOffset;
            var attackCenter = transform.position;
            //DebugExtensions.DebugCircle(attackCenter, Vector3.up, Color.red, attackDistance, 5f);
            var hits = Physics.OverlapSphere(attackCenter, attackDistance, LayerMask.GetMask(EnemiesLayerName));

            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent<HealthSystem>(out var unitHealth))
                {
                    HandleAttack(unitHealth);
                }
            }

            HandleAttack();
            VfxManager.RunVfx(VfxManager.Instance.SwordTrailVfx, transform);
        }

        private void HandleMovement()
        {
            var inputVector = GameInputManager.Instance.GetMovementVectorNormalized();
            const float cameraTiltAmount = 20f;
            var moveYOrthographicCorrection = inputVector.y / Math.Cos(cameraTiltAmount);
            var moveDirection = new Vector3(inputVector.x, 0, (float)moveYOrthographicCorrection);
            transform.position += moveDirection * (unitObjectData.MoveSpeed * Time.deltaTime);

            IsWalking = moveDirection != Vector3.zero;

            DirectionChangeCheck(moveDirection);
        }
    }
}