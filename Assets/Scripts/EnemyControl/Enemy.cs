using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace EnemyControl
{
    public class Enemy : MonoBehaviour
    {
        public GameObject enemyModel;
        public Slider healthBar;
        public Rigidbody[] allRigidbodies;

        private NavMeshAgent _enemyAgent;
        private Animator _animator;


        public float _healthPoints;
        [HideInInspector] public bool _isAlive;
    
        private void Awake()
        {
            // enemy set up
            _animator = enemyModel.GetComponent<Animator>();
            _enemyAgent = enemyModel.GetComponent<NavMeshAgent>();
            _isAlive = true;
        
            // health slider set up
            _healthPoints = EnemyProperties.StartHealthPoints;
            healthBar.maxValue = _healthPoints;
            healthBar.value = _healthPoints;
        }

        private void Start()
        {
            foreach (var currentRigidBody in allRigidbodies)
            {
                currentRigidBody.isKinematic = true;
            }

        }

        private void Update()
        {
            if (!_isAlive) return;

            if (_healthPoints <= 0 || Input.GetKeyDown(KeyCode.Escape))
                EnemyIsKilled();
        }

        public void SetEnemyPosition(Vector3 newPosition, Quaternion newRotation)
        {
            if (_enemyAgent.isOnNavMesh)
            {
                transform.position = newPosition;
                transform.rotation = newRotation;
            }
        }

        public void Reset()
        {
            EnableOrDisableEnemy(true);
        
            _isAlive = true;
            _healthPoints = EnemyProperties.StartHealthPoints;
            healthBar.value = _healthPoints;
        }

        private void EnemyIsKilled()
        {
            _isAlive = false;
            ActivateRagdollPhysics();

            Level.EnemyIsKilled.Invoke();
        }
    
        private void ActivateRagdollPhysics()
        {
            foreach (var currentRigidBody in allRigidbodies)
            {
                currentRigidBody.isKinematic = false;
                currentRigidBody.velocity = Vector3.zero;
                currentRigidBody.angularVelocity = Vector3.zero;
            }
        
            EnableOrDisableEnemy(false);
        }


        private void EnableOrDisableEnemy(bool value)
        {
            _animator.enabled = value;
            _enemyAgent.enabled = value;
            healthBar.gameObject.SetActive(value);
        }
    
    
        private void OnTriggerEnter(Collider other)
        {
            GameObject obj = other.gameObject;
        
            if (obj.CompareTag("Bullet"))
            {
                _healthPoints -= EnemyProperties.BulletDamage;
                healthBar.value = _healthPoints;
            }
        }
    }
}
