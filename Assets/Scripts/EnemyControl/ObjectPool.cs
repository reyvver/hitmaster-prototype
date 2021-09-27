using System.Collections.Generic;
using UnityEngine;

namespace EnemyControl
{
    public class ObjectPool : MonoBehaviour
    {
        // key - prefab name, value - queue of selected prefabs 
        private Dictionary<string, Queue<GameObject>> _objectPool = new Dictionary<string, Queue<GameObject>>();

        private GameObject CreateNewObject(GameObject prefab)
        {
            GameObject newObj = Instantiate(prefab);
            newObj.name = prefab.name;
            return newObj;
        }

        private Queue<GameObject> CreateNewQueue(GameObject prefab)
        {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            _objectPool.Add(prefab.name, newObjectQueue);
            return newObjectQueue;
        }

        public void ReturnObject(GameObject prefab)
        {
            if (_objectPool.TryGetValue(prefab.name, out Queue<GameObject> objectQueue))
            {
                objectQueue.Enqueue(prefab);
            }
            else
            {
                Queue<GameObject> newObjectQueue = CreateNewQueue(prefab);
                newObjectQueue.Enqueue(prefab);
            }
        }

        public void CreatStartQueue(GameObject prefab, Transform parent, int size)
        {
            Queue<GameObject> queue = CreateNewQueue(prefab);

            for (int i = 0; i < size; i++)
            {
                GameObject newObj = CreateNewObject(prefab);
                newObj.transform.SetParent(parent);
                newObj.transform.localPosition = Vector3.zero;
                newObj.SetActive(false);
            
                queue.Enqueue(newObj);
            }
        }
    
    
        public GameObject GetObject(GameObject prefab)
        {
            if (_objectPool.TryGetValue(prefab.name, out Queue<GameObject> objectQueue))
            {
                if (objectQueue.Count == 0)
                {
                    return CreateNewObject(prefab);
                }

                GameObject obj = objectQueue.Dequeue();
                return obj;
            }

            return CreateNewObject(prefab);
        }
    
    }
}

