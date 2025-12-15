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
        if (Inventaire.instance == null)
        {
            return;
        }

        foreach (var crystal in crystals)
        {
            bool collected = Inventaire.instance.IsCrystalCollected(crystal.crystalName);

            if (collected && !crystal.spawned && crystal.crystalPrefab != null && crystal.spawnPoint != null)
            {
                GameObject obj = Instantiate(crystal.crystalPrefab, crystal.spawnPoint.position, Quaternion.identity);
                obj.SetActive(true);
                crystal.spawned = true;
            }
        }

        if (AllCrystalsPlaced())
        {
            OpenDoor door = FindObjectOfType<OpenDoor>();
            if (door != null)
            {
                door.OpenDoorNow();
            }
        }
    }

    public bool AllCrystalsPlaced()
    {
        foreach (var crystal in crystals)
        {
            if (!crystal.spawned) 
            {
                return false;
            }
        }
        return true;
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
