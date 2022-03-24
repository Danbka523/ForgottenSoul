using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemUI : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    private Image itemSprite;
    [SerializeField] Unit player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Unit>();
        itemSprite = GetComponent<Image>();
        UpdateItem(null);
    }

    public void UpdateItem(Item item) { 
        this.item = item;
        if (item != null)
        {
            itemSprite.color = Color.white;
            itemSprite.sprite = this.item.icon;
        }
        else
        {
            itemSprite.color = Color.clear;
        }
    
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            player.DoAction(item.name);
        }
    }
}
