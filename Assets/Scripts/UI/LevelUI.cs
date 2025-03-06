namespace UI
{
    using Managers;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class LevelTextUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Slider experienceBar;
        [SerializeField] private TextMeshProUGUI experienceText;

        private void Awake()
        {
            levelText.text = "Level 1";
            experienceBar.value = 0;
            experienceText.text = "0 / 10";
        }

        private void Start()
        {
            LevelingManager.Instance.OnExperienceGained += LevelingManagerOnExperienceGained;
        }

        private void LevelingManagerOnExperienceGained(object sender, LevelingManager.LevelArgs e)
        {
            levelText.text = $"Level {e.CurrentLevel.ToString()}";
            experienceBar.value = (float)e.CurrentExperience / e.ExperienceToNextLevel;
            experienceText.text = $"{e.CurrentExperience} / {e.ExperienceToNextLevel}";
        }
    }
}