using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "enemyData", menuName = "Scriptable Object/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Stat")]
    [SerializeField] int hp;
    [SerializeField] float attackCd;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    
    [Header("Monster Type")]
    [SerializeField] Animator _animator;
}
