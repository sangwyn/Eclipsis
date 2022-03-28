using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _inventoryGO;

    public void OpenInventoryBtnPressed()
    {
        if (_inventoryGO.activeSelf)
        {
            for (int i = 0; i < _canvas.transform.childCount; ++i)
            {
                var child = _canvas.transform.GetChild(i);
                if (child.gameObject.name != "Panel")
                {
                    child.gameObject.SetActive(true);
                }
                if (child.gameObject.name == "ExitButton")
                {
                    child.gameObject.SetActive(false);
                }

                if (child.gameObject.name == "WeaponSwitchButton" && _player.GetComponent<PlayerController>().GetWeaponsCount() == 0)
                {
                    child.gameObject.SetActive(false);
                }
            }

            _inventoryGO.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            for (int i = 0; i < _canvas.transform.childCount; ++i)
            {
                var child = _canvas.transform.GetChild(i);
                if (!child.GetComponent<InventoryManager>())
                {
                    child.gameObject.SetActive(false);
                }
                if (child.gameObject.name == "ExitButton")
                {
                    child.gameObject.SetActive(true);
                }
            }

            _inventoryGO.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void EjectFromPool(IItem item)
    {
        GameObject itemGameObject = Instantiate(item.ItemPrefab);
        itemGameObject.transform.position = _player.transform.position;
        if (itemGameObject.GetComponent<Artifact>())
        {
            itemGameObject.GetComponent<Artifact>().RemoveBonus();
            Inventory.instance.artifactsInventory.Remove(item.ItemPrefab.GetComponent<Pickable>()._itemData);
        } else if (itemGameObject.GetComponent<Magic>())
        {
            Inventory.instance.magicInventory.Remove(item.ItemPrefab.GetComponent<Pickable>()._itemData);
        } else if (itemGameObject.GetComponent<Weapon>())
        {
            GameObject weapons = GameObject.Find("Weapons");
            for (int i = 0; i < weapons.transform.childCount; ++i)
            {
                var child = weapons.transform.GetChild(i);
                if (child.gameObject.name.Contains(item.Name + "(Clone)"))
                {
                    _player.GetComponent<PlayerController>().RemoveWeapon(i);
                    Destroy(child.gameObject);
                    break;
                }
            }
            
            Inventory.instance.weaponInventory.Remove(item.ItemPrefab.GetComponent<Pickable>()._itemData);
        }
        // анимация выброса
    }
}