using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Empty_Multiplier", menuName = "ScriptableObjects/Empty Multiplier", order = 4)]
public class PointsInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("State")]
    public bool unlocked = false;
    public bool active = false;

    [Header("Properties")]
    public float multiplier;    
    
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
