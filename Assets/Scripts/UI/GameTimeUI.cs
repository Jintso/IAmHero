namespace UI
{
    using System;
    using Managers;
    using TMPro;
    using UnityEngine;

    public class GameTimeUI : MonoBehaviour
    {
        private const string TimerFormat = @"mm\:ss";
        [SerializeField] private TextMeshProUGUI gameTimeText;

        private void Awake()
        {
            gameTimeText.text = TimeSpan.FromSeconds(0f).ToString(TimerFormat);
        }

        private void LateUpdate()
        {
            gameTimeText.text = TimeSpan.FromSeconds(WorldManager.Instance.GetGameTime()).ToString(TimerFormat);
        }
    }
}