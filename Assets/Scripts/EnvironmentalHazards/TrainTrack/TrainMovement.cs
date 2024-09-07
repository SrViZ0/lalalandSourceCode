using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    private bool isOnTrack;
    public LayerMask track;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDown();
    }

    void CheckDown()
    {
        isOnTrack = Physics.Raycast(transform.position, Vector3.down, 1f , track);
        if (isOnTrack)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        else
        {
            CheckFront();
            CheckLeft();
            CheckRight();
        }
    }
    void CheckFront()
    {
        bool isFront;
        isFront = Physics.Raycast(transform.position, Vector3.forward, 1f, track);
        if (isFront)
        {
            transform.Rotate(Vector3.up * Time.deltaTime);
        }
    }
    void CheckLeft()
    {
        bool isLeft;
        isLeft = Physics.Raycast(transform.position, Vector3.left, 1f, track);
        if (isLeft)
        {
            transform.Rotate(Vector3.left * Time.deltaTime);
        }
    }
    void CheckRight()
    {
        bool isRight;
        isRight = Physics.Raycast(transform.position, Vector3.right, 1f, track);
        if (isRight)
        {
            transform.Rotate(Vector3.right * Time.deltaTime);
        }
    }
}
