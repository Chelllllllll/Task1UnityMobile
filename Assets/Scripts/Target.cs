using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
   public int health;
   public TextMeshPro  healthText;

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
