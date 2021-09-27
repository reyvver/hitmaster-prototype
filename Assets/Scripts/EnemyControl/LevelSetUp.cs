using System.Collections.Generic;
using UnityEngine;

namespace EnemyControl
{
    public class LevelSetUp : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public List<EnemyGang> enemyGangs;
        
        private ObjectPool _objectPool;

        private void Awake()
        {
            Level.Initialize();
            Level.Start.AddListener(SpawnEnemies);
            Level.EndOfLevel.AddListener(ReturnEnemies);

            _objectPool = GetComponent<ObjectPool>();
        }

        private void Start()
        {
            Level.Start.Invoke();
        }

        private void SpawnEnemies()
        {
            foreach (EnemyGang enemyGang in enemyGangs)
            {
                foreach (var spawnPoint in enemyGang.spawnPoints)
                {
                    if (spawnPoint.isFree)
                    {
                        GameObject newEnemy = _objectPool.GetObject(enemyPrefab);
                        spawnPoint.PlaceEnemy(newEnemy);
                    }
                    
                }
            }
            
        }

        private void ReturnEnemies()
        {
            foreach (EnemyGang enemyGang in enemyGangs)
            {
                foreach (var spawnPoint in enemyGang.spawnPoints)
                {
                    if (!spawnPoint.isFree)
                    {
                        _objectPool.ReturnObject(spawnPoint.Enemy);
                        spawnPoint.Empty();
                    }
                }
            }
        }
    }
}