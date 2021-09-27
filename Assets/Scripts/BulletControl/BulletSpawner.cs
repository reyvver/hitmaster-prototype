using System.Collections.Generic;
using UnityEngine;

namespace BulletControl
{
    public class BulletSpawner : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public Transform bulletsContainer;
        public Camera mainCamera;
        public int startBulletNumber;
        
        private bool _isSpawnApproved;
        private List<GameObject> _bullets;
        
        private void Start()
        {
            Level.PlayerOnThePosition.AddListener(SpawnApproved);
            Level.PlayerOffThePosition.AddListener(SpawnDisabled);
            Level.GangIsKilled.AddListener(SpawnDisabled);

            CreateStartPool();
        }

        private void Update()
        {
            if (!_isSpawnApproved) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    transform.position = raycastHit.point;
                    ShootBullet(transform.position);
                }
            }
        }

        private void CreateStartPool()
        {
            _bullets = new List<GameObject>();
            
            for (int i = 0; i < startBulletNumber; i++)
            {
                GameObject newBullet = Instantiate(bulletPrefab);
                newBullet.transform.SetParent(bulletsContainer);
                newBullet.SetActive(false);
                
                _bullets.Add(newBullet);
            }
        }

        private GameObject GetObjectFromPool()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (!_bullets[i].activeInHierarchy)
                    return _bullets[i];
            }
            return null;
        }
        
        private void ShootBullet(Vector3 position)
        {
            GameObject newBullet = GetObjectFromPool();
            newBullet.transform.SetParent(bulletsContainer);
            newBullet.SetActive(true);

            newBullet.transform.position = position;
        }

        private void SpawnApproved()
        {
            _isSpawnApproved = true;
        }
        
        private void SpawnDisabled()
        {
            _isSpawnApproved = false;
        }


    }
}
