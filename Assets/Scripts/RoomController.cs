using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomController : MonoBehaviour
{
    public GameObject exits;
    public GameObject topExit;
    public GameObject downExit;
    public GameObject leftExit;
    public GameObject rightExit;

    public GameObject topSpawnPoint;
    public GameObject downSpawnPoint;
    public GameObject leftSpawnPoint;
    public GameObject rightSpawnPoint;

    public bool isExitRoom = false;
    public GameObject exitToNextLevel;

    public GameObject enemies;
    public GameObject[] enemyVariants;
    public List<GameObject> enemiesSpawnPoints = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        enemies.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.transform.childCount == 0)
        {
            exits.SetActive(true);
            if (isExitRoom)
            {
                exitToNextLevel.SetActive(true);
            }
        }
    }

    public void ActivateEnemies()
    {
        enemies.SetActive(true);
        if (enemies.transform.childCount != 0)
        {
            exits.SetActive(false);
        }

        for (int i = 0; i < enemies.transform.childCount; i++)
        {
            if (enemies.transform.GetChild(i).CompareTag("Enemy"))
            {
                enemies.transform.GetChild(i).GetComponent<Enemy>().isMoving = false;
            } else if (enemies.transform.GetChild(i).CompareTag("EnemyDistanceAttack"))
            {
                enemies.transform.GetChild(i).GetComponent<EnemyDistanceAttack>().isMoving = false;
            }
        }
    }

    public void MakeEnemiesMove()
    {
        Invoke("SetIsMovingTrue", 0.2f);
    }

    private void SetIsMovingTrue()
    {
        for (int i = 0; i < enemies.transform.childCount; i++)
        {
            if (enemies.transform.GetChild(i).CompareTag("Enemy"))
            {
                enemies.transform.GetChild(i).GetComponent<Enemy>().isMoving = true;
            } else if (enemies.transform.GetChild(i).CompareTag("EnemyDistanceAttack"))
            {
                enemies.transform.GetChild(i).GetComponent<EnemyDistanceAttack>().isMoving = true;
            }
        }
    }

    public void SetDoors()
    {
        
    }
}
