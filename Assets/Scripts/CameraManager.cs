using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] _players = new GameObject[2];
    double _startHeight;
    double _startDiagonal;
    double _maxZ = -7;
    
    void Start()
    {
        //_players[0] = GameObject.Find("Player1");
        _players[0] = GameObject.Find("Player1").transform.GetChild(0).gameObject;
        //_players[1] = GameObject.Find("Player2");
        _players[1] = GameObject.Find("Player2").transform.GetChild(0).gameObject;
        Vector3 midpoint = (_players[0].transform.position + _players[1].transform.position) / 2;
        _startDiagonal = Math.Sqrt(Math.Pow(_players[0].transform.position.x - midpoint.x, 2) + Math.Pow(_players[0].transform.position.y - midpoint.y, 2));
        _startHeight = midpoint.z - this.gameObject.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Camera needs to follow 2 players
        // Find the midpoint between the 2 players
        // Set the camera's position to the midpoint
        
        Vector3 midpoint = (_players[0].transform.position + _players[1].transform.position) / 2;

        //Move z to keep the camera with the same ratio
        double diagonal = Math.Sqrt(Math.Pow(_players[0].transform.position.x - midpoint.x, 2) + Math.Pow(_players[0].transform.position.y - midpoint.y, 2));
        double height = diagonal * _startHeight / _startDiagonal;
        if (midpoint.z - (float) height > _maxZ)
            return;
        Debug.Log(midpoint.z - (float) height);
        this.gameObject.transform.position = new Vector3(midpoint.x, midpoint.y, midpoint.z - (float) height);
        Debug.Log("Camera position: " + this.gameObject.transform.position);
    }
}
