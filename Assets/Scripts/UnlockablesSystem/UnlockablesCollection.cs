using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemCollection", menuName = "ScriptableObjects/Item Pool List", order = 6)]
public class UnlockablesCollection : ScriptableObject
{
    [SerializeField]
    public ItemInfoSO[] itemList;
}

