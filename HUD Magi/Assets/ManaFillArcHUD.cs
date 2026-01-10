using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaFillArcHUD : MonoBehaviour
{
    public WeaponType weaponType;
    public WeaponManaManager manaManager;
    public Image fillImage;



    void Update()
    {
        float mana = manaManager.GetCurrentManaFloat(weaponType);
        int max = manaManager.GetMaxMana(weaponType);

        fillImage.fillAmount = mana / max;
    }

}
