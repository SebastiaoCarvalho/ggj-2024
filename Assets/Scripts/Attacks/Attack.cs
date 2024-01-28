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
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent == null) target = null; // projectile
        else if (transform.parent.name == "P1") target = GameObject.Find("P1 1").transform.GetChild(0).gameObject;
        else if (transform.parent.name == "P1 1") target = GameObject.Find("P1").transform.GetChild(0).gameObject;
        else {
            GameObject parent = transform.parent.transform.parent.gameObject;
            if (parent.name == "P1") target = GameObject.Find("P1 1").transform.GetChild(0).gameObject;
            else if (parent.name == "P1 1") target = GameObject.Find("P1").transform.GetChild(0).gameObject;
        }
        mv = transform.parent.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        _attackCoolDown -= Time.deltaTime;
        if (target == null) return;
        if (! GetComponent<Collider>().enabled) return;
        if (DetectColision()) {
            CollisionHandling(target.GetComponent<Collider>());
        }
    }

    private void GetFatter() {
        // get object to scale 
        Transform objectTransform = this.transform.parent.GetChild(1); // get ass
        objectTransform.localScale = new Vector3(objectTransform.localScale.x * 1.1f, objectTransform.localScale.y * 1.1f, objectTransform.localScale.z * 1.1f);
    }

    private void OnTriggerStay(Collider collision) {
        
    }

    private bool DetectColision() {
        return GetComponent<Collider>().bounds.Intersects(target.GetComponent<Collider>().bounds);
    }

    private void CollisionHandling(Collider collision) {
        if (target == null) return;
        /* if (!GetComponent<Animator>().GetBool("Punching")) return; */
        /* Debug.Log(collision.gameObject.CompareTag("Player")); */
        /* Debug.Log("here"); */
        if (Attacking && collision.gameObject.CompareTag("Player")) {
            Debug.Log("Collision");
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();

            if (damageable != null && damageable.IsDamageable) {
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
