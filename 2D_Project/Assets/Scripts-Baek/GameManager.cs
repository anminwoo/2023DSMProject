using System;
using System.Collections.Generic;
using Scripts_An;
using Scripts_Baek.Item.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts_Baek
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _singleton;

        public static GameManager Singleton
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
                    Debug.Log("경고 : 이미 게임메니저가 존재합니다!");
                    Destroy(value.gameObject);
                }
            }
        }

        public PlayerController player;

        public int gold;
        
        private void Start()
        {
            AudioManager.instance.PlayBgm(true);
        }

        private void Awake()
        {
            Singleton = this;
        }
    }
}