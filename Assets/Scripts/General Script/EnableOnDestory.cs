using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnableOnDestory : MonoBehaviour
{
    [SerializeField] List<GameObject> target = new List<GameObject>();

    private void Awake()
    {
        if (target is not null)
        {
            foreach (GameObject item in target)
            { 
                item.SetActive(false);
            }
        }
    }
    public void OnDestroy()
    {
        foreach (GameObject item in target)
        {
            item.SetActive(true);
        }
    }
}
