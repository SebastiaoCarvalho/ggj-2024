using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _hp = 0f;
    private int _lives = 3;

    public float HP {
        get { return _hp; }
    }

    public int Lives {
        get { return _lives; }
    }
    private Vector3 _startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage) {
        _hp += damage;
        Debug.Log("Player HP: " + _hp);
    }

    public bool OutOfBounds() {
        return transform.position.y < -10;
    }

    public void Respawn() {
        _lives--;
        transform.position = _startPosition;
        _hp = 0f;
    }

}
