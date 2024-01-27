using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Abilities : MonoBehaviour
{
    bool Blocking = false;
    bool Dashing = false;
    bool Projectiling = false;
    bool Punching = false;
    public GameObject _projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Block(InputAction.CallbackContext context) {
        if (context.canceled) Blocking = false;
        if (!context.started) return;
        Blocking = true;
        Debug.Log("Block");
    }

    public void Dash(InputAction.CallbackContext context) {
        if (context.canceled) Dashing = false;
        if (!context.started) return;
        Dashing = true;
        Debug.Log("Dash");
    }

    public void Projectile(InputAction.CallbackContext context) {
        if (context.canceled) Projectiling = false;
        if (!context.started) return;
        Projectiling = true;
        Debug.Log("Twerk");
        // throw projectile
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);   
    }

    public void Punch(InputAction.CallbackContext context) {
        if (context.canceled) Punching = false;
        if (!context.started) return;
        Punching = true;
        Debug.Log("Punch");
    }
}
