using UnityEngine;
using TMPro;

public class Grabbable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private string crystalNumber;

    private bool CloseToTheObject = false;

    private void Start()
    {
        if (interactText != null)
            {
                interactText.gameObject.SetActive(false);
            }
    }

    private void Update()
    {
        if (CloseToTheObject && Input.GetKeyDown(KeyCode.Q))
        {
            if (Inventaire.instance != null)
            {
                Inventaire.instance.GrabCrystal(crystalNumber);
            }

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseToTheObject = true;

            if (interactText != null) 
            {
                interactText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseToTheObject = false;
            
            if (interactText != null) 
            {
                interactText.gameObject.SetActive(false);
            }
        }
    }
}
