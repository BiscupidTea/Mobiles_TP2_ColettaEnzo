using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    public UnityEvent OnDestroyObstacle;
    public void DestroyObstacle()
    {
        OnDestroyObstacle.Invoke();
        gameObject.SetActive(false);
    }
}
