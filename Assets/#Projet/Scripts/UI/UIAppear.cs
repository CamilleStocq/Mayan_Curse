using UnityEngine;
using UnityEngine.UI;

public class ParchmentPickup : MonoBehaviour
{
    [SerializeField] private GameObject parchmentUI; 
    [SerializeField] private Button closeButton;
    [SerializeField] private MonoBehaviour playerControll;
    private bool playerIsTrigger = false;
    private bool parchmentCollected = false;

    void Start()
    {
        parchmentUI.SetActive(false);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseParchment);
    }

    void Update()
    {
        if (playerIsTrigger && !parchmentCollected && Input.GetKeyDown(KeyCode.E))
        {
            ShowParchment();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !parchmentCollected)
            playerIsTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerIsTrigger = false;
    }

    private void ShowParchment()
    {
        parchmentUI.SetActive(true);
        Time.timeScale = 0f; // met le jeu en pause

        
        if (playerControll != null)
            playerControll.enabled = false; // désactive le script de mouvement du joueur

        
        Cursor.visible = true; // affiche et libère la souris
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseParchment()
    {
        parchmentUI.SetActive(false);
        Time.timeScale = 1f; // reprend le jeu

        
        if (playerControll != null)
            playerControll.enabled = true; // réactive le mouvement du joueur

        // cache la souris
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        parchmentCollected = true;
        
        Destroy(gameObject); // supprime le modele 3D
    }
}
