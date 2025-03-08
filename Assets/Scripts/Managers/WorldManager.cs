namespace Managers
{
    using System;
    using UnityEngine;

    public class WorldManager : MonoBehaviour
    {
        private const float LevelIncreaseTimeInterval = 60f;

        private State _gameState;
        private float _gameTimer;
        private State _lastGameState;
        private float _levelIncreaseTime;

        public static WorldManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;

            _lastGameState = State.None;
            _gameState = State.MainMenu;

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _gameTimer = 0f;
            _levelIncreaseTime = LevelIncreaseTimeInterval;
        }

        private void Update()
        {
            if (_gameState != _lastGameState)
            {
                BroadcastStateSwitch();
            }

            switch (_gameState)
            {
                case State.MainMenu:
                    HandleMainMenu();

                    break;
                case State.Gameplay:
                    HandleGameplay();

                    break;
                case State.GameOver:
                    HandleGameOver();

                    return;
                case State.None:
                    break;
                default:
                    Debug.LogError("Unknown game state!");
                    break;
            }
        }

        private void BroadcastStateSwitch()
        {
            switch (_gameState)
            {
                case State.MainMenu:
                    OnMainMenu?.Invoke(this, EventArgs.Empty);
                    break;
                case State.Gameplay:
                    OnGameplay?.Invoke(this, EventArgs.Empty);
                    break;
                case State.GameOver:
                    OnGameOver?.Invoke(this, EventArgs.Empty);
                    break;
                case State.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _lastGameState = _gameState;
        }

        public bool IsGamePlayState()
        {
            return _gameState == State.Gameplay;
        }

        public void StartGame()
        {
            _gameTimer = 0f;
            _levelIncreaseTime = LevelIncreaseTimeInterval;
            _gameState = State.Gameplay;
        }

        private void HandleMainMenu()
        {
        }

        private void HandleGameplay()
        {
            _gameTimer += Time.deltaTime;

            if (_levelIncreaseTime <= _gameTimer)
            {
                EnemyManager.Instance.SetCurrentEnemyType((int)_gameTimer);
                _levelIncreaseTime = _gameTimer + LevelIncreaseTimeInterval;
            }
        }

        private static void HandleGameOver()
        {
            if (EnemyManager.Instance == false)
            {
                return;
            }

            EnemyManager.Instance.enabled = false;
        }

        public float GetGameTime()
        {
            return _gameTimer;
        }

        public event EventHandler OnMainMenu;
        public event EventHandler OnGameplay;
        public event EventHandler OnGameOver;

        public void PlayerIsDead()
        {
            _gameState = State.GameOver;
        }

        private enum State
        {
            MainMenu,
            Gameplay,
            GameOver,
            None
        }
    }
}