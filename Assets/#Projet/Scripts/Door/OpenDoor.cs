using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
    public Animator doorAnimator;      // Animator de la porte
    public string triggerName = "OpenTrigger"; // Nom du trigger dans l'Animator
    public float delay = 2f;           // Délai en secondes

    private bool isOpening = false;    // Pour éviter de lancer plusieurs fois

    void Update()
    {
        if ( !isOpening && Input.GetKeyDown(KeyCode.O))
        {
            isOpening = true;
            StartCoroutine(OpenDoorWithDelay());
        }
    }

    private IEnumerator OpenDoorWithDelay()
    {
        yield return new WaitForSeconds(delay);
        doorAnimator.SetTrigger(triggerName);
        Debug.Log(" Porte ouverte !");
    }
}