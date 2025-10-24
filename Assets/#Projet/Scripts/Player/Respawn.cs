using UnityEngine;
using System.Collections.Generic;

public class respawn : MonoBehaviour
{
    [SerializeField] private string triggerTag = "Water";
    [SerializeField] private int framesAvantRespawn = 90;
    [SerializeField] private float offsetY = 0.2f; //decalage pour eviter de retoucher le trigger direct

    private List<Vector3> positionHistory = new List<Vector3>();
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        positionHistory.Add(transform.position); // on rajoute les positions à la list
        
        if (positionHistory.Count > 300) // limite la list à 300 frames
            positionHistory.RemoveAt(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
           
            int index = Mathf.Max(0, positionHistory.Count - framesAvantRespawn - 1); // alcule la position de x frames avant le trigger
            Vector3 respawnPos = positionHistory[index];
            respawnPos.y += offsetY; // decalage vertical pour pas retomber dans le trigger

            if (rb != null)
                rb.isKinematic = true;

            transform.position = respawnPos;// deplacement joueur

           
            if (rb != null) // reset vitesses et réactivation physique
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = false;
            }

            Debug.Log($"Respawn effectué à la position : {respawnPos}");
        }
    }
}
