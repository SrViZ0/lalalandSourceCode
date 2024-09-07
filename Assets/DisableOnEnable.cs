using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnEnable : MonoBehaviour
{
    [SerializeField] float duration;
    float t;
    private void OnEnable()
    {
          t = duration;
    }

    private void Update()
    {
        t -= Time.deltaTime;
        if ( t < 0 )
        {
            gameObject.SetActive( false );
        }
    }


}
