using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;
using GameManager = Scripts_Baek.GameManager;

public class FInalBoss : MonoBehaviour
{
    enum Skills
    {
        shot, multiShot, targetShot, 
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
            int skill = Random.Range(0, pattern.Length+1);
        
            switch (skill)
            {
                case (int)Skills.shot:
                    Vector3 dir = target.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
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
                case (int)Skills.targetShot:
                    for (int i = 0; i < summonCount; i++)
                    {
                        Quaternion quaternion = Quaternion.AngleAxis(i, Vector3.forward);
                        Instantiate(pattern[(int)Skills.shot], transform.position + Random.insideUnitSphere * summonRange, quaternion);
                    }
                    break;
            }
        }
    }
}
