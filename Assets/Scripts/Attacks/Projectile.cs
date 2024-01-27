using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Attack
{
    public Vector3 Direction = Vector3.right;
    public float Speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Speed * Time.deltaTime * Direction);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Player Triggered");
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<Player>().TakeDamage(Damage);
            Destroy(this.gameObject);
        }
    }
}
