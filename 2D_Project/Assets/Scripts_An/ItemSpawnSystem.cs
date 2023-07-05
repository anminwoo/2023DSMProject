using System.Collections;
using System.Collections.Generic;
using Scripts_Baek.Item.Core;
using UnityEngine;

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
    
    public void SpawnItem(GameObject item, Transform spawnPos)
    {
        Instantiate(item, spawnPos.position, Quaternion.identity);
    }
}