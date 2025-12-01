using UnityEngine;
using System.Collections;

public class RotateWheels : MonoBehaviour
{
    [SerializeField] private Transform wheel;
    [SerializeField] private Transform[] bridges;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private MonoBehaviour playerMovementScript; 

    private bool playerInTrigger = false;
    private bool isRotating = false;
    private float rotationStep = 90f; // nombre de degre rotation
    private float rotationDuration = 5f; // vitesse de rotation

    void Update()
    {
        bool playerIsMoving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f;

        if (playerInTrigger && !isRotating && !playerIsMoving)  
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) // tourner vers la gauche
            {
                StartCoroutine(RotateWheelAndBridges(rotationStep, rotationDuration));
                interactUI.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) // tourner vers la droite
            {
                StartCoroutine(RotateWheelAndBridges(-rotationStep, rotationDuration));
                interactUI.SetActive(false);
            }
        }
    }

    private IEnumerator RotateWheelAndBridges(float angle, float duration)
    {
        isRotating = true;

        if (playerMovementScript != null) // desactive le mouvement du joueur
        {
            playerMovementScript.enabled = false;
        }

        
        player.SetParent(bridges[0]); //fait du joueur et de la roue des enfants du ponton
        wheel.SetParent(bridges[0]);

        Quaternion wheelStartRot = wheel.rotation;
        Quaternion wheelEndRot = wheelStartRot * Quaternion.Euler(0, 0, angle);

        Quaternion[] bridgesStartRot = new Quaternion[bridges.Length];
        Quaternion[] bridgesEndRot = new Quaternion[bridges.Length];

        for (int i = 0; i < bridges.Length; i++)
        {
            bridgesStartRot[i] = bridges[i].rotation;
            bridgesEndRot[i] = bridgesStartRot[i] * Quaternion.Euler(0, 0, angle);
        }

        float startedTime = 0f;

        while (startedTime < duration)
        {
            startedTime += Time.deltaTime;
            float t = startedTime / duration;

            wheel.rotation = Quaternion.Slerp(wheelStartRot, wheelEndRot, t);

            for (int i = 0; i < bridges.Length; i++)
            {
                bridges[i].rotation = Quaternion.Slerp(bridgesStartRot[i], bridgesEndRot[i], t);
            }

            yield return null;
        }

        wheel.rotation = wheelEndRot;

        for (int i = 0; i < bridges.Length; i++)
        {
            bridges[i].rotation = bridgesEndRot[i];
        }

        
        if (playerMovementScript != null) // reactive le mouvement du joueur
        {
            playerMovementScript.enabled = true;
        }

        player.SetParent(null); // deparent tout à la fin
        wheel.SetParent(null);

        isRotating = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            interactUI.SetActive(false);
        }
    }
}
