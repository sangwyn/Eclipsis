using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance = null;
    public List<AssetItem> artifactsInventory;
    public List<AssetItem> magicInventory;
    public List<AssetItem> weaponInventory;
    [SerializeField] private InventoryCell _inventoryCellTemplate;
    private Transform _artifactsContainer;
    private Transform _magicContainer;
    private Transform _weaponContainer;
    private Transform _draggingParent;
    private InventoryManager _ejector;

    private PlayerController playerController;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        _artifactsContainer = GameObject.Find("ArtifactGrid").transform;
        _magicContainer = GameObject.Find("MagicGrid").transform;
        _weaponContainer = GameObject.Find("WeaponGrid").transform;
        _draggingParent = GameObject.Find("Canvas").transform;
        _ejector = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        instance = this;
        DontDestroyOnLoad(this);
        GameObject.Find("InventoryGO").SetActive(false);
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void OnEnable()
    {
        Render(artifactsInventory, _artifactsContainer);
        Render(magicInventory, _magicContainer);
        Render(weaponInventory, _weaponContainer);
    }

    public void Render(List<AssetItem> items, Transform container)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        items.ForEach(item =>
        {
            var cell = Instantiate(_inventoryCellTemplate, container);
            cell.Init(_draggingParent);
            cell.Render(item);
            cell.Ejecting += () => Destroy(cell.gameObject);
            cell.Ejecting += () => _ejector.EjectFromPool(item);
        });
    }

    public bool AddArtifact(AssetItem newItem)
    {
        if (!artifactsInventory.Contains(newItem))
        {
            newItem.ItemPrefab.GetComponent<Artifact>().AddBonus();
            artifactsInventory.Add(newItem);
            Render(artifactsInventory, _artifactsContainer);
            return true;
        }

        return false;
    }

    public bool AddMagic(AssetItem newItem)
    {
        if (!magicInventory.Contains(newItem))
        {
            magicInventory.Add(newItem);
            Render(magicInventory, _magicContainer);
            return true;
        }

        return false;
    }

    public bool AddWeapon(AssetItem newItem)
    {
        if (!weaponInventory.Contains(newItem) && weaponInventory.Count < 2)
        {
            GameObject itemGameObject = Instantiate(newItem.ItemPrefab, GameObject.Find("Weapons").transform);
    //        itemGameObject.transform.SetPositionAndRotation(Vector3.zero, new Quaternion(0, 0, 0, 0));
            itemGameObject.GetComponent<WeaponAiming>()._joystick = playerController.GetAttackJoystick();
            itemGameObject.GetComponent<ShootingWeapon>()._joystick = playerController.GetAttackJoystick();
            playerController.GunTransform = itemGameObject.transform;
            playerController.SwitchWeapon();
            weaponInventory.Add(newItem);
            Render(weaponInventory, _weaponContainer);
            return true;
        }

        return false;
    }
}