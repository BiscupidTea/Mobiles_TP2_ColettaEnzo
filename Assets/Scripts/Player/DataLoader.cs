using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public static DataLoader Instance;
    [SerializeField] private PlayerSo player;
    [SerializeField] private List<SpaceShipsSo> spaceShips;

    private string PlayerDataPath;
    private readonly string fileName = "PlayerData";

    private void Awake()
    {
        PlayerDataPath = Application.persistentDataPath + "/" + fileName + ".json";
    }

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadProgress();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SaveProgress()
    {
        SaveLocalData();

        Debug.Log("Saved Progress: " + player.totalMoney + " - " + player.maxDistance);
    }

    public void LoadProgress()
    {
        ((int, float), List<SpaceShipPackage>) data = LoadLocalData();

        player.totalMoney = data.Item1.Item1;
        player.maxDistance = data.Item1.Item2;
        
        for (int i = 0; i < data.Item2.Count; i++)
        {
            spaceShips[i].bought = data.Item2[i].bought;
            spaceShips[i].equipped = data.Item2[i].equipped;

            if (spaceShips[i].equipped)
            {
                player.pickedSpaceShip = spaceShips[i].Name;
            }
        }
    }

    // private ((int, float), List<SpaceShipPackage>) LoadCloudData()
    // {
    //     (int, float) CloudPlayerData = (0,0);
    //     List<SpaceShipPackage> CloudShipsData = new List<SpaceShipPackage>();
    //     SetDefaultSpaceshipValues(CloudShipsData);
    //
    //     ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
    //         fileName,
    //         DataSource.ReadNetworkOnly,
    //         ConflictResolutionStrategy.UseLongestPlaytime,
    //         (status, metadata) =>
    //         {
    //             if (status != SavedGameRequestStatus.Success)
    //             {
    //                 Debug.LogError("Error opening saved game");
    //                 CloudPlayerData.Item1 = 0;
    //                 CloudPlayerData.Item2 = 0;
    //             }
    //             else
    //             {
    //                 ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(
    //                     metadata,
    //                     (readStatus, savedData) =>
    //                     {
    //                         if (readStatus != SavedGameRequestStatus.Success)
    //                         {
    //                             Debug.LogError("Error reading saved game data");
    //
    //                             CloudPlayerData.Item1 = 0;
    //                             CloudPlayerData.Item2 = 0;
    //                         }
    //                         else
    //                         {
    //                             string jsonString = Encoding.ASCII.GetString(savedData);
    //                             CompleteDataPackage data = JsonUtility.FromJson<CompleteDataPackage>(jsonString);
    //
    //                             CloudPlayerData.Item1 = data.totalMoney;
    //                             CloudPlayerData.Item2 = data.maxDistance;
    //
    //                             for (int i = 0; i < data.GetSpaceShipsData().Count; i++)
    //                             {
    //                                 CloudShipsData[i].bought = data.GetSpaceShipsData()[i].bought;
    //                                 CloudShipsData[i].equipped = data.GetSpaceShipsData()[i].equipped;
    //                             }
    //                         }
    //                     });
    //             }
    //         });
    //
    //
    //     return (CloudPlayerData, CloudShipsData);
    // }
    //
    // private void SaveCloudData()
    // {
    //     if (!Social.localUser.authenticated)
    //     {
    //         Debug.LogWarning("User is not authenticated to Google Play Services");
    //         return;
    //     }
    //
    //     ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
    //         fileName,
    //         DataSource.ReadCacheOrNetwork,
    //         ConflictResolutionStrategy.UseMostRecentlySaved,
    //         (status, metadata) =>
    //         {
    //             if (status != SavedGameRequestStatus.Success)
    //             {
    //                 Debug.LogError("Error opening saved game");
    //                 return;
    //             }
    //
    //             CompleteDataPackage data = new CompleteDataPackage(player, spaceShips);
    //
    //             string jsonString = JsonUtility.ToJson(data);
    //             byte[] savedData = Encoding.ASCII.GetBytes(jsonString);
    //
    //             SavedGameMetadataUpdate updatedMetadata = new SavedGameMetadataUpdate.Builder()
    //                 .WithUpdatedDescription("My Save File Description")
    //                 .Build();
    //
    //             ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(
    //                 metadata,
    //                 updatedMetadata,
    //                 savedData,
    //                 (commitStatus, _) =>
    //                 {
    //                     string debugText = commitStatus == SavedGameRequestStatus.Success
    //                         ? "Data saved successfully"
    //                         : "Error saving data";
    //
    //                     Debug.Log(debugText);
    //                 });
    //         });
    // }

    private ((int, float), List<SpaceShipPackage>) LoadLocalData()
    {
        (int, float) localPlayerData;
        List<SpaceShipPackage> localShipsSData = new List<SpaceShipPackage>();
        foreach (SpaceShipsSo ship in spaceShips)
        {
            localShipsSData.Add(new SpaceShipPackage(ship.bought, ship.equipped));
        }
        
        SetDefaultSpaceshipValues(localShipsSData);

        if (File.Exists(PlayerDataPath))
        {
            string jsonData = File.ReadAllText(PlayerDataPath);
            CompleteDataPackage data = JsonUtility.FromJson<CompleteDataPackage>(jsonData);

            localPlayerData.Item1 = data.totalMoney;
            localPlayerData.Item2 = data.maxDistance;

            for (int i = 0; i < data.spaceShipsList.Count; i++)
            {
                localShipsSData[i].bought = data.spaceShipsList[i].bought;
                localShipsSData[i].equipped = data.spaceShipsList[i].equipped;
            }

            Debug.Log("Player data load successfully.");
        }
        else
        {
            localPlayerData.Item1 = 0;
            localPlayerData.Item2 = 0;

            Debug.LogWarning("Player data file not found, Set base value.");
        }

        return (localPlayerData, localShipsSData);
    }

    private void SaveLocalData()
    {
        CompleteDataPackage dataPackage = new CompleteDataPackage(player, spaceShips);
        string jsonData = JsonUtility.ToJson(dataPackage);

        File.WriteAllText(PlayerDataPath, jsonData);
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
        return spaceShips[0];
    }

    private void SetDefaultPlayerValues(PlayerSo playerSo)
    {
        player.distance = 0;
        player.maxDistance = 0;

        player.moneyToCharge = 0;
        player.totalMoney = 0;

        player.totalLives = 3;
    }

    private void SetDefaultSpaceshipValues(List<SpaceShipPackage> shipList)
    {
        for (int i = 0; i < shipList.Count; i++)
        {
            shipList[i].bought = false;
            shipList[i].equipped = false;
        }

        shipList[0].bought = true;
        shipList[0].equipped = true;
    }

    [Serializable]
    private class CompleteDataPackage
    {
        public int totalMoney;
        public float maxDistance;

        public List<SpaceShipPackage> spaceShipsList = new List<SpaceShipPackage>();

        public CompleteDataPackage(PlayerSo playerData, List<SpaceShipsSo> spaceShipsData)
        {
            totalMoney = playerData.totalMoney;
            maxDistance = playerData.maxDistance;

            foreach (SpaceShipsSo ships in spaceShipsData)
            {
                spaceShipsList.Add(new SpaceShipPackage(ships.bought, ships.equipped));
            }
        }
    }

    [Serializable]
    private class SpaceShipPackage
    {
        public bool bought;
        public bool equipped;

        public SpaceShipPackage(bool bought, bool equipped)
        {
            this.bought = bought;
            this.equipped = equipped;
        }
    }
}