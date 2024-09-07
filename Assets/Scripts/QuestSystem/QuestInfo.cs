using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Empty_Quest", menuName = "ScriptableObjects/Empty Quest", order = 1)]
public class QuestInfo : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Requirements")]
    //public GameObject[] questRequirement; //alternative
    public ScriptableObject[] questRequirement;
    //public QuestInfo[] questRequirement;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public ScriptableObject itemReward;

    // ensure the id is always the name of the Scriptable Object asset
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
