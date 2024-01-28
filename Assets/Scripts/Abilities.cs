using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Abilities : MonoBehaviour
{
    public float DashCooldown = 5.0f;
    public float CurrentDashCooldown = 0;
    public float DashSpeed = 10f;
    public float ProjectileCooldown = 0.5f;
    public float CurrentProjectileCooldown = 0;

    public bool Blocking = false;
    public bool Dashing = false;
    bool Projectiling = false;
    public bool Punching = false;
    public GameObject _projectilePrefab;

    Rigidbody rb;
    Movement mv;
    Attack attack;
    Damageable damageable;

    Attack DashAttack;
    Attack PunchAttack;

    public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mv = GetComponent<Movement>();
        damageable = GetComponent<Damageable>();
        PunchAttack = transform.GetChild(0).GetComponent<Attack>();
        DashAttack = transform.GetChild(1).GetComponent<Attack>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentDashCooldown > 0)
            CurrentDashCooldown -= Time.deltaTime;

        if (Projectiling && CurrentProjectileCooldown <= 0) {
            // throw projectile
            Vector3 offset = new Vector3(2 * mv.FacingDirection, 0, 0);
            /* GameObject projectile = Instantiate(_projectilePrefab, transform.position + offset, Quaternion.identity);
            projectile.GetComponent<Projectile>().Direction = new Vector3(mv.FacingDirection, 0, 0); */
            CurrentProjectileCooldown = ProjectileCooldown;
        }
        else if (CurrentProjectileCooldown > 0)
            CurrentProjectileCooldown -= Time.deltaTime;
    }

    public void Block(InputAction.CallbackContext context) {
        if (context.canceled) {
            Blocking = false;
            animator.SetBool("Squating", false);
            transform.LookAt(mv.Target);
        }
        if (!context.started || AbilityBeingUsed()) return;
        Blocking = true;
        transform.LookAt(new Vector3(transform.position.x, transform.position.y, 10));
        animator.SetBool("Squating", true);
        Debug.Log("Block");
    }

    public void Dash(InputAction.CallbackContext context) {
        if (!context.started || AbilityBeingUsed() || CurrentDashCooldown > 0) return;
        Dashing = true;
        animator.SetBool("Dashing", true);
        DashAttack.Attacking = true;
        
        CurrentDashCooldown = DashCooldown;
        transform.LookAt(new Vector3(-mv.Target.x, transform.position.y, transform.position.z));
        Invoke("EndDash", 0.5f);
        Debug.Log("Dash");
    }

    private void EndDash() {
        Dashing = false;
        DashAttack.Attacking = false;
        transform.LookAt(mv.Target);
        Debug.Log("End Dash");
        animator.SetBool("Dashing", false);
    }

    public void Projectile(InputAction.CallbackContext context) {
        if (context.canceled) {
            Projectiling = false;
            transform.LookAt(mv.Target);
            animator.SetBool("Twerking", false);
            transform.GetChild(4).transform.Translate(new Vector3(-.0f, .4f, .2f));
        }
        if (!context.started || AbilityBeingUsed()) return;
        if (!Projectiling) {
            Debug.Log(transform.GetChild(4).name);
            transform.GetChild(4).transform.Translate(new Vector3(.0f, -.4f, -.2f));
        }
        Projectiling = true;
        transform.LookAt(new Vector3(-mv.Target.x, transform.position.y, transform.position.z));
        animator.SetBool("Twerking", true);
        Debug.Log("Twerk");
    }

    public void Punch(InputAction.CallbackContext context) {
        if (!context.started || AbilityBeingUsed()) return;
        Punching = true;
        PunchAttack.Attacking = true;
        transform.LookAt(new Vector3(transform.position.x, transform.position.y, -10 * mv.FacingDirection));
        animator.SetBool("Punching", true);
        Invoke("EndPunch", 1f);
        Debug.Log("Punch");
    }

    private void EndPunch() {
        PunchAttack.Attacking = false;
        animator.SetBool("Punching", false);
        Punching = false;
        transform.LookAt(mv.Target);
        Debug.Log("End Punch");
    }

    public bool AbilityBeingUsed() {
        return Punching || Dashing || Blocking || Projectiling;
    }
}
