using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Specialized;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Transform ballTransform;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private int count;
    private Rigidbody rb; 
    // private int count;
    // public float jumpAmount = 80;
    // public float gravityScale = 10;
    // public float fallingGravityScale = 40;
    private float movementX;
    private float movementY;
    public int jumpSpeed = 0;
    public float speed = 0; 
    private bool isTouching = true;
    // private bool isGrounded = true;
    // private bool hasPickedUp = false;

    private Vector3 melted = new Vector3(0.25f, 0.25f, 0.25f);

    void Start()
    {
        count = 0; 
        rb = GetComponent<Rigidbody>();
        winTextObject.SetActive(false);
        SetCountText();
        StartCoroutine(Shrink(melted, 90));
    }
 
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    void Update()
    {
        if (Input.GetKey("space") && isTouching == true)
        {
            Vector3 ballJump = new Vector3(0.0f, 6.0f, 0.0f);
            rb.AddForce(ballJump * jumpSpeed);
        }
        isTouching = false;
        if (ballTransform.position.y < 1.0f)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed); 
        // rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    }
    //     if (isGrounded)
    //     {
    //         rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    //     }
    //     else
    //     {
    //         rb.AddForce(Physics.gravity * (fallingGravityScale - 1) * rb.mass);
    //     }
    // }

    void OnTriggerEnter(Collider other) 
    {
        // if (other.gameObject.CompareTag("Ground"))
        // {
        //     isGrounded = true;
        // }
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            // speed = 100;
            // Vector3 movement = new Vector3 (movementX, 0f, movementY);
            // rb.AddForce(movement * 10000);
            count++; 
            SetCountText();
            // pickupSound.clip = sfx;
            // pickupSound.Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isTouching = true;
    }

    void SetCountText() 
    {
        countText.text =  "Score: " + count.ToString();
        if (count >= 1000)
        {
           winTextObject.SetActive(true);
        }
    }

    IEnumerator Shrink(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        yield return null;
    }
}