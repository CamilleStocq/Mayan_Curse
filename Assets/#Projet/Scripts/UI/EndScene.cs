using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField] private string endSceneName = "End";

    void Start()
    {
        if (SceneManager.GetActiveScene().name == endSceneName)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1f; // S'assure que le jeu n'est pas en pause
        }
    }
}
