using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData", fileName = "ItemData")]
public class ItemData : ScriptableObject
{
    public int damage;
    public int hp;
    public float speed;
}
