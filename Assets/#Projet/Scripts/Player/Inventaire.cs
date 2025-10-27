// using UnityEngine;
// using UnityEngine.UI;

// public class Inventaire : MonoBehaviour
// {
//     public static Inventaire instance; 

//     [SerializeField] private Image itemSlot;
//     private Sprite currentItemSprite;

//     private void Awake()
//     {
//         if (instance == null)
//             instance = this;
//         else
//             Destroy(gameObject);

//         itemSlot.enabled = false;
//     }

//     public void AddItem(Sprite itemIcon)
//     {
//         currentItemSprite = itemIcon;
//         itemSlot.sprite = itemIcon;
//         itemSlot.enabled = true;
//     }

//     public void RemoveItem()
//     {
//         currentItemSprite = null;
//         itemSlot.sprite = null;
//         itemSlot.enabled = false;
//     }

//     public bool HasCrystal()
//     {
//         return currentItemSprite != null && itemSlot.enabled;
//     }
// }

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventaire : MonoBehaviour
{
    public static Inventaire instance;

    [SerializeField] private List<Image> itemSlots; 

    private List<Sprite> collectedCrystals = new List<Sprite>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        foreach (var slot in itemSlots)
            slot.enabled = false;
    }

    public void AddItem(Sprite itemIcon)
    {
        if (collectedCrystals.Count >= itemSlots.Count)
        {
            Debug.Log("[Inventaire] Inventaire plein !");
            return;
        }

        collectedCrystals.Add(itemIcon);
        int index = collectedCrystals.Count - 1;

        itemSlots[index].sprite = itemIcon;
        itemSlots[index].enabled = true;

        Debug.Log($"[Inventaire] Cristal ajouté dans le slot {index + 1}");
    }

    public void UseCrystal()
    {
        if (collectedCrystals.Count == 0) return;

        collectedCrystals.RemoveAt(0);

        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (i < collectedCrystals.Count)
            {
                itemSlots[i].sprite = collectedCrystals[i];
                itemSlots[i].enabled = true;
            }
            else
            {
                itemSlots[i].sprite = null;
                itemSlots[i].enabled = false;
            }
        }
    }

    public bool HasCrystal()
    {
        return collectedCrystals.Count > 0;
    }
}
