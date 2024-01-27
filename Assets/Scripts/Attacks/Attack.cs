using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int Damage = 1;
    public float KnockbackStrengthX = 1f;
    public float KnockbackStrengthY = 1f;
    public bool Attacking = false;
    private float _attackCoolDown = 0;

    Movement mv;

    // Start is called before the first frame update
    void Start()
    {
        mv = transform.parent.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        _attackCoolDown -= Time.deltaTime;
    }

    private void GetFatter() {
        // get object to scale 
        Transform objectTransform = this.transform.parent.transform;
        objectTransform.localScale = new Vector3(objectTransform.localScale.x * 1.1f, objectTransform.localScale.y * 1.1f, objectTransform.localScale.z * 1.1f);
    }

    private void OnTriggerStay(Collider collision) {
        if (_attackCoolDown > 0) return;
        if (Attacking && collision.gameObject.CompareTag("Player")) {
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();

            if (damageable != null) {
                Vector2 knockback = new Vector2(KnockbackStrengthX * mv.FacingDirection, KnockbackStrengthY);
                knockback *= 1 + (damageable.HP * 0.5f);
                damageable.Hit(Damage, knockback);
                Abilities ab = transform.parent.GetComponent<Abilities>();
                if (ab != null && ab.Punching) {
                    GetFatter();
                }
                _attackCoolDown = 0.3f;
                Debug.Log("Damage");
            }
        }
    }
}
