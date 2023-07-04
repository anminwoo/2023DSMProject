using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "enemyData", menuName = "Scriptable Object/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Stat")]
    public int maxHp;
    public float attackCd;
    public int damage;
    public float speed;
    public float searchRange;
    
<<<<<<< HEAD
    [Header("Animator")]
    public RuntimeAnimatorController controller;
=======
    [Header("Monster Type")]
    [SerializeField] Animator animator;
>>>>>>> main
}
