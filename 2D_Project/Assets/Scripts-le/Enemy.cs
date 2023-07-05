using System;
using System.Collections;
using System.Collections.Generic;
using Scripts_ojy;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum AttackType
    {
        melee, range
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
    
    public IEnumerator Attack(Player player, EnemyData data)
    {
        animator.SetTrigger("Attack");
        animator.SetBool("isAttacking", true);
        if (data.attackType == AttackType.range)
        {
            Vector3 dir = target.position - transform.position;
            float angle = MathF.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(projectile, transform.position, quat);
        }
        else
        {
            // 플레이어 체력--;
        }
        yield return new WaitForSeconds(attackCd);
        animator.SetBool("isAttacking", false);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !animator.GetBool("isAttacking"))
        {
            StartCoroutine(Attack(other.gameObject.GetComponent<Player>(), data[type]));
        }
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
        projectile = data.projectile;
        if(target == null) target = GameManager.Singleton.player.transform;
    }
}
