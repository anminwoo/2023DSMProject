using System;
using System.Collections;
using System.Collections.Generic;
using Scripts_Baek;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    private SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spr.flipY = GameManager.Singleton.player.transform.position.x < transform.position.x;
    }

    void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
