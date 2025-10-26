using UnityEngine;
using TMPro;

public class Grabbable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactText; 
    [SerializeField] private Sprite itemIcon;
    
    private GameObject canBeGrab; 
    private GameObject heldObject; 
    private bool CloseToTheObject = false; 
    private bool isHidden = false; 
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        if (interactText != null)
            interactText.gameObject.SetActive(false); 
    }

    void Update()
    {
        if (CloseToTheObject && canBeGrab != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Q pressé");

                if (heldObject == null)
                {
                    GrabbableObject();
                }
                else
                {
                    ReleaseObject();
                }
            }
        }
    }

    public void GrabbableObject()
    {
        heldObject = canBeGrab;
        heldObject.SetActive(false); 
        isHidden = true;

        if (Inventaire.instance != null && itemIcon != null)
        {
            Inventaire.instance.AddItem(itemIcon);
        }

        if (interactText != null)
            interactText.gameObject.SetActive(false);
    }

    public void ReleaseObject()
    {
        if (heldObject == null) return;

        heldObject.SetActive(true);
        heldObject.transform.position = initialPosition;
        heldObject.transform.rotation = initialRotation;
        isHidden = false;

        if (Inventaire.instance != null)
        {
            Inventaire.instance.RemoveItem();
        }

        heldObject = null;

        if (interactText != null)
            interactText.text = "Q";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            canBeGrab = other.gameObject;
            CloseToTheObject = true;

            if (interactText != null)
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
                interactText.gameObject.SetActive(false);
        }
    }
}
