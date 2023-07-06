using System;
using System.Collections;
using System.Collections.Generic;
using Scripts_An;
using Scripts_Baek;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public enum AttackType
    {
        Melee, 
        Range
    }
    public EnemyData[] data;
    [SerializeField] private Animator animator;
    public int damage;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float attackCd;
    [SerializeField] private int hp;
    [SerializeField] private float speed;
    Transform target;
    private IObjectPool<Enemy> enemyPool;
    private CircleCollider2D searchCol;
    private SpriteRenderer spr;
    private Vector3 dir;
    public int type;
    private Rigidbody2D rb;
    private static readonly int HasTarget = Animator.StringToHash("hasTarget");

    public void SetPool(IObjectPool<Enemy> pool)
    {
        enemyPool = pool;
    }
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        searchCol = GetComponent<CircleCollider2D>();
        spr = GetComponent<SpriteRenderer>();
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
        AudioManager.instance.playSfx(AudioManager.Sfx.MonDam);
        hp -= damage;
        if (hp <= 0)
        {
            animator.SetTrigger("Die");
        }
    }

    public void Die()
    {
        enemyPool.Release(this);
        if (enemyPool.CountInactive == 0)
        {
            BattleSystem.Singleton.BattleStop();
        }
    }
    
    public IEnumerator Attack(PlayerController player)
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
        yield return new WaitForSeconds(attackCd);
        animator.SetBool("isAttacking", false);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !animator.GetBool("isAttacking") && data[type].attackType == AttackType.Melee)
        {
            StartCoroutine(Attack(other.gameObject.GetComponent<PlayerController>()));
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
            StartCoroutine(Attack(other.gameObject.GetComponent<PlayerController>()));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool(HasTarget, false);
        }
    }

    IEnumerator addAlpha()
    {
        for (float i = 0; i <= 1; i += 0.02f)
        {
            yield return new WaitForSeconds(0.01f);
            spr.color = new Color(1, 1, 1, i);
        }
    }

    public void OnEnable()
    {
        EnemyData d = data[type];
        spr.color = new Color(1,1,1,0);
        StartCoroutine(addAlpha());
        hp = d.maxHp;
        speed = d.speed;
        attackCd = d.attackCd;
        damage = d.damage;
        searchCol.radius = d.searchRange;
        animator.runtimeAnimatorController = d.controller;
        projectile = d.projectile;
        if(target == null) target = GameManager.Singleton.player.transform;
    }

    // public void Init(EnemyData data)
    // {
    //     spr.color = new Color(1,1,1,0);
    //     StartCoroutine(addAlpha());
    //     hp = data.maxHp;
    //     speed = data.speed;
    //     attackCd = data.attackCd;
    //     damage = data.damage;
    //     searchCol.radius = data.searchRange;
    //     animator.runtimeAnimatorController = data.controller;
    //     projectile = data.projectile;
    //     if(target == null) target = GameManager.Singleton.player.transform;
    //     StartCoroutine(damaged());
    // }
}
