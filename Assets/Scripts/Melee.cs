using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private int _trailSize;

    void Attack()
    {
        Quaternion x = PlayerController.instance.gameObject.transform.rotation;
        GameObject trail = new GameObject("Attack");
       // trail.
    }
}
