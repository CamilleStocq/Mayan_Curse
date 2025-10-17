using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Grabbable : MonoBehaviour
{

    [SerializeField] private GameObject PlayerHand;
    [SerializeField] private TextMeshProUGUI interactText; // Texte UI qui dit "Appuyez sur A"
    private GameObject canBeGrab;
    private GameObject heldObject;
    private bool CloseToTheObject = false;
    

    void Start()
    {
        if (interactText != null)
            interactText.gameObject.SetActive(false); // on affiche pas le A tout de suite
    }

    void Update()
    {
        if (CloseToTheObject && canBeGrab != null) // si assez proche de l'objet et qu'il peut etre attrapable
        {
            if (Input.GetKeyDown(KeyCode.Q)) // si on appuie sur la touche A
            {
                Debug.Log("A est préssée");

                if (heldObject == null)
                {
                    GrabbableObject(); // on attrape
                }
                else
                {
                    ReleaseObject(); // on relache
                }
            }
        }
    }

    public void GrabbableObject()
    {
        heldObject = canBeGrab;
        canBeGrab.transform.SetParent(PlayerHand.transform); // attache objet à la main du player
        canBeGrab.transform.localPosition = Vector3.zero;

        if (interactText != null)
        {
            interactText.gameObject.SetActive(false); // désactive le A quand on a l'bjet en main
        }
    }

    public void ReleaseObject()
    {
        heldObject.transform.SetParent(null); // detache l'objet de la main

        heldObject = null;

        if (interactText != null)
        {
            interactText.text = "A";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            canBeGrab = other.gameObject;
            CloseToTheObject = true;
        }

        if (interactText != null) // apparition du A
        {
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            canBeGrab = null;
            CloseToTheObject = false;

            if (interactText != null)
            {
                interactText.gameObject.SetActive(false);
            }
        }
    }
}  
    