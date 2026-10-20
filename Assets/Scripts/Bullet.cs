using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObjectPool gameObjectPool;
    public GameObject prefab;
    public BulletSO data;
    
    private Vector3 startPos;

    private void OnEnable()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if(data == null) return;
        
        float distance = Vector3.Distance(transform.position, startPos);
        if (distance >= data.maxDistance)
        {
            gameObjectPool.ReturnToPool(prefab,  gameObject);
            return;
        }
        transform.Translate(Vector3.forward * (data.speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.isTrigger) return;
        
        Target target = other.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(data.damage);
        }
        gameObjectPool.ReturnToPool(prefab, gameObject);
    }
}
