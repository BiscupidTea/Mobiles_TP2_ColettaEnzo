using System;
using System.Collections.Generic;
using System.Text;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public static DataLoader Instance;
    [SerializeField] private PlayerSo player;
    [SerializeField] private List<SpaceShipsSo> spaceShips;

    private bool isSaving;
    
    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            OpenSave(false);
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void ResetPlayerValues()
    {
        player.distance = 0;
        player.maxDistance = 0;

        player.moneyToCharge = 0;
        player.totalMoney = 0;

        player.totalLives = 3;

        foreach (SpaceShipsSo ship in spaceShips)
        {
            ship.bought = false;
            ship.equipped = false;
        }

        spaceShips.ToArray()[0].bought = true;
    }

    public void OpenSave(bool saving)
    {
        if (Social.localUser.authenticated)
        {
            isSaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("SaveFile",
                DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpen);
        }
    }

    private void SaveGameOpen(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (isSaving)
            {
                byte[] myData = ASCIIEncoding.ASCII.GetBytes(GetSaveString());

                SavedGameMetadataUpdate UpdateMetaData = new SavedGameMetadataUpdate.Builder()
                    .WithUpdatedDescription("Update data at: " + DateTime.Now.ToString()).Build();

                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, UpdateMetaData, myData, SaveCallback);
            }
            else
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, LoadCallBackData);
            }
        }
        else
        {
            ResetPlayerValues();
        }
    }

    private void LoadCallBackData(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            string LoadedData = ASCIIEncoding.ASCII.GetString(data);

            LoadSaveString(LoadedData);
        }
    }

    private void LoadSaveString(string loadedData)
    {
        string[] cloudStringData = loadedData.Split('|');

        int totalMoney = int.Parse(cloudStringData[0]);
        float maxDistance = float.Parse(cloudStringData[1]);

        string[] shipsData = cloudStringData[2].Split(';');
        foreach (string shipData in shipsData)
        {
            if (string.IsNullOrWhiteSpace(shipData)) continue;

            string[] shipParts = shipData.Split(',');

            int id = int.Parse(shipParts[0]);
            bool bought = shipParts[1] == "1";
            bool equipped = shipParts[2] == "1";

            SpaceShipsSo ship = spaceShips.Find(s => s.ID == id);
            if (ship != null)
            {
                ship.bought = bought;
                ship.equipped = equipped;
            }
        }
    }

    private string GetSaveString()
    {
        string dataToSave = "";

        dataToSave += player.totalMoney;
        dataToSave += "|";
        dataToSave += player.maxDistance;
        dataToSave += "|";

        foreach (SpaceShipsSo ship in spaceShips)
        {
            dataToSave += ship.ID;
            dataToSave += ",";
            dataToSave += ship.bought ? "1" : "0";
            dataToSave += ",";
            dataToSave += ship.equipped ? "1" : "0";
            dataToSave += ";";
        }

        return dataToSave;
    }

    private void SaveCallback(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
        }
        else
        {
        }
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
}