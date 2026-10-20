using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
   [SerializeField] private WeaponSO[] weapons;
   [SerializeField] private Transform ShootPoint;
   [SerializeField] private Transform weaponPoint;

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
      HandleWeaponSwitch();
      HandleFire();
      weaponSo.Tick(ShootPoint, Time.deltaTime);
   }

   private void HandleWeaponSwitch()
   {
      if (Keyboard.current.digit1Key.wasPressedThisFrame)
      {
         weaponSo = weapons[0];
         SwitchWeapon(0);
      }

      if (Keyboard.current.digit2Key.wasPressedThisFrame)
      {
         weaponSo  = weapons[1];
         SwitchWeapon(1);
      }

      if (Keyboard.current.digit3Key.wasPressedThisFrame)
      {
         weaponSo  = weapons[2];
         SwitchWeapon(2);
      }

      if (Keyboard.current.digit4Key.wasPressedThisFrame)
      {
         weaponSo  = weapons[3];
         SwitchWeapon(3);
      }

   }

   private void SwitchWeapon(int index)
   {
      weaponModels[currentIndex].SetActive(false);
      currentIndex = index;
      weaponSo = weapons[currentIndex];
      weaponModels[currentIndex].SetActive(true);
   }
   
   private void HandleFire()
   {
      timer += Time.deltaTime;
      float interval = 1f / weaponSo.fireRate;

      Debug.Log($"Weapon: {weaponSo.name}, fireRate: {weaponSo.fireRate}, interval: {interval}, timer: {timer}");
      
      bool isFire;
      if (weaponSo.isAutomatic)
      {
         isFire = Input.GetKey(KeyCode.Mouse0);
      }
      else
      {
         isFire = Input.GetKeyDown(KeyCode.Mouse0);
      }
      
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
