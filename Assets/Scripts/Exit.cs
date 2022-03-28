using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private GameObject Player;
    
    public int i_change = 0;
    public int j_change = 0;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player.GetComponent<PlayerPosition>().i += i_change;
            // Player.GetComponent<PlayerPosition>().j += j_change;
            Player.GetComponent<PlayerPosition>().ChangeRoom(i_change, j_change);
            Player.GetComponent<PlayerController>().SetIsCanMove(false);
            //У игрока поменять координаты на i_change, и на j_change
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     print(2);
    //     if (other.CompareTag("Player"))
    //     {
    //         print(1);
    //         Player.GetComponent<PlayerPosition>().i += i_change;
    //         Player.GetComponent<PlayerPosition>().j += j_change;
    //         Player.GetComponent<PlayerPosition>().ChangeRoom();
    //         //У игрока поменять координаты на i_change, и на j_change
    //     }
    // }
}
