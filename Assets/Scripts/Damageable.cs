using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    float Health = 0f;

    public float HP {
        get { return Health; }
        set { Health = value; }
    }
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
    private void Update() {
    }

    public void Hit(int damage, Vector2 knockback) {
        bool isInvincible = GetComponent<Abilities>().Blocking || GetComponent<Abilities>().Dashing;
        if (!isInvincible) {
            Health += damage;
            this.gameObject.GetComponent<Player>().TakeDamage(damage);
            mv.Knockback(knockback);
        }
    }
}
