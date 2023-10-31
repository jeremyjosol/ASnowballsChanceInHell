using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 45.0f;

    private float currentRotation = 0.0f;

    // Update is called once per frame
    void Update()
    {
        currentRotation += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, currentRotation, 0);
    }
}
