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
    private FirstPersonController controller;
    private StarterAssetsInputs inputs;

    void Awake()
    {
        controller = GetComponent<FirstPersonController>();
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
