using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject soundSettingsUI;
    [SerializeField] private GameObject infoMenuUI;

    void Start()
    {
        pauseMenuUI.SetActive(false); // cache le menu au début
        soundSettingsUI.SetActive(false);
        infoMenuUI.SetActive(false);

        GameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // à changer par click bouton ???
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OpenSoundSettings()
    {
        pauseMenuUI.SetActive(false); // cache le menu pause
        soundSettingsUI.SetActive(true); // montre les options sonores
    }

    public void OpenInfoMenu()
    {
        pauseMenuUI.SetActive(false);
        infoMenuUI.SetActive(true);
    }

    public void CloseSoundSettings()
    {
        soundSettingsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void CloseInfoMenu()
    {
        infoMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    
    public void QuitGame()
    {
        // Debug.Log("game quit");
        Application.Quit();
    }

    public void SwitchSceneMenu()
    {
        // Debug.Log("Switch to menu");
        SceneManager.LoadScene("Start");
    }
}