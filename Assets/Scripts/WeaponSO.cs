using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Weapons/SingleShotSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject viewGun;
    public BulletSO bulletData;
    public float fireRate = 4f;
    public bool isAutomatic;

    public virtual void ExecuteFire(Transform muzzle)
    {
        SpawnBullet(muzzle.position,  muzzle.rotation);
    }
    
    public virtual void Tick(Transform muzzle, float deltaTime){}

    protected void SpawnBullet(Vector3 pos, Quaternion rot)
    {
        GameObject bulletObj = GameObjectPool.instance.GetPooledObject(bulletData.prefab);
        bulletObj.transform.position = pos;
        bulletObj.transform.rotation = rot;
        
        Bullet bullet =  bulletObj.GetComponent<Bullet>();
        bullet.data = bulletData;
        
        bulletObj.SetActive(true);
    }

    
}
