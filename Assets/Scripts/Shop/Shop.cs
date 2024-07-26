using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private SpaceShipsSo[] SpaceshipSo;
    private GameObject[] gameSpaceship;

    [SerializeField] private PlayerSo PlayerSo;

    [SerializeField] private Transform parent;

    [SerializeField] private Text name;
    [SerializeField] private Text price;
    [SerializeField] private Text totalMoney;

    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject boughtButton;
    [SerializeField] private Text boughtButtonText;

    [SerializeField] private float rotationSpeed;

    private int selectedShip = 0;

    private void Start()
    {
        gameSpaceship = new GameObject[SpaceshipSo.Length];

        for (int i = 0; i < SpaceshipSo.Length; i++)
        {
            GameObject spawnNew =
                Instantiate(SpaceshipSo[i].prefab.gameObject, parent.position, parent.rotation, parent);
            spawnNew.SetActive(false);
            gameSpaceship[i] = spawnNew;
        }

        gameSpaceship[0].SetActive(true);

        name.text = SpaceshipSo[selectedShip].Name;
        price.text = SpaceshipSo[selectedShip].Price.ToString() + "$";

        bool hasSpaceship = false;
        foreach (SpaceShipsSo shipsSo in SpaceshipSo)
        {
            if (shipsSo.equipped)
            {
                hasSpaceship = true;
            }
        }

        if (!hasSpaceship)
        {
            SpaceshipSo[0].equipped = true;
        }
    }

    private void Update()
    {
        for (int i = 0; i < SpaceshipSo.Length; i++)
        {
            gameSpaceship[i].transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        SetVisibleButton();
        totalMoney.text = PlayerSo.totalMoney.ToString() + "$";
    }

    public void SelectNextSpaceShip()
    {
        gameSpaceship[selectedShip].SetActive(false);
        if (selectedShip + 1 <= gameSpaceship.Length - 1)
        {
            selectedShip++;
        }
        else
        {
            selectedShip = 0;
        }

        gameSpaceship[selectedShip].SetActive(true);

        name.text = SpaceshipSo[selectedShip].Name;
        price.text = SpaceshipSo[selectedShip].Price.ToString() + "$";
    }

    public void SelectPreviousSpaceShip()
    {
        gameSpaceship[selectedShip].SetActive(false);
        if (selectedShip - 1 >= 0)
        {
            selectedShip--;
        }
        else
        {
            selectedShip = gameSpaceship.Length - 1;
        }

        gameSpaceship[selectedShip].SetActive(true);

        name.text = SpaceshipSo[selectedShip].Name;
        price.text = SpaceshipSo[selectedShip].Price.ToString() + "$";
    }

    public void BuySpaceShip()
    {
        if (SpaceshipSo[selectedShip].Price <= PlayerSo.totalMoney)
        {
            PlayerSo.totalMoney -= SpaceshipSo[selectedShip].Price;
            SpaceshipSo[selectedShip].bought = true;
            AchivementController.Instance.BuyShip();

            DataLoader.Instance.SaveProgress();
            AnalyticsManager.Instance.HandleReportPurchase(SpaceshipSo[selectedShip].Name, SpaceshipSo[selectedShip].Price);
        }
    }

    public void EquippedSpaceShip()
    {
        for (int i = 0; i < SpaceshipSo.Length; i++)
        {
            if (SpaceshipSo[i].equipped)
            {
                SpaceshipSo[i].equipped = false;
            }
        }

        if (SpaceshipSo[selectedShip].bought)
        {
            SpaceshipSo[selectedShip].equipped = true;
        }

        DataLoader.Instance.SaveProgress();
        PlayerSo.pickedSpaceShip = SpaceshipSo[selectedShip].Name;
    }

    public void SetVisibleButton()
    {
        if (SpaceshipSo[selectedShip].bought)
        {
            buyButton.SetActive(false);
            boughtButton.SetActive(true);

            if (SpaceshipSo[selectedShip].equipped)
            {
                boughtButtonText.text = "Equipped";
            }
            else
            {
                boughtButtonText.text = "Use";
            }
        }
        else
        {
            buyButton.SetActive(true);
            boughtButton.SetActive(false);
        }
    }
}