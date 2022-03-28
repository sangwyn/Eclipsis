using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public int id;
    [SerializeField] private int strBonus = 0;
    [SerializeField] private int dexBonus = 0;
    [SerializeField] private int intBonus = 0;

    public void AddBonus()
    {
        PlayerController.instance.GiveStrength(strBonus);
        PlayerController.instance.GiveDexterity(dexBonus);
        PlayerController.instance.GiveIntelligence(intBonus);
    }
    
    public void RemoveBonus()
    {
        PlayerController.instance.GiveStrength(-strBonus);
        PlayerController.instance.GiveDexterity(-dexBonus);
        PlayerController.instance.GiveIntelligence(-intBonus);
    }
}