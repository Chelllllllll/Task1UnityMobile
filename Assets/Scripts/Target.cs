using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
   public int health;
   public TextMeshPro  healthText;

   public int Health
   {
      get
      {
         return health;
      }
   }

   public void SetState(int newHealth, bool alive)
   {
      health = newHealth;
      UpdateText();
      gameObject.SetActive(alive);
   }
   
   public void Start()
   {
      UpdateText();
   }

   public void TakeDamage(int damage)
   {
      health -= damage;
      UpdateText();
      if (health <= 0)
      {
         if (GameManager.Instance != null)
         {
            GameManager.Instance.AddScore(1);
         }
         gameObject.SetActive(false);
      }
   }

   private void UpdateText()
   {
      if (healthText != null)
      {
         healthText.text = health.ToString();
      }
   }
}
