using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponAiming : MonoBehaviour
{
    [SerializeField] private Transform target;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (target == null)
            return;
        float angle = Vector2.SignedAngle(Vector2.right, target.position - transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (angle >= 90 && angle <= 180 || angle <= -90 && angle >= -180)
            sprite.flipY = true;
        else 
            sprite.flipY = false;
    }
}
