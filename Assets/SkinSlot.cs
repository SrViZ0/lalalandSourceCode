using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSlot : MonoBehaviour
{
    public CharacterSkinInfoSO characterSkinInfo;
    List<SkinnedMeshRenderer> playerModel = new List<SkinnedMeshRenderer>();

    private void Start()
    {   
        GameObject player = GameObject.Find("BodyParts");
        foreach(Transform child  in player.transform)
        {
            playerModel.Add(child.gameObject.GetComponent<SkinnedMeshRenderer>());
        }
    }
    public void EquipSkin()
    {
        if (!characterSkinInfo.unlocked) return;
        foreach (SkinnedMeshRenderer mr in  playerModel) 
       {
            if (mr != null)
            {
                mr.material = characterSkinInfo.skinUV;
                Debug.Log("Skin Equipped");
            }
       }
    }
}
