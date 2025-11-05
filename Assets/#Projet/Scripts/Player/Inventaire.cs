// using UnityEngine;
// using UnityEngine.UI;

// public class Inventaire : MonoBehaviour
// {
//     [SerializeField] private Image crystalPasRamasse;
//     public static Inventaire instance; // singleton 
//     private Sprite currentItemSprite = null; // pas d'objet ramassé pour le moment

//     private void Awake()
//     {
//         Debug.Log(" Awake exécuté !");

//         if (instance == null)
//         {
//             instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         // else
//         // {
//         //     Destroy(gameObject);
//         //     return;
//         // }

//         if (crystalPasRamasse != null)
//         {
//             crystalPasRamasse.enabled = true;
//         }
//     }

//     public void AddItem(Sprite itemIcon) // quand le joueur ramasse le crystal
//     {
//         if (Inventaire.instance == null)
//         {
//             Debug.Log("inventaire.instance");
//         }

//         if (crystalPasRamasse == null)
//         {
//             Debug.Log("crystal pas ramasseé");
//         }


//         Debug.Log("Objet ajouté");

//         currentItemSprite = itemIcon; 

//         if (crystalPasRamasse != null)
//         {
//             crystalPasRamasse.enabled = false; // disparait visuellement de l'ui
//         }
//     }

//     public void RemoveItem()
//     {
//         currentItemSprite = null;
//         if (crystalPasRamasse != null)
//         {
//             crystalPasRamasse.enabled = true; // on peut la remettre si nécessaire
//         }
//     }

//     public bool HasCrystal()
//     {
//         return currentItemSprite != null;
//     }
// }


using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    public static Inventaire instance;

    [Header("UI Crystals")]
    [SerializeField] private Image crystal1;
    [SerializeField] private Image crystal2;
    [SerializeField] private Image crystal3;
    [SerializeField] private Image crystal4;

    private void Awake()
    {
        // Singleton sécurisé
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (crystal1 != null) crystal1.enabled = true;
        if (crystal2 != null) crystal2.enabled = true;
        if (crystal3 != null) crystal3.enabled = true;
        if (crystal4 != null) crystal4.enabled = true;
    }

    public void GrabCrystal(string crystalNumber)
    {
        switch (crystalNumber)
        {
            case "Crystal1":
                if (crystal1 != null) crystal1.enabled = false;
                break;
            case "Crystal2":
                if (crystal2 != null) crystal2.enabled = false;
                break;
            case "Crystal3":
                if (crystal3 != null) crystal3.enabled = false;
                break;
            case "Crystal4":
                if (crystal4 != null) crystal4.enabled = false;
                break;
            
            default:
                Debug.LogWarning("Crystal number inconnue : " + crystalNumber);
                break;
        }
    }
}
