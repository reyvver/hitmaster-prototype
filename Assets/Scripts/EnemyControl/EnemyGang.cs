using UnityEngine;

public class EnemyGang : MonoBehaviour
{
    public EnemySpawnPoint[] spawnPoints;
    [HideInInspector] public bool isKilled;

    private void Start()
    {
        Level.EnemyIsKilled.AddListener(CheckIfGangIsKilled);
        Level.Start.AddListener(OnStart);
    }

    private void OnStart()
    {
        isKilled = false;
    }

    private void CheckIfGangIsKilled()
    {
        int activeEnemies = 0;

        if (isKilled) return;

        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.IsEnemyAlive())
            {
                activeEnemies++;
            }
        }

        if (activeEnemies == 0)
        {
            Level.GangIsKilled.Invoke();
            isKilled = true;
        }
    }
}
