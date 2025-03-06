namespace Systems
{
    using System;
    using Managers;
    using UnityEngine;

    public class LevelSystem : MonoBehaviour
    {
        private const int RequiredExperienceMultiplier = 10;
        private int _currentExperience;
        private int _currentLevel;
        private int _experienceToNextLevel;

        private void Awake()
        {
            _currentLevel = 1;
            _currentExperience = 0;
            _experienceToNextLevel = _currentLevel * RequiredExperienceMultiplier;
        }

        public event EventHandler<LevelingManager.LevelArgs> OnLevelUp;
        public event EventHandler<LevelingManager.LevelArgs> OnExperienceGained;

        public void GiveExperience(int amount)
        {
            _currentExperience += amount;

            OnExperienceGained?.Invoke(
                this,
                new LevelingManager.LevelArgs
                {
                    CurrentLevel = _currentLevel,
                    CurrentExperience = _currentExperience,
                    ExperienceToNextLevel = _experienceToNextLevel,
                });

            if (_currentExperience < _currentLevel * RequiredExperienceMultiplier)
            {
                return;
            }

            _currentLevel += amount / RequiredExperienceMultiplier;
            _experienceToNextLevel = _currentLevel * RequiredExperienceMultiplier;
            _currentExperience = 0;

            var levelArgs = new LevelingManager.LevelArgs
            {
                CurrentLevel = _currentLevel,
                CurrentExperience = _currentExperience,
                ExperienceToNextLevel = _experienceToNextLevel,
            };

            OnLevelUp?.Invoke(this, levelArgs);
            OnExperienceGained?.Invoke(this, levelArgs);
        }
    }
}