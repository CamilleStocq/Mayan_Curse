using UnityEngine;
using TMPro;

public class OpenChest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactText;
    //[SerializeField]private Animator anim;

    private Animator currentChestAnim;
    private bool CloseToTheObject = false;
    //private GameObject canBeOpen;
    

    void Start()
    {
        if (interactText != null)
            interactText.gameObject.SetActive(false); // on affiche pas le E tout de suite
    }

    void Update()
    {

        if (CloseToTheObject && currentChestAnim != null) // si assez proche de l'objet et qu'il peut etre attrapable
        {
            if (Input.GetKeyDown(KeyCode.E)) // si on appuie sur la touche E
            {
                //bool currentState = anim.GetBool("IsOpen");// récupère l'état actuel
                //anim.SetBool("IsOpen", !currentState);// inverse l'état
                bool isOpen = currentChestAnim.GetBool("IsOpen");
                currentChestAnim.SetBool("IsOpen", !isOpen);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Openable"))
        {
            //canBeOpen = other.gameObject;
            //CloseToTheObject = true;
            if (other.CompareTag("Openable"))
        {
            currentChestAnim = other.GetComponent<Animator>();
            if (currentChestAnim != null)
            {
                CloseToTheObject = true;
                    if (interactText != null)
                    {
                        interactText.gameObject.SetActive(true);
                    }
            }
        }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Openable"))
        {
            CloseToTheObject = false;
            currentChestAnim = null;

            if (interactText != null)
            {
                interactText.gameObject.SetActive(false);
            }
        }
    }
}
