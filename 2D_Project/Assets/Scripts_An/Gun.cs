using System.Collections;
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
        float horizontal = -Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(0, 0, horizontal);
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