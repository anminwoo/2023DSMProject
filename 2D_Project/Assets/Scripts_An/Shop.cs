// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Scripts_Baek.Item.Core;
// using TMPro;
// using UnityEngine;
// using Random = UnityEngine.Random;
//
// public class Shop : MonoBehaviour
// {
//
//     [SerializeField] private Transform[] itemPos;
//     [SerializeField] private Passive[] passives;
//
//     [SerializeField] private ShopItem shopItemPrefab;
//
//     public void Init()
//     {
//         for (int i = itemPos.Length - 1; i >= 0; i--)
//         {
//             var newShowItem = Instantiate(shopItemPrefab, itemPos[i].position, Quaternion.identity);
//             newShowItem.passive = passives[Random.Range(0, passives.Length)];
//             newShowItem.costText.text = $"{newShowItem.passive.ItemInfo.ItemCost}";
//             newShowItem.passive.Initialize();
//         }
//     }
//
// }
