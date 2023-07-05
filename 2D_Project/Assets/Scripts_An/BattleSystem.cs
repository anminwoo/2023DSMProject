using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private static BattleSystem _singleton;

    public static BattleSystem Singleton
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
                Debug.Log("경고 : 이미 배틀시스템이 존재합니다!");
                Destroy(value.gameObject);
            }
        }
    }

    [SerializeField] public bool isBattle;

    public BoxCollider2D[] lockers;

    private void Awake()
    {
        Singleton = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BattleStart();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            BattleStop();
        }
    }

    public void BattleStart()
    {
        isBattle = true;
        for (int i = lockers.Length - 1; i >= 0; i--)
        {
            lockers[i].isTrigger = false;
        }
    }

    public void BattleStop()
    {
        isBattle = false;
        for (int i = lockers.Length - 1; i >= 0; i--)
        {
            lockers[i].isTrigger = true;
        }
    }
}
