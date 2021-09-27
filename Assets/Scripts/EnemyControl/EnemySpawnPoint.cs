using UnityEngine;

namespace EnemyControl
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [HideInInspector] public bool isFree;
        private Transform _spawnPoint;
        private MeshRenderer _visibleRenderer;
    
        private GameObject _enemy;
        private Enemy _enemyController;

        public GameObject Enemy => _enemy;

        private void Awake()
        {
            _spawnPoint = GetComponent<Transform>();
            _visibleRenderer = GetComponent<MeshRenderer>();
            isFree = true;
            HideSpawnPoint();
        }

        private void HideSpawnPoint()
        {
            _visibleRenderer.enabled = false;
        }

        public void PlaceEnemy(GameObject enemyGameObject)
        {
            isFree = false;
            _enemy = enemyGameObject;
            _enemyController = _enemy.GetComponent<Enemy>();
        
            _enemy.transform.SetParent(_spawnPoint);
            _enemyController.SetEnemyPosition(_spawnPoint.position, _spawnPoint.rotation);
        }
    
        public void Empty()
        {
            isFree = true;
            _enemyController.Reset();
        
            _enemy = null;
            _enemyController = null;
        }

        public bool IsEnemyAlive()
        {
            return _enemyController._isAlive;
        }

    }
}
