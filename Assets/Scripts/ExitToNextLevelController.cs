using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToNextLevelController : MonoBehaviour
{
    private GameObject RoomGenerator;

    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        RoomGenerator = GameObject.Find("RoomGenerator");
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
            var script = RoomGenerator.GetComponent<RoomsGenerator>();
            script.Clear();
            script.GenerateNewLevel();
            var playerPosition = Player.GetComponent<PlayerPosition>();
            playerPosition.Init();
            playerPosition.ChangeRoom(4 - playerPosition.i, 5 - playerPosition.j);
        }
    }
}
