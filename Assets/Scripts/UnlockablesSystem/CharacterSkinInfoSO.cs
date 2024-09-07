using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Character Skin", menuName = "ScriptableObjects/Skin Info", order = 4)]
public class CharacterSkinInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("State")]
    public bool unlocked = false;
    public bool active = false;

    public Material skinUV;

    public void UnlockSkin(bool a)
    {
        unlocked = a;
    }

    public void ActiveSkin(bool a)
    {
        active = a;
    }
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
