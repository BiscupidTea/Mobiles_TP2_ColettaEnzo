using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeathMediator : MonoBehaviour, IMediator
{
    private PlayerManager player;
    [SerializeField] private UnityEvent finishGame;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerManager>();
        player.mediator = this;
    }

    public void NotifyPlayerDeath()
    {
        finishGame.Invoke();
    }
}
