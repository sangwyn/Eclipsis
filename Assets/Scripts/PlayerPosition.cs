using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerPosition : MonoBehaviour
{
    private GameObject current_room;
    private RoomController current_room_controller;
    public GameObject player;
    
    public int i = 4;
    public int j = 5;

    private int n = 10;

    public RoomsGenerator RoomsGenerator;
    private int[,] map = new int[10, 10];
    Dictionary<Cell, Cell[]> rooms = new Dictionary<Cell, Cell[]>();
    private Dictionary<Cell, GameObject> rooms_go = new Dictionary<Cell, GameObject>();

    private GameObject prev_room = null;

    //private Cell prev_cell = new Cell(4, 5);

    private float room_x;
    private float room_y;

    private bool isDark = false;
    
    public Image imgPanel;
    public GameObject panel;
    
    public AudioSource SwitchSound;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        transform.position = new Vector3(room_x * 5 * 3, -room_y * 4 * 3, 0);
        //ChangeRoom(0, 0);
    }

    public void Init()
    {
        map = RoomsGenerator.GetMap();
        rooms = RoomsGenerator.GetRoomsDictionary();
        rooms_go = RoomsGenerator.Get_rooms_go();
        room_x = RoomsGenerator.room_x;
        room_y = RoomsGenerator.room_y;
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void ChangeRoom(int i_diff, int j_diff)
    {
        //prev_cell.i = i;
        //prev_cell.j = j;
        i += i_diff;
        j += j_diff;
        var currentRoom = rooms_go[new Cell(i, j)];
        var roomController = currentRoom.GetComponent<RoomController>();
        current_room_controller = roomController;
        //var spawnPoints = currentRoom.transform.GetChild(1);
        Vector3 spawnPosition = new Vector3(room_x * j * 3, -room_y * i * 3, 0);
        // Transform topSpawnPoint = new RectTransform(), downSpawnPoint = new RectTransform(),
        //          leftSpawnPoint = new RectTransform(), rightSpawnPoint = new RectTransform();
        // for (int k = 0; k < spawnPoints.childCount; k++)
        // {
        //     var spawnPoint = spawnPoints.GetChild(k);
        //     if (spawnPoint.CompareTag("TopSpawnPoint"))
        //     {
        //         topSpawnPoint = spawnPoint;
        //     }
        //     if (spawnPoint.CompareTag("DownSpawnPoint"))
        //     {
        //         downSpawnPoint = spawnPoint;
        //     }
        //     if (spawnPoint.CompareTag("LeftSpawnPoint"))
        //     {
        //         leftSpawnPoint = spawnPoint;
        //     }
        //     if (spawnPoint.CompareTag("RightSpawnPoint"))
        //     {
        //         rightSpawnPoint = spawnPoint;
        //     }
        // }
        if (i_diff == -1)
        {
            // spawnPosition = downSpawnPoint.transform.position;
            spawnPosition = roomController.downSpawnPoint.transform.position;
        }
        if (i_diff == 1)
        {
            // spawnPosition = topSpawnPoint.transform.position;
            spawnPosition = roomController.topSpawnPoint.transform.position;
        }
        if (j_diff == -1)
        {
            // spawnPosition = rightSpawnPoint.transform.position;
            spawnPosition = roomController.rightSpawnPoint.transform.position;
        }
        if (j_diff == 1)
        {
            // spawnPosition = leftSpawnPoint.transform.position;
            spawnPosition = roomController.leftSpawnPoint.transform.position;
        }
        // player.transform.position = new Vector3(19f * j, -8.5f * i, 0);
        //player.transform.position = spawnPosition;
        StartCoroutine(Blackout(spawnPosition));
    }

    IEnumerator Blackout(Vector3 newPosition)
    {
        SwitchSound.Play();
        
        isDark = false;
        panel.SetActive(true);
        float time = 0;
        var camera = GameObject.Find("Main Camera");
        while (Math.Abs(imgPanel.color.a) > Time.deltaTime || !isDark)
        {
            if (Math.Abs(imgPanel.color.a - 1) < Time.deltaTime)
            {
                isDark = true;
                time += Time.deltaTime;
                player.transform.position = newPosition;
                var cameraPosition = newPosition;
                cameraPosition.z = -10; 
                camera.transform.position = cameraPosition;
            }

            if (!isDark)
            {
                imgPanel.color = new Color(imgPanel.color.r, imgPanel.color.g, imgPanel.color.b, imgPanel.color.a + Time.deltaTime);
            }
            else if (time > 0.5)
            {
                current_room_controller.ActivateEnemies();
                imgPanel.color = new Color(imgPanel.color.r, imgPanel.color.g, imgPanel.color.b, imgPanel.color.a - Time.deltaTime);
            }

            yield return null;
            
        }
        isDark = false;
        panel.SetActive(false);
        current_room_controller.MakeEnemiesMove();
        player.GetComponent<PlayerController>().SetIsCanMove(true);
    }
}
