namespace UI
{
    using System;
    using Managers;
    using TMPro;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private Button mainMenuButton;

        private void Awake()
        {
            gameOverText.gameObject.SetActive(false);
            mainMenuButton.gameObject.SetActive(false);

            mainMenuButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
        }

        private void Start()
        {
            WorldManager.Instance.OnGameOver += InstanceOnGameOver;
        }

        private void InstanceOnGameOver(object sender, EventArgs e)
        {
            gameOverText.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
        }
    }
}