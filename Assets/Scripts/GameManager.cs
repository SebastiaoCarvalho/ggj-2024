using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject[] _players = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        _players[0] = GameObject.Find("Player1");
        _players[1] = GameObject.Find("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
