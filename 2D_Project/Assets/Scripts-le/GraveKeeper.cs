using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Scripts_Baek;
using UnityEngine;
using Random = UnityEngine.Random;

public class GraveKeeper : MonoBehaviour
{
    enum Skills
    {
        shot, multiShot, summon
    }
    [SerializeField] private int hp;
    [SerializeField] private float attackCd;
    private bool isAttacking;
    [SerializeField] private float multiShotCount;
    [SerializeField] private int summonCount;
    [SerializeField] private float enemyDieTime;
    [SerializeField] private float summonRange;
    [SerializeField] private GameObject[] pattern;
    private Animator anim;
    private SpriteRenderer spr;
    private Transform target;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        target = GameManager.Singleton.player.transform;
        StartCoroutine(Attack());
    }

    private void Update()
    {
        spr.flipX = target.position.x < transform.position.x;
    }
    
    public void Damaged(int dmg)
    {
        anim.SetTrigger("Damaged");
        AudioManager.instance.playSfx(AudioManager.Sfx.MonDam);
        hp -= dmg;
        if (hp <= 0)
        {
            anim.SetTrigger("Die");
            StartCoroutine(OnDie());

        }
    }

    IEnumerator OnDie()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    IEnumerator killEnemy(Enemy enemy)
    {
        yield return new WaitForSeconds(enemyDieTime);
        enemy.Damaged(999);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackCd);
            anim.SetTrigger("Attack");
            int skill = Random.Range(0, pattern.Length+1);
        
            switch (skill)
            {
                case (int)Skills.shot:
                    Vector3 dir = target.position - transform.position;
                    float angle = MathF.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
                    Instantiate(pattern[(int)Skills.shot], transform.position, quat);
                    break;
                case (int)Skills.multiShot:
                    for (float i=0; i <= 360; i += 360 / multiShotCount)
                    {
                        Quaternion quaternion = Quaternion.AngleAxis(i, Vector3.forward);
                        Instantiate(pattern[(int)Skills.shot], transform.position, quaternion);
                    }
                    break;
                case (int)Skills.summon:
                    for (int i = 0; i < summonCount; i++)
                    {
                        Enemy enemy = PoolManager.instance.enemyPool.Get();
                        enemy.transform.position = transform.position + Random.insideUnitSphere * summonRange;
                        StartCoroutine(killEnemy(enemy));
                    }
                    break;
            }
        }
    }
}
