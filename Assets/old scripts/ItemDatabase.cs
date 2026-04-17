using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    [SerializeField] private ItemData Potion;
    [SerializeField] private ItemData Sword;
    [SerializeField] private ItemData Shield;


   private Dictionary <ItemType,ItemData> m_items;

    public static ItemDatabase Instance { get; private set; }

    private void Awake()
    {
       if(Instance != null)
        {
            if (Instance != this)
            {
                Debug.LogError("multiple instances of database found! destroying the ");
                Destroy(gameObject);
                return;
            }
        }
       Instance = this;

        BuildDatabase();
    }
   
    private void BuildDatabase()
    {
        m_items = new Dictionary<ItemType, ItemData>
        {
            {  ItemType.Potion , Potion},
            {  ItemType.Sword , Sword},
            { ItemType.Shield , Shield }

        };
    }

    public bool TryGetItemData(ItemType type, out ItemData itemData)
    {
        return m_items.TryGetValue(type, out itemData);
    }

    public bool ContainsItemType(ItemType type)
    {
        return m_items.ContainsKey(type);
    }
}
