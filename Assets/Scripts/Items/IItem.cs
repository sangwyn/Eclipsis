using UnityEngine;

public interface IItem 
{
    string Name { get; }
    Sprite UIIcon { get; }
    GameObject ItemPrefab { get; }
}
