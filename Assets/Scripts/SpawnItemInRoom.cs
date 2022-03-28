using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemInRoom : MonoBehaviour
{

    public GameObject[] items;
    
    // Start is called before the first frame update
    void Start()
    {
        if (items.Length == 0)
        {
            return;
        }
        Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
