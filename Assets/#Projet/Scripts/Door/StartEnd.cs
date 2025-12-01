using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEnd : MonoBehaviour
{
    
    [SerializeField] private string NomScene;

    public void GoToEnd()
    {
        SceneManager.LoadScene(NomScene);
    }

    public void OnTriggerEnter(Collider other)
    {
        GoToEnd();
    }
}
