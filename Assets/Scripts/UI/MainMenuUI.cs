namespace UI
{
    using Managers;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            playButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(1);
                WorldManager.Instance.StartGame();
            });

            exitButton.onClick.AddListener(Application.Quit);
        }
    }
}