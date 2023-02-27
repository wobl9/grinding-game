using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private LevelUpMenu levelUpMenu;
        [SerializeField] private Player player;

        private bool isGamePaused { get => _isGamePaused; set { _isGamePaused = value; } }
        private bool _isGamePaused;

        private void Awake()
        {
            player.levelSystem.OnLevelUp += OnLevelUp;
            levelUpMenu.OnButtonClicked += OnLevelUpSpellChosen;
        }

        public void LoadMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Main menu");
        }

        public void HideMenu(GameObject menu)
        {
            menu.SetActive(false);
            Time.timeScale = 1f;
            isGamePaused = false;
        }

        public void ShowMenu(GameObject menu)
        {
            menu.SetActive(true);
            Time.timeScale = 0f;
            isGamePaused = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGamePaused)
                {
                    HideMenu(pauseMenu);
                }
                else
                {
                    ShowMenu(pauseMenu);
                }
            }
        }

        private void OnLevelUp(object Sender, System.EventArgs args)
        {
            ShowMenu(levelUpMenu.gameObject);
            levelUpMenu.Init();
        }

        private void OnLevelUpSpellChosen(object Sender, string spellId)
        {
            HideMenu(levelUpMenu.gameObject);
        }
    }
}
