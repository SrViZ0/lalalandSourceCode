using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDestroy : MonoBehaviour
{
    [SerializeField] List<GameObject> targets;

    private void OnDestroy()
    {
        foreach
        (GameObject target in targets)
        {
           Destroy(target);
        }
    }
}
