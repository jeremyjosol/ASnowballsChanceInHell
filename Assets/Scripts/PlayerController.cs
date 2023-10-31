using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; 
    // private int count;
    public float jumpAmount = 80;
    public float gravityScale = 10;
    public float fallingGravityScale = 40;
    private float movementX;
    private float movementY;

    public float speed = 0; 
    private bool isGrounded = true;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    
    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed); 
        // rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);

        if (isGrounded)
        {
            rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
        }
        else
        {
            rb.AddForce(Physics.gravity * (fallingGravityScale - 1) * rb.mass);
        }
    }

     void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // The player is grounded when colliding with an object tagged as "Ground."
        }
        
        
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