using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int damage;

    [SerializeField] private float coolTime;

    [SerializeField] private bool isCoolTime;

    [SerializeField] private float rotationSpeed;

    public Animator animator;
    public BoxCollider2D collider;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isCoolTime == false)
            {
                StartCoroutine(Attack());
            }
        }
        Rotate();
    }

    private void Rotate()
    {
        Vector2 target = transform.position;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float Angle = Mathf.Atan2(pos.y - target.y, pos.x - target.x) * Mathf.Rad2Deg - 45;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Angle));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Damaged(damage);
        }
    }

    public IEnumerator Attack()
    {
        isCoolTime = true;
        animator.SetBool("MotionEnd", false);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(coolTime);
        animator.SetBool("MotionEnd", true);
        isCoolTime = false;
    }

}