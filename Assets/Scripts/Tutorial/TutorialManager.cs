using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] List<string> tutorialTxt = new List<string>();   
    [HideInInspector]public int textIndex;
    [SerializeField] TextMeshProUGUI display;
    public static bool displayTxt;

    public TutorialDoorUnlock ttDU;

    private void Update()
    {
        display.text = tutorialTxt[textIndex];

        if (textIndex >= tutorialTxt.Count-1)
        {
            ttDU.canUnlock = true;
            Destroy(this.gameObject, 5f);
            Destroy(display, 20f);
        }
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
