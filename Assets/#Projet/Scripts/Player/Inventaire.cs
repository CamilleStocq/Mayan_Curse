using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    public static Inventaire instance; // singleton

    [SerializeField] private Image crystal1;
    [SerializeField] private Image crystal2;
    [SerializeField] private Image crystal3;
    [SerializeField] private Image crystal4;

    private void Awake()
    {
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
                Debug.Log("Crystal number inconnue : " + crystalNumber);
                break;
        }
    }
}
