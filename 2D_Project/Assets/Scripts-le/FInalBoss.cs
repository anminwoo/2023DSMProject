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
    [SerializeField] private int shotCount;
    [SerializeField] private float multiShotCount;
    [SerializeField] private int summonCount;
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
        anim.SetTrigger("damaged");
        AudioManager.instance.playSfx(AudioManager.Sfx.MonDam);
        hp -= dmg;
        if (hp <= 0)
        {
            anim.SetTrigger("die");
            StartCoroutine(OnDie());
        }
    }

    IEnumerator OnDie()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    IEnumerator Attack()
    {
        while (hp > 0)
        {
            yield return new WaitForSeconds(attackCd);
            anim.SetTrigger("attack");
            int skill = Random.Range(0, pattern.Length+2);
        
            switch (skill)
            {
                case (int)Skills.shot:
                    for (float i=0; i <= shotCount; i++)
                    {
                        Vector3 dir = target.position - transform.position;
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
                        Instantiate(pattern[(int)Skills.shot], transform.position, quat);
                        yield return new WaitForSeconds(0.5f);
                    }
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
                        Vector3 dir = target.position - transform.position;
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
                        Instantiate(pattern[(int)Skills.shot], transform.position + Random.insideUnitSphere * summonRange, quat);
                    }
                    break;
            }
        }
    }
}
