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
        if (parchmentUI != null) // parchemin et croix sont cachés
            parchmentUI.SetActive(false);

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseParchment);
            closeButton.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
        if (playerIsTrigger && !parchmentCollected && Input.GetKeyDown(KeyCode.E)) // interaction avec parchemin
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
        if (parchmentUI != null) // affiche le parchemin
            parchmentUI.SetActive(true);
  
        if (closeButton != null) // affiche la croix
            closeButton.gameObject.SetActive(true);
   
        Time.timeScale = 0f;// met le jeu en pause
   
        if (playerControll != null) // desactive contrôle joueur
            playerControll.enabled = false;

        Cursor.visible = true; // affiche et libère la souris
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseParchment()
    {
        if (parchmentUI != null)
            parchmentUI.SetActive(false);// ferme le parchemin et cache la croix

        if (closeButton != null)
            closeButton.gameObject.SetActive(false);

        Time.timeScale = 1f;// reprend le jeu

        if (playerControll != null)
            playerControll.enabled = true; // reactive contrôle
        
        Cursor.visible = false;// cache la souris
        Cursor.lockState = CursorLockMode.Locked;

        parchmentCollected = true; // parchemin ramassé

        Destroy(gameObject);// supprime objet 3D de la scène
    }
}
