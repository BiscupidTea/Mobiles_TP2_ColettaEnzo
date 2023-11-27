using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualPlayerLife : MonoBehaviour, IObserver
{
    [SerializeField] private Image textureState1;
    [SerializeField] private Image textureState2;
    [SerializeField] private Image textureState3;

    [SerializeField] private GameObject Particles1;
    [SerializeField] private GameObject Particles2;
    private void Start()
    {
        PlayerManager player = FindAnyObjectByType<PlayerManager>();
        player.AddObserver(this);
    }

    public void UpdateObserver(int Life)
    {
        if (Life == 3)
        {
            textureState1.enabled = true;
            textureState2.enabled = true;
            textureState3.enabled = true;

            Particles1.SetActive(false);
            Particles1.SetActive(false);
        }
        else if (Life == 2)
        {
            textureState1.enabled = false;
            Particles1.SetActive(true);
        }
        else
        {
            textureState2.enabled = false;
            Particles2.SetActive(true);
        }
    }
}
