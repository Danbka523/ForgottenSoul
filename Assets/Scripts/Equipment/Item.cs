using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string name;
    public string description;
    public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    public Item(int id, string name, string description, Dictionary<string, int> stats)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        Debug.Log("Sprites/" + name);
        this.icon = Resources.Load<Sprite>("Sprites/"+name);
        this.stats = stats; 
    }

    public Item(Item item) { 
        this.id = item.id;
        this.name = item.name;
        this.description = item.description;
        this.stats = item.stats;
        this.icon = Resources.Load<Sprite>("Sprites/" + item.name);

    }



}
