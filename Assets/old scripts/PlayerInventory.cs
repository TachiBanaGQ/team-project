using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;


public class PlayerInventory : MonoBehaviour
{
    //Inspector setup 
    [Header("Settings")]
    [SerializeField] private int _maxCapacity = 10;

    [Header("Events")]
    [SerializeField] private UnityEvent _onInventoryChanged;
    //Private State
    private List<ItemData> _m_heldItems;
    private InventoryUi _inventoryUi;

    //public Read-Only 
    public int Count => _m_heldItems.Count;

    public bool IsFull => _m_heldItems.Count >= _maxCapacity;

    public List <ItemData> HeldItems => _m_heldItems;

    // LifeCycle
    private void Awake()
    {
       _m_heldItems = new List<ItemData>();
        _inventoryUi = FindFirstObjectByType<InventoryUi>();
    }

    //Public APi 

    public bool PickUp(ItemData type)
    {
        if(IsFull)
        {
            Debug.Log("[Inventory]Full - cannot pick up more items.");
            return false;
        }
        
        _m_heldItems.Add(type);
        _onInventoryChanged?.Invoke();
        _inventoryUi.Refresh();
        /*if (ItemDatabase.Instance != null && ItemDatabase.Instance.TryGetItemData(type, out ItemData data))
        {
            Debug.Log($"[Inventory] Picked up : {data.name})");
        }*/

        return true;
    }
      public bool UseItem(ItemData type)
    {
        if (!_m_heldItems.Contains(type))
        {
            Debug.Log($"[Inventory] {type} not in inventory");
            return false;
        }

        _m_heldItems.Remove(type);
        _onInventoryChanged?.Invoke();
        Debug.Log($"[Inventory] Used: {type}");
        return true;
    }
    public bool UseFirst()
    {
        if (_m_heldItems.Count == 0)
        {
            Debug.Log("[Inventory]nothing to use");
            return false;

        }
        ItemData first = _m_heldItems[0];
        _m_heldItems.RemoveAt(0);
        _onInventoryChanged?.Invoke();
        Debug.Log($"[Inventory] Used first item: {first}");
        return true;
    }

    public bool Has(ItemData type) => _m_heldItems.Contains(type);

    public void LogContents()
    {
        if (_m_heldItems.Count == 0)
        {
            Debug.Log("[Inventory] Empty.");
            return;
        }
        foreach (ItemData item in _m_heldItems) Debug.Log($" = {item}");
    }
    public System.Collections.Generic.IReadOnlyList<ItemData> GetItems()
    {
        return _m_heldItems;
    }
}
