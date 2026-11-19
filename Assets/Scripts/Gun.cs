using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private WeaponSO[] weapons;
    [SerializeField] private Transform ShootPoint;
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private FireButton fireButton;

    public event Action<int> WeaponChanged;

    public int CurrentIndex
    {
        get { return currentIndex; }
    }

    public int WeaponCount
    {
        get { return weapons.Length; }
    }

    private GameObject[] weaponModels;
    private int currentIndex;
    private WeaponSO weaponSo;
    private float timer;

    private void Start()
    {
        weaponModels = new GameObject[weapons.Length];

        for (int i = 0; i < weapons.Length; i++)
        {
            GameObject model = Instantiate(weapons[i].viewGun, weaponPoint);
            model.SetActive(false);
            weaponModels[i] = model;
        }

        SwitchWeapon(0);
    }

    private void Update()
    {
        HandleFire();
        weaponSo.Tick(ShootPoint, Time.deltaTime);
    }

    public void SwitchWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length)
        {
            return;
        }

        weaponModels[currentIndex].SetActive(false);
        currentIndex = index;
        weaponSo = weapons[currentIndex];
        weaponModels[currentIndex].SetActive(true);

        if (WeaponChanged != null)
        {
            WeaponChanged(currentIndex);
        }
    }

    public string GetWeaponName(int index)
    {
        return weapons[index].name;
    }

    private void HandleFire()
    {
        timer += Time.deltaTime;
        float interval = 1f / weaponSo.fireRate;

        bool pressed = fireButton.ConsumePress();
        bool isFire = weaponSo.isAutomatic ? fireButton.IsHeld : pressed;

        if (timer >= interval && isFire)
        {
            weaponSo.ExecuteFire(ShootPoint);
            timer = 0f;
        }
    }

    public string GetCurrentWeaponId()
    {
        return weaponSo.name;
    }

    public void SetWeaponById(string weaponId)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].name == weaponId)
            {
                SwitchWeapon(i);
                return;
            }
        }
    }
}
