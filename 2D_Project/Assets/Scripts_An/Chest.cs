using System;
using System.Collections;
using System.Collections.Generic;
using Item.Core;
using Unity.Mathematics;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private ChestData chestData;
    [SerializeField] private bool isOpen;

    private Item.Core.Item item;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AnimationClip clip;

    private void Start()
    {
        item = chestData.spawnableItems.GetRandom();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer.sprite = chestData.chestSprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (isOpen)
        {
            return;
        }
        Debug.Log("open");
        Open();
    }

    public void Open()
    {
        isOpen = true;
        clip.legacy = true;
        // Item.Core.Item spawnItem = Instantiate(item, transform.position, quaternion.identity);
        // spawnItem.Initialize();
    }
}
