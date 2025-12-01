using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    [System.Serializable]
    public class Crystal
    {
        public string crystalName;
        public GameObject crystalPrefab;
        public Transform spawnPoint;
        [HideInInspector] public bool spawned = false; 

    }

    public Crystal[] crystals; 
    private bool playerInTrigger = false;

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            SpawnCrystals();
        }
    }

    private void SpawnCrystals()
    {
        foreach (var crystal in crystals)
        {
            if (!crystal.spawned && crystal.crystalPrefab != null && crystal.spawnPoint != null)
            {
                GameObject obj = Instantiate(crystal.crystalPrefab, crystal.spawnPoint.position, Quaternion.identity);
                obj.SetActive(true); // s'assure qu'il est visible
                crystal.spawned = true;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
