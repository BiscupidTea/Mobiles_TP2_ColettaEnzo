using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class SingleTerrain : MonoBehaviour
{
    [Serializable]
    public class PositionsList
    {
        public Transform position;
        public Position tagPosition;
    }

    public Transform finalPoint;
    [SerializeField] private PositionsList[] positionList;
    private Dictionary<Position, Transform> PositionAndTransform = new Dictionary<Position, Transform>();

    private GameObject coin = null;

    private GameObject obstacle = null;
    [SerializeField] private float obstacleVelocity;
    private Patterns currentPaternObstacle;
    private int currentPosition = 0;

    public enum Position
    {
        Center,
        Up,
        Down,

        Right,
        RightUp,
        RightDown,

        Left,
        LeftUp,
        LeftDown
    }

    public enum Patterns
    {
        Tup,
        Tdown,
        Tright,
        Tleft,

        SQUAREupright,
        SQUAREupleft,
        SQUAREdownright,
        SQUAREdownleft,

        L1247,
        L3689,
        L1236,
        L4789,
        L1258,
        L2358,
        L2578,
        L2589,
    }

    private Dictionary<Patterns, List<Position>> ObstaclePatterns = new Dictionary<Patterns, List<Position>>
    {
        {
            Patterns.Tup,
            new List<Position> { Position.RightUp, Position.Up, Position.Center, Position.Up, Position.LeftUp }
        },
        {
            Patterns.Tdown,
            new List<Position> { Position.RightDown, Position.Down, Position.Center, Position.Down, Position.LeftDown }
        },
        {
            Patterns.Tright,
            new List<Position> { Position.RightUp, Position.Right, Position.Center, Position.Right, Position.LeftDown }
        },
        {
            Patterns.Tleft,
            new List<Position> { Position.LeftDown, Position.Left, Position.Center, Position.Left, Position.LeftUp }
        },

        { Patterns.SQUAREupright, new List<Position> { Position.RightUp, Position.Right, Position.Center, Position.Up } },
        { Patterns.SQUAREupleft, new List<Position> { Position.LeftUp, Position.LeftUp, Position.Center, Position.Up } },
        { Patterns.SQUAREdownright, new List<Position> { Position.RightDown, Position.Right, Position.Center, Position.Down } },
        { Patterns.SQUAREdownleft, new List<Position> { Position.LeftDown, Position.Left, Position.Center, Position.Down } },
        
        // { Patterns.Tup, new List<Position> { Position.RightUp, Position.Up, Position.LeftUp } },
        // { Patterns.Tup, new List<Position> { Position.RightUp, Position.Up, Position.LeftUp } },
        // { Patterns.Tup, new List<Position> { Position.RightUp, Position.Up, Position.LeftUp } },
        // { Patterns.Tup, new List<Position> { Position.RightUp, Position.Up, Position.LeftUp } },
        // { Patterns.Tup, new List<Position> { Position.RightUp, Position.Up, Position.LeftUp } },
        // { Patterns.Tup, new List<Position> { Position.RightUp, Position.Up, Position.LeftUp } },
        // { Patterns.Tup, new List<Position> { Position.RightUp, Position.Up, Position.LeftUp } },
        // { Patterns.Tup, new List<Position> { Position.RightUp, Position.Up, Position.LeftUp } },
    };

    private void Awake()
    {
        foreach (PositionsList list in positionList)
        {
            PositionAndTransform.Add(list.tagPosition, list.position);
        }
    }

    public void SpawnCoin(GameObject coinPrefab)
    {
        coin = Instantiate(coinPrefab, positionList[Random.Range(0, 5)].position.position, Quaternion.identity,
            transform);
    }

    public void SpawnObstacle(GameObject enemyPrefab)
    {
        obstacle = Instantiate(enemyPrefab, positionList[Random.Range(0, 5)].position.position, Quaternion.identity,
            transform);
        
        obstacle.GetComponent<Obstacle>().OnDestroyObstacle.AddListener(OnDestroyObstacle);

        SetObstaclePattern();
    }

    private void SetObstaclePattern()
    {
        currentPaternObstacle = (Patterns)Random.Range(0, ObstaclePatterns.Count);

        StartCoroutine(SetNewPositionToObstacle());
    }

    private void OnDestroyObstacle()
    {
        StopCoroutine(SetNewPositionToObstacle());
    }

    IEnumerator SetNewPositionToObstacle()
    {
        do
        {
            float timePass = 0;

            Vector3 startPoint =
                PositionAndTransform[ObstaclePatterns[currentPaternObstacle].ToArray()[currentPosition]]
                    .position;
            Vector3 endPoint =
                PositionAndTransform[ObstaclePatterns[currentPaternObstacle].ToArray()[currentPosition + 1]]
                    .position;

            while (timePass < obstacleVelocity)
            {
                obstacle.transform.position = Vector3.Lerp(startPoint, endPoint, timePass / obstacleVelocity);
                timePass += Time.deltaTime;
                yield return null;
            }

            currentPosition++;

            yield return null;
        } while (currentPosition < ObstaclePatterns[currentPaternObstacle].Count - 1);
    }
}