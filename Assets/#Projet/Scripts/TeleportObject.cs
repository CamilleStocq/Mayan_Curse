using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Transform player;         // Transform du joueur
    [SerializeField] private GameObject crystalPrefab; // Prefab du cristal
    [SerializeField] private Transform portalTarget;   // Endroit où le cristal doit arriver

    [Header("Paramètres")]
    [SerializeField] private float activationDistance = 3f; // Distance max pour activer le portail
    [SerializeField] private float flySpeed = 5f;           // Vitesse du vol du cristal
    [SerializeField] private KeyCode activationKey = KeyCode.E; // Touche pour activer

    private bool isFlying = false;      // Indique si le cristal est en vol
    private GameObject spawnedCrystal;  // L’instance du cristal en vol

    void Awake()
    {
        Debug.Log("[TeleportObject] Awake appelé");

        if (player == null) Debug.LogError("[TeleportObject] Player non assigné !");
        if (crystalPrefab == null) Debug.LogError("[TeleportObject] CrystalPrefab non assigné !");
        if (portalTarget == null) Debug.LogError("[TeleportObject] PortalTarget non assigné !");
    }

    void OnEnable()
    {
        Debug.Log("[TeleportObject] Script activé (OnEnable)");
    }

    void Update()
    {
        if (player == null || portalTarget == null) return;

        // 1️⃣ Calcul de la distance entre joueur et portail
        float distance = Vector3.Distance(player.position, transform.position);
        bool isPlayerNearPortal = distance <= activationDistance;

        // 2️⃣ Debug de la distance
        Debug.Log($"[TeleportObject] Distance joueur → portail : {distance:F2} / ActivationDistance : {activationDistance}");

        // 3️⃣ Message si proche
        if (isPlayerNearPortal)
            Debug.Log("[TeleportObject] Le joueur est proche du portail");

        // 4️⃣ Détection appui sur la touche d’activation
        if (isPlayerNearPortal && Input.GetKeyDown(activationKey))
        {
            Debug.Log("[TeleportObject] Touche E pressée");

            if (Inventaire.instance == null)
            {
                Debug.LogWarning("[TeleportObject] Inventaire.instance est null !");
                return;
            }

            // 5️⃣ Vérification du cristal dans l’inventaire
            if (Inventaire.instance.HasCrystal())
            {
                Debug.Log("[TeleportObject] Cristal détecté dans l'inventaire !");
                StartCrystalFlight();
            }
            else
            {
                Debug.Log("[TeleportObject] Aucun cristal dans l'inventaire !");
            }
        }

        // 6️⃣ Déplacer le cristal en vol vers la cible
        if (isFlying && spawnedCrystal != null)
        {
            spawnedCrystal.transform.position = Vector3.MoveTowards(
                spawnedCrystal.transform.position,
                portalTarget.position,
                flySpeed * Time.deltaTime
            );

            if (Vector3.Distance(spawnedCrystal.transform.position, portalTarget.position) < 0.05f)
            {
                isFlying = false;
                Debug.Log("[TeleportObject] Cristal placé dans le portail !");
                // Optionnel : détruire ou désactiver le cristal une fois arrivé
                // Destroy(spawnedCrystal);
            }
        }
    }

    // Méthode pour lancer le vol du cristal
    void StartCrystalFlight()
    {
        if (isFlying) return;

        if (crystalPrefab == null || player == null)
        {
            Debug.LogError("[TeleportObject] Impossible de lancer le vol: refs manquantes");
            return;
        }

        Debug.Log("[TeleportObject] Le cristal s'envole vers le portail !");
        isFlying = true;

        spawnedCrystal = Instantiate(crystalPrefab, player.position + Vector3.up * 1.2f, Quaternion.identity);
        Inventaire.instance.RemoveItem(); // Supprime le cristal de l’inventaire
    }

    // Affiche une sphère dans l’éditeur pour visualiser la zone d’activation
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}
