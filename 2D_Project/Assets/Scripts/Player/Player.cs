using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 InputVector;
    public float speed;

    private Rigidbody2D rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector2 nextVector = InputVector.normalized * speed;
        rigid.velocity = nextVector;
    }

    void OnMove(InputValue value)
    {
        InputVector = value.Get<Vector2>();
    }
    
}
