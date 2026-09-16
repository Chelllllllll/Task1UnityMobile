using UnityEngine;


[CreateAssetMenu(fileName = "MultiShotWeaponSO", menuName = "Weapons/MultiShotWeaponSO")]
public class MultiShotWeaponSO : WeaponSO
{
    public int bulletCount = 3;
    public float delayBetweenShots = 0.08f;

    private int pendingShots;
    private float nextShotTime;
    
    public override void ExecuteFire(Transform muzzle)
    { 
        if(pendingShots > 0) return;
        SpawnBullet(muzzle.position,  muzzle.rotation);
        pendingShots = bulletCount - 1;
        nextShotTime = delayBetweenShots;
    }

    public override void Tick(Transform muzzle, float deltaTime)
    {
        if (pendingShots <= 0) return;
        nextShotTime -= deltaTime;
        if (nextShotTime <= 0)
        {
            SpawnBullet(muzzle.position, muzzle.rotation);
            pendingShots --;
            nextShotTime = delayBetweenShots;
        }
    }
}
