using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public float Impulse = 4f;

    public bool Moving = false;
    bool Jumping = false;
    bool IsGrounded = false;
    public float FacingDirection = 1f;
    Vector2 Direction = Vector2.zero;
    Rigidbody rb;
    Abilities ab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ab = GetComponent<Abilities>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (ab.Dashing)
            rb.velocity = rb.velocity;
        else if (Moving)  {
            rb.velocity = new Vector2(Direction.x * MoveSpeed, rb.velocity.y);
            /* Debug.Log(rb.velocity); */
        }
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context) {
        if (context.canceled) Moving = false;
        if (!context.started) return;
        Moving = true;
        Direction = context.ReadValue<Vector2>();
        FacingDirection = Direction.x;
        Debug.Log(Direction);
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.canceled) Jumping = false;
        if (!context.started) return;
        Jumping = true;
        if (IsGrounded)
            rb.velocity = new Vector2(rb.velocity.x, Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a specific tag
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Colide");
            IsGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the colliding object has a specific tag
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Nao Colide");
            IsGrounded = false;
        }
    }
}
