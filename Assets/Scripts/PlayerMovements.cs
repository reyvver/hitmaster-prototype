using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour
{
    public GameObject playerGameObject;
    public List<WayPoint> wayPointsList;

    private Queue<WayPoint> _wayPoints;
    private Player _player;
    private Transform _playerTransform;
    private Vector3 _destination;

    private void Awake()
    {
        _wayPoints = new Queue<WayPoint>();
        _player = new Player(playerGameObject);
        _playerTransform = playerGameObject.transform;
    }

    private void Start()
    {
        Level.Initialize();
        Level.Start.AddListener(ResetPlayerAndWayPoints);
        
        Level.Start.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MovePlayer();
        }
    }

    private void ResetPlayerAndWayPoints()
    {
        InitializeWayPointsQueue();
        
        WayPoint startPos = _wayPoints.Dequeue();
        _player.SetPlayerPosition(startPos.GetVector3());
    }

    private void InitializeWayPointsQueue()
    {
        foreach (var currentWayPoint in wayPointsList)
        {
            _wayPoints.Enqueue(currentWayPoint);
        }
    }

    private void MovePlayer()
    {
        WayPoint nextWayPoint = _wayPoints.Dequeue();
        _destination = nextWayPoint.GetVector3();

        _player.MovePlayerToNewPosition(_destination);
        StartCoroutine(WaitForWayPointReached());
    }

    private IEnumerator WaitForWayPointReached()
    {
        yield return new WaitUntil(WayPointReached);
        OnWayPoint();
    } 
    
    private bool WayPointReached()
    {
        float distance = _playerTransform.position.z - _destination.z;

        if (distance == 0)
            return true;
        
        return false;
    }

    private void OnWayPoint()
    {
        _player.StopPlayer();
        
        if (CheckIfFinish())
            print("finish");
            //Level.EndOfLevel.Invoke();
    }

    private bool CheckIfFinish()
    {
        if (_wayPoints.Count == 0)
            return true;

        return false;
    }

}
