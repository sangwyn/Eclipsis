using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField] private int _baseDamage;
    [SerializeField] private int _manaCost;
    [SerializeField] private int _coolDown;
    [SerializeField] private int _id;
    private bool _canCast = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canCast)
                StartCoroutine("CoolDown");
        }
    }

    int GetSpellDamage()
    {
        return _baseDamage + PlayerController.instance.GetIntelligence();
    }

    int GetManaCost()
    {
        return _manaCost;
    }

    void Cast()
    {
        if (!PlayerController.instance.GiveMana(-_manaCost))
            return;
        switch (_id)
        {
            case 1:
                break;
        }
    }

    IEnumerator CoolDown()
    {
        Cast();
        _canCast = false;
        yield return new WaitForSeconds(_coolDown);
        _canCast = true;
    }
}
