using UnityEngine;
using UnityEngine.AI;

namespace PlayerControl
{
    public class Player
    {
        private readonly NavMeshAgent _playerAgent;
        private readonly Animator _animator;
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        public Player(GameObject player)
        {
            _animator = player.GetComponent<Animator>();
            _playerAgent = player.GetComponent<NavMeshAgent>();
        }
    
        private bool IsMoving
        {
            set => _animator.SetBool(IsWalking, value);
        }
    
        public void MovePlayerToNewPosition(Vector3 newPosition)
        {
            _playerAgent.SetDestination(newPosition);
            IsMoving = true;
        }

        public void SetPlayerPosition(Vector3 newPosition)
        {
            if (_playerAgent.isOnNavMesh)
                _playerAgent.Warp(newPosition);
        }

        public void StopPlayer()
        {
            IsMoving = false;
        }
    

    }
}
