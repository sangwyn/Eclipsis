using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _baseDamage;
    [SerializeField] private bool _strengthScale;

    public int GetWeaponDamage()
    {
        if (_strengthScale)
            return _baseDamage + PlayerController.instance.GetStrength() / 2;
        return _baseDamage + PlayerController.instance.GetDexterity() / 2;
    }
}