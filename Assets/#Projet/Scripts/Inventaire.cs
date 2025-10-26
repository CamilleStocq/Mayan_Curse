using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    public static Inventaire instance; 

    [SerializeField] private Image itemSlot;
    private Sprite currentItemSprite;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        itemSlot.enabled = false;
    }

    public void AddItem(Sprite itemIcon)
    {
        currentItemSprite = itemIcon;
        itemSlot.sprite = itemIcon;
        itemSlot.enabled = true;
    }

    public void RemoveItem()
    {
        currentItemSprite = null;
        itemSlot.sprite = null;
        itemSlot.enabled = false;
    }

    public bool HasCrystal()
    {
        return currentItemSprite != null && itemSlot.enabled;
    }
}