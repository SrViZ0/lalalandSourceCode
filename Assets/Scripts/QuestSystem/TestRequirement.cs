using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Empty_Requirements", menuName = "ScriptableObjects/Empty Quest Requirements", order = 2)]
public class TestRequirement : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }
    public bool testCondition;
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
