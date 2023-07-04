using System;
using System.Collections;
using System.Collections.Generic;
using Scripts_ojy;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData[] data;
    [SerializeField] private Animator animator;
    public int damage;
    [SerializeField] private float attackCd;
    [SerializeField] private bool isAttacking;
    [SerializeField] private int hp;
    [SerializeField] private float speed;
    [SerializeField] Transform target;
    private CircleCollider2D searchCol;
    private Vector3 dir;
    public int type;

    private Rigidbody2D rb;
    private static readonly int HasTarget = Animator.StringToHash("hasTarget");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        searchCol = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        Init(data[type]);
    }

    void FixedUpdate()
    {
        Vector3 nextVec = target.position - transform.position;
        if (animator.GetBool(HasTarget))
        {
            rb.velocity = nextVec.normalized * speed;
        }
    }

    public void Damaged(int damage)
    {
        animator.SetTrigger("Damaged");
        hp -= damage;
    }
    
    public IEnumerator Attack(Player player)
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        if (type == 5 || type == 6)
        {
            Instantiate();
        }
        else
        {
            // 플레이어 체력--;
        }
        yield return new WaitForSeconds(attackCd);
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool(HasTarget, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool(HasTarget, false);
        }
    }

    public void Init(EnemyData data)
    {
        hp = data.maxHp;
        speed = data.speed;
        attackCd = data.attackCd;
        damage = data.damage;
        searchCol.radius = data.searchRange;
        animator.runtimeAnimatorController = data.controller;
        if(target == null) target = GameManager.Singleton.player.transform;
    }
}
