// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Scripts_Baek;
// using Scripts_Baek.Item.Core;
// using TMPro;
// using UnityEngine;
//
// public class ShopItem : MonoBehaviour
// {
//     public Passive passive;
//     public TextMeshProUGUI costText;
//
//     private void Start()
//     {
//         
//         costText.text = $"{passive.ItemInfo.ItemCost}";
//     }
//
//     private void OnCollisionEnter2D(Collision2D other)
//     {
//         if (other.gameObject.CompareTag("Player"))
//         {
//             if (GameManager.Singleton.gold >= passive.ItemInfo.ItemCost)
//             {
//                 GameManager.Singleton.gold -= passive.ItemInfo.ItemCost;
//                 GameManager.Singleton.AddItem(passive);
//                 passive.OnGet();
//                 gameObject.SetActive(false);
//             }
//         }
//     }
// }
