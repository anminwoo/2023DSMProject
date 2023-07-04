using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 InputVector;
    public float speed;

    private Rigidbody2D rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rigid.AddForce(InputVector * speed);
    }

    void OnMove(InputValue value)
    {
        InputVector = value.Get<Vector2>();
    }
    
}
