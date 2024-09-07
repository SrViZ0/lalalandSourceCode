using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBarUI : MonoBehaviour
{
    [SerializeField] RectMask2D ammoBarMask;
    [SerializeField] Transform invSlots;
    WeaponScript activeWeapon;
    float ammoPercent;

    private void Start()
    {
        invSlots = GameObject.Find("InvSlots").transform;
    }

    private void Update()
    {
        foreach (Transform child in invSlots)
        {
            if (child.gameObject.activeSelf)
            {   
                activeWeapon = child.GetComponent<WeaponScript>();
                if (activeWeapon.ItemInfo == null) { ammoBarMask.padding = new Vector4(0, 0, 0, 0); return; }
            }
        }

        //Debug.Log("Ammo " + ((activeWeapon.ItemInfo.maxAmmoCount - activeWeapon.ItemInfo.currentAmmoCount).ConvertTo<float>() / activeWeapon.ItemInfo.maxAmmoCount.ConvertTo<float>()) * 165f);

        ammoBarMask.padding = new Vector4(0, 0, ((activeWeapon.ItemInfo.maxAmmoCount - activeWeapon.ItemInfo.currentAmmoCount).ConvertTo<float>() / activeWeapon.ItemInfo.maxAmmoCount.ConvertTo<float>()) * 165f , 0);
    }
}
