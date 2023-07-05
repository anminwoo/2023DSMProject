using System;
using System.Collections;
using System.Collections.Generic;
using Scripts_Baek.Item.Core;
using UnityEditor;
using UnityEngine;
using Item = Scripts_An.Item;

public class ItemSpawnSystem : MonoBehaviour
{
    private static ItemSpawnSystem _singleton;

    public static ItemSpawnSystem Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton	!= value)
            {
                Debug.Log("경고 : 이미 아이템 스폰 시스템이 존재합니다!");
                Destroy(value.gameObject);
            }
        }
    }

    private void Awake()
    {
        _singleton = this;
    }

    public Item SpawnItem(GameObject item, Transform spawnPos)
    {
        GameObject g = Instantiate(item, spawnPos.position, Quaternion.identity);
        return g.GetComponent<Item>();
    }
}