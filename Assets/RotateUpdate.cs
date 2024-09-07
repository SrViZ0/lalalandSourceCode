using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUpdate : MonoBehaviour
{
    [SerializeField] float speed = -30f;
    private void Update()
    {
        this.transform.Rotate(0,speed * Time.deltaTime,0);
    }
}
