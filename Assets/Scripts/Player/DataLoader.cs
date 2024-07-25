using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLoader : MonoBehaviour
{
    public static DataLoader Instance;
    [SerializeField] private PlayerSo player;
    [SerializeField] private List<SpaceShipsSo> spaceShips;

    private string PlayerDataPath;
    private string SpaceShipDataPath;

    private bool isSaving;

    private void Awake()
    {
        PlayerDataPath = Application.persistentDataPath + "/playerData.json";
        SpaceShipDataPath = Application.persistentDataPath + "/spaceShipsData.json";
    }

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadData();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void LoadData()
    {
        if (File.Exists(PlayerDataPath))
        {
            string jsonData = File.ReadAllText(PlayerDataPath);
            PlayerDataSerializable data = JsonUtility.FromJson<PlayerDataSerializable>(jsonData);

            PlayerSo playerData = ScriptableObject.CreateInstance<PlayerSo>();
            data.CopyTo(playerData);
            
            Debug.Log("Player data load successfully.");
        }
        else
        {
            Debug.LogWarning("Player data file not found, Set base value.");
            SetDefaultPlayerValues();
        }

        if (File.Exists(SpaceShipDataPath))
        {
            string jsonData = File.ReadAllText(SpaceShipDataPath);
            SpaceShipsListWrapper wrapper = JsonUtility.FromJson<SpaceShipsListWrapper>(jsonData);
            spaceShips = wrapper.spaceShipsList;
            Debug.Log("SpaceShips data load successfully.");
        }
        else
        {
            Debug.LogWarning("SpaceShips data file not found, Set base value.");
            SetDefaultSpaceshipValues();
        }
    }

    public void SaveData()
    {
        PlayerDataSerializable data = new PlayerDataSerializable(player);
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(PlayerDataPath, jsonData);

        jsonData = JsonUtility.ToJson(new SpaceShipsListWrapper(spaceShips));
        File.WriteAllText(SpaceShipDataPath, jsonData);
    }

    public SpaceShipsSo GetCurrentPlayerSpaceShip()
    {
        foreach (SpaceShipsSo ship in spaceShips)
        {
            if (ship.bought && ship.equipped)
            {
                return ship;
            }
        }

        Debug.Log("Fail to pick spaceship, return base");
        return spaceShips.ToArray()[0];
    }

    private void SetDefaultPlayerValues()
    {
        player.distance = 0;
        player.maxDistance = 0;

        player.moneyToCharge = 0;
        player.totalMoney = 0;

        player.totalLives = 3;
    }

    private void SetDefaultSpaceshipValues()
    {
        foreach (SpaceShipsSo ship in spaceShips)
        {
            ship.bought = false;
            ship.equipped = false;
        }

        spaceShips.ToArray()[0].bought = true;
        spaceShips.ToArray()[0].equipped = true;
    }

    [System.Serializable]
    private class SpaceShipsListWrapper
    {
        public List<SpaceShipsSo> spaceShipsList;

        public SpaceShipsListWrapper(List<SpaceShipsSo> spaceShipsList)
        {
            this.spaceShipsList = spaceShipsList;
        }
    }
    
    [System.Serializable]
    private class PlayerDataSerializable
    {
        public int totalLives;
        public int totalMoney;
        public int moneyToCharge;
        public float distance;
        public float maxDistance;

        public PlayerDataSerializable(PlayerSo playerData)
        {
            totalLives = playerData.totalLives;
            totalMoney = playerData.totalMoney;
            moneyToCharge = playerData.moneyToCharge;
            distance = playerData.distance;
            maxDistance = playerData.maxDistance;
        }

        public void CopyTo(PlayerSo playerData)
        {
            playerData.totalLives = totalLives;
            playerData.totalMoney = totalMoney;
            playerData.moneyToCharge = moneyToCharge;
            playerData.distance = distance;
            playerData.maxDistance = maxDistance;
        }
    }
}