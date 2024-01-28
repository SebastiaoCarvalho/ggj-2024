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
    bool CanMove = true;
    public float FacingDirection = 1f;
    Vector2 Direction = Vector2.zero;
    Rigidbody rb;
    Abilities ab;

    public bool IsKnockbacked { get; private set; }


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
        /*if (ab.Dashing)
            rb.velocity = rb.velocity;
        else if (Moving)  {
            rb.velocity = new Vector2(Direction.x * MoveSpeed, rb.velocity.y);
            /* Debug.Log(rb.velocity); 
        }
        else if (IsKnockbacked) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        else if (CanMove)
            rb.velocity = new Vector2(0, rb.velocity.y);*/
    }

    public void Move(InputAction.CallbackContext context) {
        if (context.canceled) Moving = false;
        if (!context.started) return;
        Moving = true;
        Direction = context.ReadValue<Vector2>();
        if (Direction.x != FacingDirection) {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            FacingDirection = Direction.x;
        }
        Debug.Log(Direction);
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.canceled) Jumping = false;
        if (!context.started) return;
        Jumping = true;
        if (IsGrounded)
            rb.velocity = new Vector2(rb.velocity.x, Impulse);
    }

    public void Knockback(Vector2 knockback) {
        CanMove = false;
        rb.velocity = new Vector2(knockback.x + rb.velocity.x, knockback.y + Impulse);
        IsKnockbacked = true;
        Invoke("EndKnockback", 0.3f);
        Debug.Log("End knockback");
    }

    private void EndKnockback() {
        CanMove = true;
        Debug.Log("End knockback");
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a specific tag
        if (collision.gameObject.CompareTag("Floor"))
        {
            IsGrounded = true;
            IsKnockbacked = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the colliding object has a specific tag
        if (collision.gameObject.CompareTag("Floor"))
        {
            IsGrounded = false;
        }
    }
}
