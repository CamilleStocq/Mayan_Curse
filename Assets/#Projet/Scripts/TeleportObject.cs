// using UnityEngine;

// public class TeleportObject : MonoBehaviour
// {
//     [Header("Références")]
//     [SerializeField] private Transform player;         // Transform du joueur
//     [SerializeField] private GameObject crystalPrefab; // Prefab du cristal
//     [SerializeField] private Transform portalTarget;   // Endroit où le cristal doit arriver

//     [Header("Paramètres")]
//     [SerializeField] private float activationDistance = 3f; // Distance max pour activer le portail
//     [SerializeField] private float flySpeed = 5f;           // Vitesse du vol du cristal
//     [SerializeField] private KeyCode activationKey = KeyCode.E; // Touche pour activer

//     private bool isFlying = false;      // Indique si le cristal est en vol
//     private GameObject spawnedCrystal;  // L’instance du cristal en vol

//     void Awake()
//     {
//         Debug.Log("[TeleportObject] Awake appelé");

//         if (player == null) Debug.LogError("[TeleportObject] Player non assigné !");
//         if (crystalPrefab == null) Debug.LogError("[TeleportObject] CrystalPrefab non assigné !");
//         if (portalTarget == null) Debug.LogError("[TeleportObject] PortalTarget non assigné !");
//     }

//     void OnEnable()
//     {
//         Debug.Log("[TeleportObject] Script activé (OnEnable)");
//     }

//     void Update()
//     {
//         if (player == null || portalTarget == null) return;

//         float distance = Vector3.Distance(player.position, transform.position);
//         bool isPlayerNearPortal = distance <= activationDistance;

//         Debug.Log($"[TeleportObject] Distance joueur → portail : {distance:F2} / ActivationDistance : {activationDistance}");

//         if (isPlayerNearPortal)
//             Debug.Log("[TeleportObject] Le joueur est proche du portail");

//         if (isPlayerNearPortal && Input.GetKeyDown(activationKey))
//         {
//             Debug.Log("[TeleportObject] Touche E pressée");

//             if (Inventaire.instance == null)
//             {
//                 Debug.LogWarning("[TeleportObject] Inventaire.instance est null !");
//                 return;
//             }

//             if (Inventaire.instance.HasCrystal())
//             {
//                 Debug.Log("[TeleportObject] Cristal détecté dans l'inventaire !");
//                 StartCrystalFlight();
//             }
//             else
//             {
//                 Debug.Log("[TeleportObject] Aucun cristal dans l'inventaire !");
//             }
//         }

//         if (isFlying && spawnedCrystal != null)
//         {
//             spawnedCrystal.transform.position = Vector3.MoveTowards(
//                 spawnedCrystal.transform.position,
//                 portalTarget.position,
//                 flySpeed * Time.deltaTime
//             );

//             if (Vector3.Distance(spawnedCrystal.transform.position, portalTarget.position) < 0.05f)
//             {
//                 isFlying = false;
//                 Debug.Log("[TeleportObject] Cristal placé dans le portail !");
//                 // Optionnel : détruire ou désactiver le cristal une fois arrivé
//                 // Destroy(spawnedCrystal);
//             }
//         }
//     }

//     // Méthode pour lancer le vol du cristal
//     void StartCrystalFlight()
//     {
//         if (isFlying) return;

//         if (crystalPrefab == null || player == null)
//         {
//             Debug.LogError("[TeleportObject] Impossible de lancer le vol: refs manquantes");
//             return;
//         }

//         Debug.Log("[TeleportObject] Le cristal s'envole vers le portail !");
//         isFlying = true;

//         spawnedCrystal = Instantiate(crystalPrefab, player.position + Vector3.up * 1.2f, Quaternion.identity);
//         Inventaire.instance.RemoveItem(); // Supprime le cristal de l’inventaire
//     }

//     // Affiche une sphère dans l’éditeur pour visualiser la zone d’activation
//     void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.cyan;
//         Gizmos.DrawWireSphere(transform.position, activationDistance);
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private List<Transform> portalTargets;

    [SerializeField] private float activationDistance = 3f;
    [SerializeField] private float flySpeed = 5f;
    [SerializeField] private KeyCode activationKey = KeyCode.E;

    private int nextTargetIndex = 0;
    private bool isFlying = false;

    void Update()
    {
        if (player == null || portalTargets.Count == 0) return;

        float distance = Vector3.Distance(player.position, transform.position);
        bool nearPortal = distance <= activationDistance;

        if (nearPortal && Input.GetKeyDown(activationKey))
        {
            if (Inventaire.instance != null && Inventaire.instance.HasCrystal())
            {
                if (nextTargetIndex < portalTargets.Count)
                {
                    StartCoroutine(SendCrystalToPortal(portalTargets[nextTargetIndex]));
                    Inventaire.instance.UseCrystal();
                    nextTargetIndex++;
                }
                else
                {
                    Debug.Log("[TeleportObject] Tous les emplacements du portail sont remplis !");
                }
            }
            else
            {
                Debug.Log("[TeleportObject] Aucun cristal dans l’inventaire !");
            }
        }
    }

    private IEnumerator SendCrystalToPortal(Transform target)
    {
        isFlying = true;

        GameObject flyingCrystal = Instantiate(crystalPrefab, player.position + Vector3.up * 1.2f, Quaternion.identity);

        while (Vector3.Distance(flyingCrystal.transform.position, target.position) > 0.05f)
        {
            flyingCrystal.transform.position = Vector3.MoveTowards(
                flyingCrystal.transform.position,
                target.position,
                flySpeed * Time.deltaTime
            );
            yield return null;
        }

        flyingCrystal.transform.position = target.position;
        isFlying = false;

        Debug.Log("[TeleportObject] Cristal placé !");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}
