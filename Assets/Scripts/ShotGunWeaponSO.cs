using UnityEngine;

[CreateAssetMenu(fileName = "ShotGunWeaponSO", menuName = "Weapons/ShotGunWeaponSO")]
public class ShotGunWeaponSO : WeaponSO
{
    public int bulletCount = 6;
    public float bulletsAngel = 15f;

    public override void ExecuteFire(Transform muzzle)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angel = Random.Range(-bulletsAngel, bulletsAngel);
            Quaternion rotation = muzzle.rotation * Quaternion.Euler(0f, angel, 0f);
            SpawnBullet(muzzle.position, rotation);
        }
    }
}
