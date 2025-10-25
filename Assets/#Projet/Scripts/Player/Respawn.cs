using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using StarterAssets; // important, pour accéder au ThirdPersonController

public class Respawn : MonoBehaviour
{
    [SerializeField] private string triggerTag = "Water";
    [SerializeField] private int framesAvantRespawn = 90;
    [SerializeField] private float offsetY = 0.5f;
    [SerializeField] private float freezeDuration = 0.2f;

    private List<Vector3> positions = new List<Vector3>();
    private ThirdPersonController controller;
    private StarterAssetsInputs inputs;

    void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
        inputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        positions.Add(transform.position);
        if (positions.Count > 300)
            positions.RemoveAt(0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(triggerTag) || positions.Count <= framesAvantRespawn) return;

        int i = positions.Count - framesAvantRespawn - 1;
        Vector3 respawnPos = positions[i] + Vector3.up * offsetY;

        Debug.Log($"[Respawn Triggered] Vers {respawnPos}");
        StartCoroutine(RespawnRoutine(respawnPos));
    }

    private IEnumerator RespawnRoutine(Vector3 respawnPos)
    {
        // 1️⃣ Désactiver le contrôleur et les inputs
        controller.enabled = false;
        inputs.move = Vector2.zero;
        inputs.look = Vector2.zero;
        inputs.jump = false;
        inputs.sprint = false;

        // 2️⃣ Déplacer le joueur
        transform.position = respawnPos;
        Debug.Log($"[Respawn] Position appliquée : {transform.position}");

        // 3️⃣ Petite pause pour stabiliser le contrôleur
        yield return new WaitForSeconds(freezeDuration);

        // 4️⃣ Réactiver le contrôleur
        controller.enabled = true;

        Debug.Log("[Respawn] Contrôleur réactivé ✅");
    }
}
