using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBar : MonoBehaviour
{
    [SerializeField] private Gun gun;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.5f);
    [SerializeField] private Color selectedColor = Color.yellow;

    private void Awake()
    {
        gun.WeaponChanged += Highlight;
    }

    private void OnDestroy()
    {
        if (gun != null)
        {
            gun.WeaponChanged -= Highlight;
        }
    }

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => gun.SwitchWeapon(index));

            TMP_Text label = buttons[i].GetComponentInChildren<TMP_Text>();
            if (label != null && i < gun.WeaponCount)
            {
                label.text = gun.GetWeaponName(i);
            }
        }

        Highlight(gun.CurrentIndex);
    }

    private void Highlight(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.color = i == index ? selectedColor : normalColor;
        }
    }
}
