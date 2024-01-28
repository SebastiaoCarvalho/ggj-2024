using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject[] _players = new GameObject[2];
    GameObject[] _floor;
    
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private GameObject _endScreen1;
    AudioManager audioManager;

    // Start is called before the first frame update
    public void Awake()
    {
        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        _players[0] = GameObject.Find("Player1").transform.GetChild(0).gameObject;
        _players[1] = GameObject.Find("Player2").transform.GetChild(0).gameObject;
        _floor = GameObject.FindGameObjectsWithTag("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject player in _players) {
            Player playerScript = player.GetComponent<Player>();
            if (playerScript.OutOfBounds()) {
                if (playerScript.Lives > 1)
                    playerScript.Respawn();
                else {
                    if (player == _players[0]) {
                        _endScreen.SetActive(true);
                        Debug.Log("Game Over for Player 1");
                    }
                    else if (player == _players[1]) {
                        _endScreen1.SetActive(true);
                        Debug.Log("Game Over for Player 2");
                    }
                }
            }
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Attack");
        foreach (GameObject projectile in projectiles) {
            float width = 17.5f;
            float startX = _floor[0].transform.position.x - width;
            float endX = _floor[0].transform.position.x + width;
            if (projectile.transform.position.x < startX || projectile.transform.position.x >  endX)
                Destroy(projectile);
        }
        }
    }
}