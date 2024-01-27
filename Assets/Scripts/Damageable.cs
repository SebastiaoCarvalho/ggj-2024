using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    bool IsInvincible = false;
    float Health = 0f;

    Movement mv;

    private void Awake()
    {
        mv = GetComponent<Movement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update() {}

    public void Hit(int damage, Vector2 knockback) {
        if (!IsInvincible) {
            Health += damage;

            mv.Knockback(knockback);
        }
    }
}
