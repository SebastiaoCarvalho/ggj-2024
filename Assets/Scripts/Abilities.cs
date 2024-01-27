using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Abilities : MonoBehaviour
{
    public float DashCooldown = 5.0f;
    public float CurrentDashCooldown = 0;
    public float DashImpulse = 20f;
    public float ProjectileCooldown = 1.5f;
    public float CurrentProjectileCooldown = 0;

    bool Blocking = false;
    public bool Dashing = false;
    bool Projectiling = false;
    bool Punching = false;
    public GameObject _projectilePrefab;

    Rigidbody rb;
    Movement mv;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mv = GetComponent<Movement>();
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
            GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            CurrentProjectileCooldown = ProjectileCooldown;
        }
        else if (CurrentProjectileCooldown > 0)
            CurrentProjectileCooldown -= Time.deltaTime;
    }

    public void Block(InputAction.CallbackContext context) {
        if (context.canceled) Blocking = false;
        if (!context.started || AbilityBeingUsed()) return;
        Blocking = true;
        Debug.Log("Block");
    }

    public void Dash(InputAction.CallbackContext context) {
        if (!context.started || AbilityBeingUsed() || CurrentDashCooldown > 0) return;
        Dashing = true;
        
        CurrentDashCooldown = DashCooldown;
        rb.velocity = new Vector2(DashImpulse * mv.FacingDirection, rb.velocity.y);
        Debug.Log(rb.velocity);
        Invoke("EndDash", 0.5f);
        Debug.Log("Dash");
    }

    private void EndDash() {
        Dashing = false;
        Debug.Log("End Dash");
    }

    public void Projectile(InputAction.CallbackContext context) {
        if (context.canceled) Projectiling = false;
        if (!context.started || AbilityBeingUsed()) return;
        Projectiling = true;
        Debug.Log("Twerk");
    }

    public void Punch(InputAction.CallbackContext context) {
        if (!context.started || AbilityBeingUsed()) return;
        Punching = true;
        Invoke("EndPunch", 0.5f);
        Debug.Log("Punch");
    }

    private void EndPunch() {
        Punching = false;
        Debug.Log("End Punch");
    }

    public bool AbilityBeingUsed() {
        return Punching || Dashing || Blocking || Projectiling;
    }
}
