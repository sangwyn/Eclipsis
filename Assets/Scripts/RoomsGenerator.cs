using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class RoomsGenerator : MonoBehaviour
{
    public NavMeshSurface2d NavMeshSurface2d;
    
    public GameObject[] room_variants;
    public GameObject exit_room;
    public GameObject treasures_room;
    public GameObject start_room;
    
    public int rooms_in_dungeon = 10;
    public int level = 1;
    public int enemiesInRoom = 5;


    int n = 10;
    int m = 10;
    private Queue<Cell> queue = new Queue<Cell>();
    private int[,] map = new int[10, 10];
    private Dictionary<Cell, Cell[]> rooms = new Dictionary<Cell, Cell[]>();
    private Dictionary<Cell, GameObject> rooms_go = new Dictionary<Cell, GameObject>();

    public float room_x;
    public float room_y;

    int NeighborSum(Cell num)
    {
        if (map[num.i, num.j] != 0) return 4;
        int a = 0, b = 0, c = 0, d = 0;
        if (num.j - 1 >= 0)
        {
            if (map[num.i, num.j - 1] != 0)
            {
                a = 1;
            }
        }

        if (num.j + 1 < m)
        {
            if (map[num.i, num.j + 1] != 0)
            {
                b = 1;
            }
        }

        if (num.i + 1 < n)
        {
            if (map[num.i + 1, num.j] != 0)
            {
                c = 1;
            }
        }

        if (num.i - 1 >= 0)
        {
            if (map[num.i - 1, num.j] != 0)
            {
                d = 1;
            }
        }

        return a + b + c + d;
    }

    List<Cell> CheckNeighbors(Cell num)
    {
        List<Cell> ans = new List<Cell>();
        if (num.j - 1 >= 0 && NeighborSum(new Cell(num.i, num.j - 1)) < 2)
        {
            ans.Add(new Cell(num.i, num.j - 1));
        }

        if (num.j + 1 < m && NeighborSum(new Cell(num.i, num.j + 1)) < 2)
        {
            ans.Add(new Cell(num.i, num.j + 1));
        }

        if (num.i + 1 < n && NeighborSum(new Cell(num.i + 1, num.j)) < 2)
        {
            ans.Add(new Cell(num.i + 1, num.j));
        }

        if (num.i - 1 >= 0 && NeighborSum(new Cell(num.i - 1, num.j)) < 2)
        {
            ans.Add(new Cell(num.i - 1, num.j));
        }

        return ans;
    }

    void GenerateMap()
    {
        int cnt = 0;

        map[4, 5] = 6;
        if (!rooms.ContainsKey(new Cell(4, 5))) rooms.Add(new Cell(4, 5), new[] {new Cell(4, 5)});
        int random = Random.Range(1, 4);
        if (random == 3)
        {
            queue.Enqueue(new Cell(4, 6));
            queue.Enqueue(new Cell(4, 4));
            queue.Enqueue(new Cell(5, 5));
            map[4, 6] = 1;
            map[4, 4] = 1;
            map[5, 5] = 1;
            cnt = 3;
            if (!rooms.ContainsKey(new Cell(4, 6))) rooms.Add(new Cell(4, 6), new[] {new Cell(4, 6)});
            if (!rooms.ContainsKey(new Cell(4, 4))) rooms.Add(new Cell(4, 4), new[] {new Cell(4, 4)});
            if (!rooms.ContainsKey(new Cell(5, 5))) rooms.Add(new Cell(5, 5), new[] {new Cell(5, 5)});
        }
        else if (random == 2)
        {
            queue.Enqueue(new Cell(4, 6));
            queue.Enqueue(new Cell(4, 4));
            queue.Enqueue(new Cell(3, 5));
            map[4, 6] = 1;
            map[4, 4] = 1;
            map[3, 5] = 1;
            cnt = 3;
            if (!rooms.ContainsKey(new Cell(4, 6))) rooms.Add(new Cell(4, 6), new[] {new Cell(4, 6)});
            if (!rooms.ContainsKey(new Cell(4, 4))) rooms.Add(new Cell(4, 4), new[] {new Cell(4, 4)});
            if (!rooms.ContainsKey(new Cell(3, 5))) rooms.Add(new Cell(3, 5), new[] {new Cell(3, 5)});
        }
        // else if (random == 1)
        // {
        //     queue.Enqueue(new Cell(4, 6));
        //     queue.Enqueue(new Cell(3, 5));
        //     map[4, 6] = 1;
        //     map[3, 5] = 1;
        //     cnt = 2;
        // }
        else if (random == 1)
        {
            queue.Enqueue(new Cell(4, 6));
            queue.Enqueue(new Cell(4, 4));
            queue.Enqueue(new Cell(3, 5));
            queue.Enqueue(new Cell(5, 5));
            map[4, 6] = 1;
            map[4, 4] = 1;
            map[3, 5] = 1;
            map[5, 5] = 1;
            cnt = 4;
            if (!rooms.ContainsKey(new Cell(4, 6))) rooms.Add(new Cell(4, 6), new[] {new Cell(4, 6)});
            if (!rooms.ContainsKey(new Cell(4, 4))) rooms.Add(new Cell(4, 4), new[] {new Cell(4, 4)});
            if (!rooms.ContainsKey(new Cell(5, 5))) rooms.Add(new Cell(5, 5), new[] {new Cell(5, 5)});
            if (!rooms.ContainsKey(new Cell(3, 5))) rooms.Add(new Cell(3, 5), new[] {new Cell(3, 5)});
        }

        while (queue.Count > 0)
        {
            if (cnt >= rooms_in_dungeon)
            {
                break;
            }

            var current_room = queue.Peek();
            queue.Dequeue();
            List<Cell> variants = CheckNeighbors(current_room);
            if (variants.Count == 0) continue;

            random = Random.Range(0, 2);
            if (random == 1)
            {
                // обычная комната, но с двумя выходами
                random = Random.Range(0, variants.Count);
                map[variants[random].i, variants[random].j] = 1;
                ++cnt;
                if (!rooms.ContainsKey(variants[random]))
                    rooms.Add(variants[random], new[] {variants[random]});
                queue.Enqueue(variants[random]);
                variants.RemoveAt(random);

                if (variants.Count == 0) continue;

                random = Random.Range(0, variants.Count);
                map[variants[random].i, variants[random].j] = 1;
                if (!rooms.ContainsKey(variants[random]))
                    rooms.Add(variants[random], new[] {variants[random]});
                ++cnt;
                queue.Enqueue(variants[random]);
            }
            else
            {
                random = Random.Range(0, variants.Count);
                map[variants[random].i, variants[random].j] = 1;
                if (!rooms.ContainsKey(variants[random]))
                    rooms.Add(variants[random], new[] {variants[random]});
                ++cnt;
                queue.Enqueue(variants[random]);
            }
        }
        
        List<Cell> candidates = new List<Cell>();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (map[i, j] != 0)
                {
                    int value = map[i, j];
                    map[i, j] = 0;
                    if (NeighborSum(new Cell(i, j)) == 1)
                    {
                        candidates.Add(new Cell(i, j));   
                    }
                    map[i, j] = value;
                }
            }
        }
        int ind = Random.Range(0, candidates.Count);
        map[candidates[ind].i, candidates[ind].j] = 7;
        candidates.RemoveAt(ind);
        ind = Random.Range(0, candidates.Count);
        map[candidates[ind].i, candidates[ind].j] = 8;
        candidates.RemoveAt(ind);

        // if (cnt < rooms_in_dungeon || !Check())
        // {
        //     map = new int[10, 10];
        //     while (queue.Count != 0) queue.Dequeue();
        //     rooms = new Dictionary<Cell, Cell[]>();
        //     GenerateMap();
        // }
    }


    // Start is called before the first frame update
    void Awake()
    {
        GenerateNewLevel();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int[,] GetMap()
    {
        return map;
    }

    public Dictionary<Cell, Cell[]> GetRoomsDictionary()
    {
        return rooms;
    }

    public GameObject GetRoom(int i, int j)
    {
        return map[i, j] switch
        {
            6 => start_room,
            7 => exit_room,
            8 => treasures_room,
            _ => room_variants[Random.Range(0, room_variants.Length)]
        };
        // var key = new Cell(i, j);
        // print(key.i + " " + key.j);
        // // foreach (var r in rooms.Keys)
        // // {
        // //     print(r.i + " " + r.j);
        // // }
        //
        // var cells = rooms[key];
        // return cells.Length switch
        // {
        //     1 => room,
        //     2 when cells[0].i == cells[1].i => longRoomHorizontal,
        //     2 => longRoomVert,
        //     3 => Lroom,
        //     _ => bigRoom
        // };
    }

    public Dictionary<Cell, GameObject> Get_rooms_go()
    {
        return rooms_go;
    }

    public void Clear()
    {
        foreach (var room in rooms_go)
        {
            Destroy(room.Value);
        }

        rooms = new Dictionary<Cell, Cell[]>();
        rooms_go = new Dictionary<Cell, GameObject>();
        map = new int[n, m];
        queue = new Queue<Cell>();
    }

    public void GenerateNewLevel()
    {
        GenerateMap();
        for (int i = 0; i < n; i++)
        {
            var str = "";
            for (int j = 0; j < m; j++)
            {
                str += map[i, j].ToString();
            }

            Debug.Log(str);
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (map[i, j] != 0)
                {
                    var roomPos = transform.position;

                    roomPos.x = room_x * j * 3;
                    roomPos.y = -room_y * i * 3;
                    roomPos.z = 0;
                    var currentRoom = Instantiate(GetRoom(i, j), roomPos, Quaternion.identity, transform);
                    rooms_go.Add(new Cell(i, j), currentRoom);
                    var roomController = currentRoom.GetComponent<RoomController>();

                    if (i - 1 >= 0 && map[i - 1, j] != 0)
                    {
                        roomController.topExit.SetActive(true);
                        roomController.topExit.GetComponent<Exit>().i_change = -1;
                    }

                    if (i + 1 < n && map[i + 1, j] != 0)
                    {
                        roomController.downExit.SetActive(true);
                        roomController.downExit.GetComponent<Exit>().i_change = 1;
                    }

                    if (j - 1 >= 0 && map[i, j - 1] != 0)
                    {
                        roomController.leftExit.SetActive(true);
                        roomController.leftExit.GetComponent<Exit>().j_change = -1;
                    }

                    if (j + 1 < n && map[i, j + 1] != 0)
                    {
                        roomController.rightExit.SetActive(true);
                        roomController.rightExit.GetComponent<Exit>().j_change = 1;
                    }

                    if (map[i, j] == 1)
                    {
                        for (int k = 0; k < enemiesInRoom && roomController.enemiesSpawnPoints.Count != 0; k++)
                        {
                            int enemySpawnPointIndex = Random.Range(0, roomController.enemiesSpawnPoints.Count);
                            var enemy = roomController.enemyVariants[
                                Random.Range(0, roomController.enemyVariants.Length)];
                            var spawnPoint = roomController.enemiesSpawnPoints[enemySpawnPointIndex];
                            roomController.enemiesSpawnPoints.RemoveAt(enemySpawnPointIndex);
                            var spawnPosition = spawnPoint.transform.position;
                            Instantiate(enemy, spawnPosition, Quaternion.identity, roomController.enemies.transform);
                            Destroy(spawnPoint);
                        }
                    }
                    if (map[i, j] == 7)
                    {
                        int enemySpawnPointIndex = Random.Range(0, roomController.enemiesSpawnPoints.Count);
                        var enemy = roomController.enemyVariants[
                            Random.Range(0, roomController.enemyVariants.Length)];
                        var spawnPoint = roomController.enemiesSpawnPoints[enemySpawnPointIndex];
                        roomController.enemiesSpawnPoints.RemoveAt(enemySpawnPointIndex);
                        var spawnPosition = spawnPoint.transform.position;
                        Instantiate(enemy, spawnPosition, Quaternion.identity, roomController.enemies.transform);
                        Destroy(spawnPoint);
                    }
                }
            }
        }
        NavMeshSurface2d.BuildNavMesh();
        //GetComponent<NavMeshSurface2d>().BuildNavMesh();
    }
}