using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 3f;

    private void LateUpdate()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }   
}
