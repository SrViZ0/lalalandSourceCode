using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Empty_Item", menuName = "ScriptableObjects/Empty Unlockables", order = 4)]
public class ItemInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;

    //TODO - store item properties such as atk range etc here

    [Header("Item State")]
    public bool unlocked = false;

    public bool equipped = false; //item in hotbar
    public bool active = false; //using item

    [Header("Item Properties")]
    public float fireRate;

    public int currentAmmoCount;
    public int maxAmmoCount;
    public float downtime;

    public GameObject hubSlot;

    [Header("Item Asset")]
    public GameObject lockedModelPrefab;
    [HideInInspector] public GameObject activeModel;

    public GameObject projectilePrefab;


    [Header("Skin Data")]
    public List<GameObject> skins;
    // ensure the id is always the name of the Scriptable Object asset
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
