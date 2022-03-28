using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public AssetItem _itemData;

    public void PickUp()
    {
        Debug.Log("Rneteww");
        if (GetComponent<Artifact>() && Inventory.instance.AddArtifact(_itemData))
        {
            Debug.Log("art");
            Destroy(this.gameObject);
        }
        else if (GetComponent<Magic>() && Inventory.instance.AddMagic(_itemData))
        {
            Debug.Log("mag");
            Destroy(this.gameObject);
        }
        else if (GetComponent<Weapon>() && Inventory.instance.AddWeapon(_itemData))
        {
            Debug.Log("weap");
            Destroy(this.gameObject);
        }
    }
}
