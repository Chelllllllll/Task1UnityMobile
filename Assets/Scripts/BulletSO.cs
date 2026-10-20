using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO", menuName = "Weapons/BulletSO")]
public class BulletSO : ScriptableObject
{
    public GameObject prefab;
    public float speed = 10f;
    public float maxDistance = 20f;
    public int damage = 10;
}
