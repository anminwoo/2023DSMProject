using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    void Update()
    {
        Rotate();
    }
    
    private void Rotate()
    {
        float horizontal = -Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(0, 0, horizontal);
    }
}
