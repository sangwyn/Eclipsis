using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class CameraFollowing : MonoBehaviour
{
    [FormerlySerializedAs("lookAt")] [Header("Set in Inspector")]
    public Transform target;

    public Vector3 offset;
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        transform.position = target.transform.position;
    }

    private void Update()
    {
        if (target == null)
            return;
        var desiredPosition = target.position + offset;
        var smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity , smoothTime);
        transform.position = smoothedPosition;
 //       transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
