using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _hp = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Attack")) {
            _hp -= other.gameObject.GetComponent<Attack>().Damage;
            Debug.Log("Player HP: " + _hp);
        }
    }
}
