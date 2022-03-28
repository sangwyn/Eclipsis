using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpPlayer : MonoBehaviour
{
    private GameObject target;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (target == null)
            return;
        transform.position = target.transform.position;
    }
}
