using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryUI : MonoBehaviour
{
    public List<ItemUI> uIitems = new List<ItemUI>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numbersOfSlots = 16;


    public void Awake()
    {
          for (int i = 0; i < numbersOfSlots; i++)
          {
              GameObject instance = Instantiate(slotPrefab);
              instance.transform.SetParent(slotPanel);
              uIitems.Add(instance.GetComponentInChildren<ItemUI>());
          }      
    }

    public void UpdateSlot(int slot, Item item) {
        if (uIitems.Count != 0)
            uIitems[slot].UpdateItem(item);
    }

    public void AddNewItem(Item item)
    {
        Debug.Log(uIitems.FindIndex(i => i.item == null));
        UpdateSlot(uIitems.FindIndex(i => i.item == null), item);
    }
    public void RemoveItem(Item item)
    {
        UpdateSlot(uIitems.FindIndex(i => i.item == item), null);
    }


}
