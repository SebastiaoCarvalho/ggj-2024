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
    List<Rigidbody> rbList = new List<Rigidbody>();
    Abilities ab;

    public bool IsKnockbacked { get; private set; }

    public Vector3 Target;
    public Animator animator;

    private void Awake()
    {
        addRigidBodyToList(this.gameObject);
        ab = GetComponent<Abilities>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetNewTarget(new Vector3(FacingDirection * 1000, transform.position.y, transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Count" + rbList.Count);
        GameObject floor = GameObject.FindGameObjectWithTag("Floor");
        if (GetComponent<Collider>().bounds.Intersects(floor.GetComponent<Collider>().bounds))
            IsGrounded = true;
        else
            IsGrounded = false;
        if (!IsGrounded) {
            GetComponent<Animator>().enabled = false;
        }
        else {
            GetComponent<Animator>().enabled = true;
        }
    }

    void addRigidBodyToList(GameObject obj) {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null) {
            rbList.Add(rb);
        }
        foreach (Transform child in obj.transform) {
            addRigidBodyToList(child.gameObject);
        }
    }

    private void FixedUpdate() {
        if (ab.Dashing) {
            Vector3 direction = Target - transform.position;
            /* Debug.Log(direction); */
            transform.Translate(direction.normalized * ab.DashSpeed * Time.deltaTime, Space.World);
        }
        else if (Moving && !ab.AbilityBeingUsed())  {
            Vector3 direction = Target - transform.position;
            /* Debug.Log(direction); */
            transform.Translate(direction.normalized * MoveSpeed * Time.deltaTime, Space.World);
            //rbList.ForEach(rb => rb.velocity = new Vector2(MoveSpeed * Direction.x, rb.velocity.y));
            /* Debug.Log(rb.velocity);*/
        }
        else if (IsKnockbacked) {
            rbList.ForEach(rb => rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y));
        }
        else if (CanMove)
            rbList.ForEach(rb => rb.velocity = new Vector2(0, rb.velocity.y));
    }

    public void Move(InputAction.CallbackContext context) {
        if (context.canceled) {
            Moving = false;
            animator.SetBool("Walking", false);
        }
        if (!context.started) return;
        Moving = true;
        animator.SetBool("Walking", true);
        Direction = context.ReadValue<Vector2>();
        if (Direction.x != FacingDirection) {
            SetNewTarget(new Vector3(-1 * Target.x, Target.y, Target.z));
            FacingDirection = Direction.x;
        }
        Debug.Log(Direction);
    }

    void SetNewTarget(Vector3 newTarget) {
        Target = newTarget;
        transform.LookAt(Target);
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.canceled) Jumping = false;
        if (!context.started) return;
        Jumping = true;
        if (IsGrounded)
            rbList.ForEach(rb => rb.velocity = new Vector2(rb.velocity.x, Impulse));
    }

    public void Knockback(Vector2 knockback) {
        CanMove = false;
        rbList.ForEach(rb => rb.velocity = new Vector2(knockback.x, 0));
        transform.Translate(new Vector3(knockback.x, 0, 0) * Time.deltaTime, Space.World);
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
