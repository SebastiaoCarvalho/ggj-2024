using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int Damage = 1;
    public float KnockbackStrengthX = 1f;
    public float KnockbackStrengthY = 1f;
    public bool Attacking = false;

    Movement mv;

    // Start is called before the first frame update
    void Start()
    {
        mv = transform.parent.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider collision) {
        if (Attacking && collision.gameObject.CompareTag("Player")) {
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();

            if (damageable != null) {
                Vector2 knockback = new Vector2(KnockbackStrengthX * mv.FacingDirection, KnockbackStrengthY);
                knockback *= 1 + (damageable.HP * 0.5f);
                damageable.Hit(Damage, knockback);
                Debug.Log("Damage");
            }
        }
    }
}
