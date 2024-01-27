using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public float moveStrenght;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            myRigidbody.velocity = Vector3.left * moveStrenght;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            myRigidbody.velocity = Vector3.right * moveStrenght;
        }
    }
}
