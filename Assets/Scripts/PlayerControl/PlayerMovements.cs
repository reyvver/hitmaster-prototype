using System.Collections;
using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerMovements : MonoBehaviour
    {
        public CinemachineVirtualCamera virtualCamera;
        public GameObject playerPrefab;
        public List<WayPoint> wayPointsList;

        private Queue<WayPoint> _wayPoints;
        private Vector3 _destination;

        private GameObject _playerGameObject;
        private Player _player;
        private Transform _playerTransform;
        private bool _isCurrentGangKilled = true;

        private void Awake()
        {
            _wayPoints = new Queue<WayPoint>();
            
            Level.Initialize();
            
            Level.Start.AddListener(ResetPlayerAndWayPoints);
            Level.GangIsKilled.AddListener(MovementToNextWayPointApproved);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _isCurrentGangKilled)
            {
                MovePlayer();
            }
        }

        private void ResetPlayerAndWayPoints()
        {
            InitializeWayPointsQueue();
            SetPlayer();
        }

        private void InitializeWayPointsQueue()
        {
            foreach (var currentWayPoint in wayPointsList)
            {
                _wayPoints.Enqueue(currentWayPoint);
            }
        }

        private void SetPlayer()
        {
            if (_playerGameObject == null)
            {
                CreatePLayer();
            }
            
            virtualCamera.Follow = _playerTransform;
            virtualCamera.LookAt = _playerTransform;

            _isCurrentGangKilled = true;
            
            WayPoint startPos = _wayPoints.Dequeue();
            _player.SetPlayerPosition(startPos.GetVector3());
        }

        private void CreatePLayer()
        {
            _playerGameObject = Instantiate(playerPrefab);
            _playerGameObject.name = playerPrefab.name;
            
            _player = new Player(_playerGameObject);
            _playerTransform = _playerGameObject.transform;
        }

        private void MovePlayer()
        {
            WayPoint nextWayPoint = _wayPoints.Dequeue();
            _destination = nextWayPoint.GetVector3();

            _isCurrentGangKilled = false;
            Level.PlayerOffThePosition.Invoke();

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
            float distance = Vector3.Distance(_playerTransform.position, _destination);
            
            if (Math.Abs(distance) - 0.8f <= 0.15f)
                return true;
        
            return false;
        }

        private void OnWayPoint()
        {
            _player.StopPlayer();
            _isCurrentGangKilled = false;
            Level.PlayerOnThePosition.Invoke();

            if (CheckIfFinish())
            { 
                Level.EndOfLevel.Invoke();
                // for more complex - Restart event
                Level.Start.Invoke();
            }
        }

        private bool CheckIfFinish()
        {
            if (_wayPoints.Count == 0)
                return true;

            return false;
        }

        private void MovementToNextWayPointApproved()
        {
            _isCurrentGangKilled = true;
        }

    }
}
