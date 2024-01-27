using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Attack
{
    public Vector3 Direction = Vector3.right;
    public float Speed = 5.0f;
    public float Intensity = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Speed * Time.deltaTime * Direction);
    }

    private void OnTriggerStay(Collider collision) {} // do nothing

    private void OnTriggerEnter(Collider collision) {
        Debug.Log("Player Triggered");
        if (collision.gameObject.CompareTag("Player")) {
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();
            Movement mv = collision.gameObject.GetComponent<Movement>();

            if (damageable != null) {
                Vector2 knockback = new Vector2(Direction.x * KnockbackStrengthX, KnockbackStrengthY);
                knockback *= 1 + (damageable.HP * Intensity);
                Debug.Log("Knockback: " + knockback);
                damageable.Hit(Damage, knockback);
                Debug.Log("Damage");
            }
            Destroy(this.gameObject);
        }
    }
}
