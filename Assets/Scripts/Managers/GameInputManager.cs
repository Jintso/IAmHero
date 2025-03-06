namespace Managers
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class GameInputManager : MonoBehaviour
    {
        private InputSystemActions _inputActions;
        public static GameInputManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            _inputActions = new InputSystemActions();
            _inputActions.Player.Enable();

            _inputActions.Player.Attack.performed += AttackOnPerformed;

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public event EventHandler OnAttack;

        public Vector2 GetMovementVectorNormalized()
        {
            return _inputActions.Player.Move.ReadValue<Vector2>();
        }

        private void AttackOnPerformed(InputAction.CallbackContext obj)
        {
            OnAttack?.Invoke(this, EventArgs.Empty);
        }
    }
}