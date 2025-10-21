using UnityEngine;
using System.Collections;

public class RotationWheelLR : MonoBehaviour
{
    [SerializeField] private Transform wheel;
    [SerializeField] private Transform ponton;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject interactUI;  // UI avec flèches ← / →
    [SerializeField] private float rotationAngle = 90f;
    [SerializeField] private float rotationDuration = 3f;

    private bool playerInTrigger = false;
    private bool isRotating = false;

    void Update()
    {
        if (playerInTrigger && !isRotating)
        {
            interactUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(RotatePonton(-rotationAngle));
                interactUI.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(RotatePonton(rotationAngle));
                interactUI.SetActive(false);
            }
        }
    }

    IEnumerator RotatePonton(float angle)
    {
        isRotating = true;

        // Parent le joueur au ponton
        player.SetParent(ponton, true); // conserve la position locale du joueur

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // bloque la physique pendant la rotation
        }

        Quaternion startRotPonton = ponton.rotation;
        Quaternion endRotPonton = startRotPonton * Quaternion.Euler(0, 0, angle);

        Quaternion startRotWheel = wheel.rotation;
        Quaternion endRotWheel = startRotWheel * Quaternion.Euler(0, 0, angle);

        float time = 0f;

        while (time < rotationDuration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / rotationDuration);

            ponton.rotation = Quaternion.Slerp(startRotPonton, endRotPonton, t);
            wheel.rotation = Quaternion.Slerp(startRotWheel, endRotWheel, t);

            yield return null;
        }

        // fin de rotation
        ponton.rotation = endRotPonton;
        wheel.rotation = endRotWheel;

        
        player.SetParent(null, true); // déparent le joueur après la rotation

        if (rb != null)
        {
            rb.isKinematic = false;
        }

        isRotating = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            interactUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            interactUI.SetActive(false);
        }
    }
}
