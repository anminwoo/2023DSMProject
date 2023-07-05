using System;
using System.Collections;
using System.Collections.Generic;
using Scripts_Baek;
using Scripts_Baek.Item.Core;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public Item item;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = GameManager.Singleton.player;
            if (GameManager.Singleton.gold >= item.ItemInfo.ItemCost)
            {
                GameManager.Singleton.gold -= item.ItemInfo.ItemCost;
            }
        }
    }
}
