using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTrashShute : MonoBehaviour
{
    public bool PlayerInRange = false;

    public bool trashDoor = false;

    public TrashShute theDoor;
    public TrashDispose throwAway;

    public AudioSource Open;
    public AudioSource Close;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange && !trashDoor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Trash door Open");
                trashDoor = true;
                theDoor.isOpening= true;
                theDoor.rotate = false;
                throwAway.throwAway = false;

                Open.Play();
            }
        }

        else if (PlayerInRange && trashDoor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Trash door Close");
                trashDoor = false;
                theDoor.isOpening = false;
                theDoor.rotate = true;
                throwAway.throwAway = true;

                Close.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When target is hit
        if (other.gameObject.name == "ExternalChecker")
        {
            PlayerInRange = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When target is hit
        if (other.gameObject.name == "ExternalChecker")
        {
            PlayerInRange = false;

        }

    }


}
