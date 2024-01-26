using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _hp = 10f;
    private float _damage = 1f;

    public float Damage {
        get { return _damage; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            _hp -= other.gameObject.GetComponent<Player>().Damage;
            Debug.Log("Player HP: " + _hp);
        }
    }
}
