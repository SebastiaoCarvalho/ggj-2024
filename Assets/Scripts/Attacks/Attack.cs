using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int Damage = 1;
    public float KnockbackStrengthX = 1f;
    public float KnockbackStrengthY = 1f;
    public bool Attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision) {
        if (Attacking && collision.gameObject.CompareTag("Player")) {
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();
            Movement mv = collision.gameObject.GetComponent<Movement>();

            if (damageable != null) {
                Vector2 knockback = new Vector2(KnockbackStrengthX, KnockbackStrengthY);
                knockback = Vector2.Scale(knockback, new Vector2(mv.FacingDirection, 1));
                damageable.Hit(Damage, knockback);
                Debug.Log("Damage");
            }
        }
    }
}
