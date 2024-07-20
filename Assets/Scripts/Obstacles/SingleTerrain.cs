using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;
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
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private float timePass = 0;

    [SerializeField] private PositionsList[] positionList;
    private Dictionary<Position, Transform> PositionAndTransform = new Dictionary<Position, Transform>();
    
    public float obstacleSpeed;
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

        {
            Patterns.SQUAREupright,
            new List<Position> { Position.RightUp, Position.Right, Position.Center, Position.Up }
        },
        {
            Patterns.SQUAREupleft, new List<Position> { Position.LeftUp, Position.LeftUp, Position.Center, Position.Up }
        },
        {
            Patterns.SQUAREdownright,
            new List<Position> { Position.RightDown, Position.Right, Position.Center, Position.Down }
        },
        {
            Patterns.SQUAREdownleft,
            new List<Position> { Position.LeftDown, Position.Left, Position.Center, Position.Down }
        },
    };

    private void Awake()
    {
        foreach (PositionsList list in positionList)
        {
            PositionAndTransform.Add(list.tagPosition, list.position);
        }
    }

    public void SpawnCoin()
    {
        coinPrefab.SetActive(true);
        coinPrefab.GetComponent<BoxCollider>().enabled = true;
        coinPrefab.transform.position = positionList[Random.Range(0, 5)].position.position;
    }

    public void SpawnObstacle()
    {
        obstaclePrefab.SetActive(true);
        currentPaternObstacle = (Patterns)Random.Range(0, ObstaclePatterns.Count);
        obstaclePrefab.transform.position = PositionAndTransform[ObstaclePatterns[currentPaternObstacle].ToArray()[0]].position;
        currentPosition = 0;
        
        Position newPosition1 = ObstaclePatterns[currentPaternObstacle].ToArray()[currentPosition];
        Position newPosition2 = ObstaclePatterns[currentPaternObstacle].ToArray()[currentPosition + 1];
        
        startPoint = PositionAndTransform[newPosition1]
            .position;
        endPoint = PositionAndTransform[newPosition2]
            .position;
    }

    private void OnDisable()
    {
        obstaclePrefab.SetActive(false);
        coinPrefab.SetActive(false);
    }

    private void Update()
    {
        if (obstaclePrefab)
        {
            obstaclePrefab.transform.position = Vector3.Lerp(startPoint, endPoint, timePass / obstacleSpeed);
            timePass += Time.deltaTime;

            if (obstaclePrefab.transform.position == endPoint)
            {
                currentPosition++;

                if (currentPosition >= ObstaclePatterns[currentPaternObstacle].Count)
                {
                    currentPosition = 0;
                }
                
                int nextPosition = currentPosition + 1;
                
                if (nextPosition >= ObstaclePatterns[currentPaternObstacle].Count)
                {
                    nextPosition = 0;
                }
                
                Position newPosition1 = ObstaclePatterns[currentPaternObstacle].ToArray()[currentPosition];
                Position newPosition2 = ObstaclePatterns[currentPaternObstacle].ToArray()[nextPosition];
        
                startPoint = PositionAndTransform[newPosition1]
                    .position;
                endPoint = PositionAndTransform[newPosition2]
                    .position;
                
                timePass = 0;
            }
        }
    }
}