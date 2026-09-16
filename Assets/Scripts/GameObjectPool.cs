using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameObjectPool : MonoBehaviour
{
    public static GameObjectPool instance;

    Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();

    void Awake()
    {
        instance = this;
    }
    
    void InitPool(GameObject prefab, int poolSize)
    {
        Queue<GameObject> pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = InitObject(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
        pools[prefab] = pool;
    }

    GameObject InitObject(GameObject prefab)
    {
        GameObject gameObject = Instantiate(prefab);
        Bullet bullet = gameObject.GetComponent<Bullet>();
        bullet.gameObjectPool = this;
        bullet.prefab = prefab;
        return gameObject;
    }

    public GameObject GetPooledObject(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
        {
            InitPool(prefab, 10);
        }
        
        Queue<GameObject> pool = pools[prefab];
        if (pool.Count > 0)
        {
            return  pool.Dequeue();
        }
        else
        {
            return  InitObject(prefab);
        }
    }

    public void ReturnToPool(GameObject prefab, GameObject obj)
    {
        pools[prefab].Enqueue(obj);
        obj.SetActive(false);
    }
    
}
    