using UnityEngine;
using System.Collections;

public class RotateWheels : MonoBehaviour
{
    [SerializeField] private Transform wheel;
    [SerializeField] private Transform[] bridges;
    [SerializeField] private GameObject interactWithE;
    [SerializeField] private Transform player;
    [SerializeField] private MonoBehaviour playerMovementScript; 

    private bool playerInTrigger = false;
    private bool isRotating = false;
    private float rotationStep = 90f; // nombre de degre rotation
    private float rotationDuration = 5f; // vitesse de rotation

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            StartCoroutine(RotateWheelAndBridges(rotationStep, rotationDuration));
            interactWithE.SetActive(false);
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
            bridges[i].rotation = bridgesEndRot[i];

        
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
            interactWithE.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            interactWithE.SetActive(false);
        }
    }
}
