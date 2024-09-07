using System;
using UnityEngine;

public class MarkerScript : MonoBehaviour
{
    Transform player;
    SpriteRenderer material;
    [Tooltip("Distance Marker starts fading out")]
    [SerializeField] float fadeDist = 4;
    [Tooltip("% of fadeDist where marker compeletely fades out")]
    [Range(0, 1)] [SerializeField] float fadeRange = 0.375f; //Default is 37.5%
    Color color;
    /*[Range(0, 1)] [SerializeField] */float alpha = 1f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        material = this.transform.GetComponentInChildren<SpriteRenderer>();
    }
    void Update()
    {
        float dist = Vector3.Distance(player.position, this.transform.position);
        transform.LookAt(player.position);
        color = material.color;
        if (dist < fadeDist)
        {
            alpha = ((dist*dist-((fadeDist* fadeRange) *dist)) / fadeDist);
        }
        else 
        {
            alpha = 1;
        }
        color.a = alpha;
        material.color = color;
    }
}
