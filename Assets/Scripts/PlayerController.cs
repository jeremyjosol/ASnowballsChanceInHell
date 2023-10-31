using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; 
    // private int count;
    private float movementX;
    private float movementY;

    public float speed = 0; 

    void Start()
    {
        // count = 0; 
        rb = GetComponent<Rigidbody>();
        // winTextObject.SetActive(false);
        // SetCountText();
    }
 
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed); 
    }

     void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            // count++; 
            // SetCountText();
            // pickupSound.clip = sfx;
            // pickupSound.Play();
        }
    }
}