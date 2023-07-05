using System;
using System.Collections;
using System.Collections.Generic;
using Scripts_Baek;
using Scripts_ojy;
using Scripts_ojy.Player;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum AttackType
    {
        Melee, 
        Range
    }
    [SerializeField] private EnemyData[] data;
    [SerializeField] private Animator animator;
    public int damage;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float attackCd;
    [SerializeField] private int hp;
    [SerializeField] private float speed;
    Transform target;
    private CircleCollider2D searchCol;
    private SpriteRenderer spr;
    private Vector3 dir;
    public int type;
    private Rigidbody2D rb;
    private static readonly int HasTarget = Animator.StringToHash("hasTarget");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        searchCol = GetComponent<CircleCollider2D>();
        spr = GetComponent<SpriteRenderer>();
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
            spr.flipX = target.position.x < transform.position.x;
        }
    }

    public void Damaged(int damage)
    {
        animator.SetTrigger("Damaged");
        if (this.damage > 0)
        {
            hp -= damage;
        }
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    
    public IEnumerator Attack(Player player)
    {
        animator.SetTrigger("Attack");
        animator.SetBool("isAttacking", true);
        if (data[type].attackType == AttackType.Range)
        {
            Vector3 dir = target.position - transform.position;
            float angle = MathF.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(projectile, transform.position, quat);
        }
        else
        {
            player.currentHp -= damage;
        }
        yield return new WaitForSeconds(attackCd);
        animator.SetBool("isAttacking", false);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !animator.GetBool("isAttacking") )
        {
            StartCoroutine(Attack(other.gameObject.GetComponent<Player>()));
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool(HasTarget, true);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && data[type].attackType == AttackType.Range && !animator.GetBool("isAttacking"))
        {
            StartCoroutine(Attack(other.gameObject.GetComponent<Player>()));
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
        projectile = data.projectile;
        if(target == null) target = GameManager.Singleton.player.transform;
    }
}
