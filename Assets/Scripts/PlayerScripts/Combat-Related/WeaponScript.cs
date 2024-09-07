  using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponScript : MonoBehaviour, ICollisonAmmo
{
    InputManager inputManager;

    [Tooltip("Weapon assigned")]
    public ItemInfoSO ItemInfo; //Data for weapon equipped

    [Header("Shooting Variables")]

    [Tooltip("Coords to instentiate bullet")]
    public GameObject shootPoint;

    public float timer; //Tracks the time between each shot

    private int shotsFired; //Tracks how many bullets has been fired regardless of reload.

    [Header("Debug")]
    [SerializeField] private bool fillGun, infAmmo, emptyMag, specAval, allowPickUps; //Mostly debugging, specAval is if the weapon has a special feature, allowPickUps if weapon reload using pickups
    //Mode change occures in the script assigned to a weapon

    private void OnEnable()
    {
        LoadModel();
    }
    private void OnDisable()
    {
        specAval = false;
    }
    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }
    private void Update()
    {
        if (NullCheck())
        {
            //Runs check that needs to be done every frame
            CanShoot();
            Reloads();
        }

        SpecialWeaponAddons(); //Can put in nullcheck but its wtv
    }

    public void LoadModel()
    {
        if (NullCheck())
        {
            DupeCheck();
        }
    }
    private bool DupeCheck() //Loads in weapon model
    {
        foreach (Transform child in this.transform)
        {
            if (child.gameObject != ItemInfo.activeModel)
            {
                try
                {
                    Instantiate(ItemInfo.activeModel, this.transform);
                }
                catch { break; }

                Destroy(this.transform.GetChild(0).gameObject);

                specAval = true;

                return false;
            }
        }
        return true;
    }


    private bool NullCheck() //Check if there is a weapon equipped
    {
        if (ItemInfo != null)
        {
            return true;
        }
        return false;
    }
    public void HandleShooting()
    {

        if (!NullCheck()) 
        {
            return;
        }
        if (CanShoot() && HasAmmo() && specAval) 
        {
            Instantiate(ItemInfo.projectilePrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            ItemInfo.currentAmmoCount--;
            shotsFired++;
            timer = 0;
            //TODO add sfx and vfx here
        }
    }

    public bool CanShoot() //Check firerate
    {
        if (timer >= ItemInfo.fireRate)
        {
            return true;
        }
        else if (timer < 5) //Idk why this is 5 tbh
        {
            timer += Time.deltaTime;
        }
        return false;
    }

    public bool HasAmmo() //Check if weapon has ammo
    {
        if (ItemInfo.currentAmmoCount > 0) 
        {
            return true;
        }
        return false;
    }

    private void Reloads()
    {
        //Run all 3 Simul

        if (fillGun) //Dont set fillgun to True if weapon has spec prop
        {
            ItemInfo.currentAmmoCount = ItemInfo.maxAmmoCount;
            fillGun = false;
        }

        if (emptyMag)
        {
            ItemInfo.currentAmmoCount = 0;
        }
        

        if (infAmmo)         //debugUse
        {
            ItemInfo.currentAmmoCount = ItemInfo.maxAmmoCount;
        }
    }

    public void PickUpAmmo(GameObject ammoPickUp)
    {
        //if (!allowPickUps) return;
        Destroy(ammoPickUp.gameObject);
        fillGun = true;
    }

    private void SpecialWeaponAddons() //Check if weapon has any special addons
    {
        if (!NullCheck()) //Actually redundant but ehh
        {
            return;
        }

        switch (ItemInfo.id) //Fun part. Checks what the weapon is.
        {

            case "GluBlastr":
                GlueBlasterSpec();
                break;

            case "Grapple":
                GrapplingHookSpec();
                //Todo - track shots fired, do the reload over time here && UI circle display here
                break;

        }
    }

    public bool GlueBlasterSpec() //Stuff
    {
        //Expensive but ehh
        Renderer firstCharge = this.transform.GetChild(0).GetChild(7).GetChild(0).gameObject.GetComponent<Renderer>();
        Renderer secondCharge = this.transform.GetChild(0).GetChild(7).GetChild(1).gameObject.GetComponent<Renderer>();
        Renderer thirdCharge = this.transform.GetChild(0).GetChild(7).GetChild(2).gameObject.GetComponent<Renderer>();

        Material uncharged = this.transform.GetChild(0).GetComponent<GlueBlastrSpec>().chargeMaterial[0];
        Material charged = this.transform.GetChild(0).GetComponent<GlueBlastrSpec>().chargeMaterial[1];

        bool activeCheck = this.transform.GetChild(0).GetComponent<GlueBlastrSpec>().CountDown();

        switch (shotsFired)
        {
            //case 0:
            //    firstCharge.material = uncharged;
            //    secondCharge.material = uncharged;
            //    thirdCharge.material = uncharged;
            //    break;
            case 1: firstCharge.material = charged;
                break;
            case 2: secondCharge.material = charged;
                break;
            case 3: thirdCharge.material = charged;
                break;
            case 4:
                if (specAval)
                {
                    emptyMag = true;//Empty active magazine
                    specAval = false;
                    return true;
                }
                Debug.Log(activeCheck);
                if (!activeCheck && !specAval && shotsFired == 4)
                {
                    //Debug.Log("Trigggreded" + !this.gameObject.activeInHierarchy);
                    specAval = true;
                    emptyMag = false;
                    shotsFired = 0;
                    firstCharge.material = uncharged;
                    secondCharge.material = uncharged;
                    thirdCharge.material = uncharged;
                    //ToDo reset chargets
                }
                return false;
        }
        return false;
    }
    public void GrapplingHookSpec() 
    {
        timer = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TestGrapple>().valToTrack;
    }
}
