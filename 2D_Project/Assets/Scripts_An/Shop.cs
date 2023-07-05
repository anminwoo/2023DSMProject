using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Transform[] itemSpawnPos;
    [SerializeField] private List<GameObject> spawnableItems;

    public void SpawnItems()
    {
        for (int i = itemSpawnPos.Length - 1; i >= 0; i--)
        {
            int randomNum = Random.Range(0, itemSpawnPos.Length);
            ItemSpawnSystem.Singleton.SpawnItem(spawnableItems[randomNum], itemSpawnPos[i]);
        }
    }
}
