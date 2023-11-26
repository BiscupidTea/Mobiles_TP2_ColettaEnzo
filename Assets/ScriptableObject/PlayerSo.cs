using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Create Player")]
public class PlayerSo : ScriptableObject
{
    public int totalLives;
    public float totalMoney;
    public float distance;
    public float maxDistance;
    public GameObject SelectedSpaceShip;
}
