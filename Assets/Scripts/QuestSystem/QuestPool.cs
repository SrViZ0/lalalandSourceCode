using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "QuestCollection", menuName = "ScriptableObjects/Quest Pool List", order = 3)]
public class QuestPool : ScriptableObject
{
    [SerializeField]
    public QuestInfo [] questList;
}


