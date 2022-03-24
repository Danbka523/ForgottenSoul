using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public List<Item> playerItems = new List<Item>();
    public ItemDB itemDB;
    public InventoryUI inventoryUI;

    //test
    private void Start()
    {
        GiveItem(0);
    }

    public void GiveItem(int id) { 
        Item itemToAdd=itemDB.GetItem(id);
        playerItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("added:" + itemToAdd.name);
    } 
    public void GiveItem(string name) { 
        Item itemToAdd=itemDB.GetItem(name);
        playerItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("added:" + itemToAdd.name);
    }

    public Item CheckItem(int id) {
        return playerItems.Find(x => x.id == id);
    }
    public Item CheckItem(string name)
    {
        return playerItems.Find(x => x.name == name);
    }

    public void RemoveItem(int id) { 
        Item item=CheckItem(id);
        if (item != null)
        {
            playerItems.Remove(item);
            inventoryUI.RemoveItem(item);
            Debug.Log("item removed:"+item.name);
        }
    }

    public void RemoveItem(string name) {
        Item item = CheckItem(name);
        if (item != null)
        {
            playerItems.Remove(item);
            inventoryUI.RemoveItem(item);
            Debug.Log("item removed:" + item.name);
        }
    }


}
