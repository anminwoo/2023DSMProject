using System;
using System.Collections;
using System.Collections.Generic;
using Scripts_Baek;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public Slider hpSlider;
    public TextMeshProUGUI goldText;
    public Button waveStart;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hpSlider.maxValue = GameManager.Singleton.player.maxHp;
        goldText.text = $"{GameManager.Singleton.gold}";
    }

    public void OnClick()
    {
        BattleSystem.Singleton.BattleStart();
    }
}
