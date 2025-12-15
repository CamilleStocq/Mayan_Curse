using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;      
    [SerializeField] private string triggerName = "OpenTrigger"; 
    [SerializeField] private float delay = 2f;           

    private bool isOpening = false;    

    public void OpenDoorNow()
    {
        if (!isOpening)
        {
            isOpening = true;
            StartCoroutine(OpenDoorWithDelay());
        }
    }

    private IEnumerator OpenDoorWithDelay()
    {
        yield return new WaitForSeconds(delay);
        doorAnimator.SetTrigger(triggerName);
        Debug.Log("Porte ouverte automatiquement !");
    }
}