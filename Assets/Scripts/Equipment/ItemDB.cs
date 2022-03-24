using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public List<Item> items;

    private void Awake()
    {
        BuildDB();
    }

    void BuildDB() {
        items = new List<Item> {
        new Item(0,"heal1","test heal item", new Dictionary<string, int>
            {
             {"Heal",20 }
            }),
        new Item(1,"damage1","test damage item", new Dictionary<string, int>
        {
            {"Damage",10 }
        }),
        new Item(2,"heal2","test heal item 2", new Dictionary<string, int>
        {
            {"Heal",30 }
        })
        };
    }

    public Item GetItem(int id) { 
        return items.Find(x => x.id == id);
    }

    public Item GetItem(string name) { 
        return items.Find(x=>x.name == name);
    }
}
