using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData[] data;
    [SerializeField] private Animator animator;
    [SerializeField] private int damage;
    [SerializeField] private float attackCd;
    [SerializeField] private int hp;
    [SerializeField] private float speed;
    [SerializeField] private float searchRange;
    private Transform target;

    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Init(data[0]);
    }

    void FixedUpdate()
    {
        if (animator.GetBool("hasTarget"))
        {
            Vector3 nextVec = target.position.normalized * speed;
            rb.MovePosition(transform.position + nextVec);
        }
    }

    public void Init(EnemyData data)
    {
        hp = data.maxHp;
        speed = data.speed;
        attackCd = data.attackCd;
        damage = data.damage;
        searchRange = data.searchRange;
        animator.runtimeAnimatorController = data.controller;
    }
}
