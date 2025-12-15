using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    public static Inventaire instance;

    [Header("UI")]
    [SerializeField] private Image crystal1Image;
    [SerializeField] private Image crystal2Image;
    [SerializeField] private Image crystal3Image;
    [SerializeField] private Image crystal4Image;

    [Header("Grisés")]
    [SerializeField] private Sprite crystal1Grey;
    [SerializeField] private Sprite crystal2Grey;
    [SerializeField] private Sprite crystal3Grey;
    [SerializeField] private Sprite crystal4Grey;

    [Header("Ramassés")]
    [SerializeField] private Sprite crystal1Color;
    [SerializeField] private Sprite crystal2Color;
    [SerializeField] private Sprite crystal3Color;
    [SerializeField] private Sprite crystal4Color;

    private HashSet<string> collected = new HashSet<string>();

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
        if (crystal1Image != null) 
            {crystal1Image.sprite = crystal1Grey;}
        if (crystal2Image != null) 
            {crystal2Image.sprite = crystal2Grey;}
        if (crystal3Image != null) 
            {crystal3Image.sprite = crystal3Grey;}
        if (crystal4Image != null) 
            {crystal4Image.sprite = crystal4Grey;}

        collected.Clear();
    }

    public void GrabCrystal(string crystalNumber)
    {
        switch (crystalNumber)
        {
            case "Crystal1":
                if (crystal1Image != null) crystal1Image.sprite = crystal1Color;
                collected.Add("Crystal1");
                break;

            case "Crystal2":
                if (crystal2Image != null) crystal2Image.sprite = crystal2Color;
                collected.Add("Crystal2");
                break;

            case "Crystal3":
                if (crystal3Image != null) crystal3Image.sprite = crystal3Color;
                collected.Add("Crystal3");
                break;

            case "Crystal4":
                if (crystal4Image != null) crystal4Image.sprite = crystal4Color;
                collected.Add("Crystal4");
                break;

            default:
                Debug.LogWarning("[Inventaire] Crystal number inconnu : " + crystalNumber);
                break;
        }
    }

    public void PlaceCrystal(string crystalNumber)
    {
        switch (crystalNumber)
        {
            case "Crystal1":
                if (crystal1Image != null) crystal1Image.sprite = crystal1Grey;
                collected.Remove("Crystal1");
                break;

            case "Crystal2":
                if (crystal2Image != null) crystal2Image.sprite = crystal2Grey;
                collected.Remove("Crystal2");
                break;

            case "Crystal3":
                if (crystal3Image != null) crystal3Image.sprite = crystal3Grey;
                collected.Remove("Crystal3");
                break;
                
            case "Crystal4":
                if (crystal4Image != null) crystal4Image.sprite = crystal4Grey;
                collected.Remove("Crystal4");
                break;
        }
    }

    public bool IsCrystalCollected(string crystalNumber)
    {
        return collected.Contains(crystalNumber);
    }

    public bool AllCrystalsCollected()
    {
        return 
                collected.Contains("Crystal1") &&
                collected.Contains("Crystal2") &&
                collected.Contains("Crystal3") &&
                collected.Contains("Crystal4");
    }
}

