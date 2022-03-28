using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class WeaponAiming : MonoBehaviour
{
    public FloatingJoystick _joystick;
    [SerializeField] private GameObject _crosshair;
    private SpriteRenderer sprite;
    private GameObject player;

    private void Awake()
    {
        sprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (!_joystick)
            return;
        if (_joystick.Direction != Vector2.zero)
        {
            // поворачиваем ствол по направлению стика, при условии, что он зажат
            float angle = Vector2.SignedAngle(Vector2.right, _joystick.Direction);
            if (Math.Abs(player.transform.localScale.x - (-1)) < 0.1)
            {
                angle += 180;
            }
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (Math.Abs(player.transform.localScale.x - (-1)) < 0.1)
            {
                if (angle >= 270 && angle <= 360 || angle <= 90 && angle >= 0)
                    sprite.flipY = false;
                else
                    sprite.flipY = true;
            }
            else
            {
                if (angle >= 90 && angle <= 180 || angle <= -90 && angle >= -180)
                    sprite.flipY = true;
                else
                    sprite.flipY = false;
            }

            _crosshair.SetActive(true);
        }
        else
        {
            // прицел не виден, если стик не зажат
            _crosshair.SetActive(false);
        }
    }
}