using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpaceShip", menuName = "Create SpaceShip")]
public class SpaceShipsSo : ScriptableObject
{
    public GameObject prefab;
    public string Name;
    public int Price;
    public bool bought;
    public bool equipped;
}
