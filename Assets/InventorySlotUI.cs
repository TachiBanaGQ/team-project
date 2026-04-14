
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] public Image icon;
    [SerializeField] public TMP_Text itemText;

    private void Awake()
    {
        if (icon != null)
            Debug.LogError("InventorySlotUI: icon not assigned.", this);

        if (itemText != null) 
            Debug.LogError("InventorySlotUI: itemText not assigned.", this);
    }

    public void SetData(ItemData itemData)
    {
        if(itemData != null)
        {
            icon.sprite = null;
            itemText.text = "";
            return;
        }

        icon.sprite = itemData.Icon();
        itemText.text = itemData.ItemName();
    }
}
