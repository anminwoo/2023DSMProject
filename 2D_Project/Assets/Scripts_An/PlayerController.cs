using System;
using System.Collections;
using System.Collections.Generic;
using Scripts_Baek.Item.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamagable
{
    [SerializeField] private Vector2 inputVector;

    public int maxHp;
    public int currentHp;
    public int currentDamage;

    [SerializeField] private float currentSpeed;
    
    private Rigidbody2D _rigid;
    private SpriteRenderer _sr;
    private Animator _anim;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 nextVector = inputVector.normalized * currentSpeed;
        _rigid.velocity = nextVector;
    }

    private void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    private void LateUpdate()
    {
        _anim.SetBool("IsWalk", inputVector != Vector2.zero);
        if (inputVector.x != 0)
        {
            _sr.flipX = inputVector.x < 0;
        }
    }

    public void GetDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _anim.SetTrigger("Die");
    }

    public void ChangeStatus(ItemData itemData)
    {
        currentDamage += itemData.damage;
        currentHp += itemData.hp;
        currentSpeed += itemData.speed;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                GetDamage(enemy.damage);    
            }
        }
    }
}
