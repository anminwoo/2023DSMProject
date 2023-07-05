using System;
using System.Collections;
using System.Collections.Generic;
using Scripts_Baek;
using UnityEngine;

public class MoveHpBar : MonoBehaviour
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.Singleton.player.transform.position + new Vector3(0,-0.1f,0));
    }
}
