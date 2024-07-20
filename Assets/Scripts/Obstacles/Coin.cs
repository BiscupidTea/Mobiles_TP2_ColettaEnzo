using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioSource picksound;
    [SerializeField] private ParticleSystem _particleSystem;

    public void DestroyObstacle()
    {
        picksound.Play();
        _particleSystem.Play();
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}