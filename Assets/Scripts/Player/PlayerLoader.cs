using Unity.VisualScripting;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    [SerializeField] private PlayerSo player;
    [SerializeField] private SpaceShipsSo[] SpaceshipSo;
    private AchivementController achivement;

    private void Start()
    {

        if (!PlayerPrefs.HasKey("IsLoaded"))
        {
            PlayerPrefs.SetInt("IsLoaded", 0);
        }

        if (PlayerPrefs.GetInt("IsLoaded") == 0)
        {

            player.SelectedSpaceShip = null;
            player.distance = 0;
            player.maxDistance = 0;
            player.totalMoney = 0;
            player.totalLives = 3;

            LoadMoney();
            LoadMaxDistance();
            LoadSpaceShips();
            PlayerPrefs.SetInt("IsLoaded", 1);
            achivement.FirstTime();
        }

        if (!Application.isPlaying)
        {
            PlayerPrefs.SetInt("IsLoaded", 0);
        }

    }

    public void LoadMoney()
    {
        //Load Money
        if (PlayerPrefs.HasKey("money"))
        {
            player.totalMoney = PlayerPrefs.GetFloat("money");
        }
        else
        {
            player.totalMoney = 0;
            PlayerPrefs.SetFloat("money", 0);
        }
    }

    public void LoadMaxDistance()
    {
        //Load Money
        if (PlayerPrefs.HasKey("distance"))
        {
            player.maxDistance = PlayerPrefs.GetFloat("distance");
        }
        else
        {
            player.maxDistance = 0;
            PlayerPrefs.SetFloat("distance", 0);
        }
    }

    public void LoadSpaceShips()
    {
        bool isEquipedSomeSpaceShip = false;

        for (int i = 0; i < SpaceshipSo.Length; i++)
        {
            if (PlayerPrefs.HasKey(SpaceshipSo[i].Name))
            {
                if (PlayerPrefs.GetInt(SpaceshipSo[i].Name) == 1)
                {
                    //no buy
                    SpaceshipSo[i].bought = false;
                    SpaceshipSo[i].equipped = false;
                }
                else if (PlayerPrefs.GetInt(SpaceshipSo[i].Name) == 2)
                {
                    //buy
                    SpaceshipSo[i].bought = true;
                    SpaceshipSo[i].equipped = false;
                }
                else if (PlayerPrefs.GetInt(SpaceshipSo[i].Name) == 3)
                {
                    //equipped
                    SpaceshipSo[i].bought = true;
                    SpaceshipSo[i].equipped = true;
                    player.SelectedSpaceShip = SpaceshipSo[i].prefab;
                    isEquipedSomeSpaceShip = true;
                }
            }
            else
            {
                SpaceshipSo[i].bought = false;
                SpaceshipSo[i].equipped = false;
                PlayerPrefs.GetInt(SpaceshipSo[i].Name, 1);
            }
        }

        SpaceshipSo[0].bought = true;
        if (!isEquipedSomeSpaceShip)
        {
            SpaceshipSo[0].equipped = true;
            player.SelectedSpaceShip = SpaceshipSo[0].prefab;
        }
        PlayerPrefs.GetInt(SpaceshipSo[0].Name, 3);
    }
}
