namespace Managers
{
    using System;
    using UnityEngine;

    public class LevelingManager : MonoBehaviour
    {
        public static LevelingManager Instance { get; private set; }

        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void PlayerExperienceGained(object sender, LevelArgs e)
        {
            OnExperienceGained?.Invoke(sender, e);
        }

        public event EventHandler<LevelArgs> OnLevelUp;
        public event EventHandler<LevelArgs> OnExperienceGained;

        public void PlayerLevelUp(object sender, LevelArgs e)
        {
            OnLevelUp?.Invoke(sender, e);
        }

        public class LevelArgs : EventArgs
        {
            public int CurrentExperience;
            public int CurrentLevel;
            public int ExperienceToNextLevel;
        }
    }
}