// using UnityEngine;
// using System.Collections.Generic;
// using System.Collections;
// using StarterAssets; // important, pour accéder au ThirdPersonController

// public class Respawn : MonoBehaviour
// {
//     [SerializeField] private string triggerTag = "Water";
//     [SerializeField] private int framesAvantRespawn = 90; // combien on retourne en arriere
//     [SerializeField] private float offsetY = 0.5f; // decalge pour pas retomber dans leau
//     [SerializeField] private float freezeDuration = 0.2f; // freeze du playerController apres respawn

//     private List<Vector3> positions = new List<Vector3>(); // stockage positions du player
//     private ThirdPersonController controller;
//     private StarterAssetsInputs inputs;

//     void Awake()
//     {
//         controller = GetComponent<ThirdPersonController>();
//         inputs = GetComponent<StarterAssetsInputs>();
//     }

//     void Update()
//     {
//         positions.Add(transform.position);

//         if (positions.Count > 300) // si plus de 300 frames stockées on en supprime
//         {
//             positions.RemoveAt(0);
//         }
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         if (!other.CompareTag(triggerTag) || positions.Count <= framesAvantRespawn)
//         {
//             return;
//         }

//         int i = positions.Count - framesAvantRespawn - 1;
//         Vector3 respawnPos = positions[i] + Vector3.up * offsetY;

//         StartCoroutine(RespawnRoutine(respawnPos));
//     }

//     private IEnumerator RespawnRoutine(Vector3 respawnPos)
//     {
//         // desactiver le controleur et les inputs
//         controller.enabled = false;
//         inputs.move = Vector2.zero;
//         inputs.look = Vector2.zero;
//         inputs.jump = false;
//         inputs.sprint = false;

//         transform.position = respawnPos;// deplacer le joueur
        
//         yield return new WaitForSeconds(freezeDuration); // petite pause pour stabiliser le controleur
        
//         controller.enabled = true; // reactiver le controleur

//     }
// }


using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using StarterAssets; // important, pour accéder au FirstPersonController

public class Respawn : MonoBehaviour
{
    [SerializeField] private string triggerTag = "Water";
    [SerializeField] private int framesAvantRespawn = 90; // retour arrière
    [SerializeField] private float offsetY = 0.5f; // éviter de retomber dans l'eau
    [SerializeField] private float freezeDuration = 0.2f; // blocage du controller après respawn

    private List<Vector3> positions = new List<Vector3>(); // stockage positions du player
    private FirstPersonController controller;   // <--- changé ici
    private StarterAssetsInputs inputs;

    void Awake()
    {
        controller = GetComponent<FirstPersonController>(); // <--- changé ici
        inputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        positions.Add(transform.position);

        if (positions.Count > 300)
        {
            positions.RemoveAt(0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(triggerTag) || positions.Count <= framesAvantRespawn)
        {
            return;
        }

        int i = positions.Count - framesAvantRespawn - 1;
        Vector3 respawnPos = positions[i] + Vector3.up * offsetY;

        StartCoroutine(RespawnRoutine(respawnPos));
    }

    private IEnumerator RespawnRoutine(Vector3 respawnPos)
    {
        // désactiver controller + inputs
        controller.enabled = false;
        inputs.move = Vector2.zero;
        inputs.look = Vector2.zero;
        inputs.jump = false;
        inputs.sprint = false;

        // replacer le joueur
        transform.position = respawnPos;

        // freeze court
        yield return new WaitForSeconds(freezeDuration);

        // réactiver le controller
        controller.enabled = true;
    }
}
