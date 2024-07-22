using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpaceShip", menuName = "Create SpaceShip")]
[Serializable]
public class SpaceShipsSo : ScriptableObject
{
    public int ID;
    public GameObject prefab;
    public string Name;
    public int Price;
    public bool bought;
    public bool equipped;
}
