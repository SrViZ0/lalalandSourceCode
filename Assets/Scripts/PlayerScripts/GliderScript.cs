using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GliderScript : MonoBehaviour
{
    Rigidbody playerRb;
    [SerializeField] KeyCode glideKey;
    [SerializeField] MiscInfoSO miscSo;
    [SerializeField] GameObject glider;
    bool gliding;
    float timer;

    private void Awake()
    {
        glider.SetActive(false);
        if (glideKey == KeyCode.None)
        {
            glideKey = KeyCode.LeftAlt;
        }

        playerRb = this.GetComponent<Rigidbody>();

        if (miscSo is null) return;
        miscSo.active = false;
        //Todo - get component of SO and check for unlock bool
    }
    private void Start()
    {
        GameEventsManager.instance.miscEvent.onMiscUnlock += UnlockGlider;
    }
    private void OnEnable()
    {
        GameEventsManager.instance.miscEvent.onMiscUnlock += UnlockGlider;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.miscEvent.onMiscUnlock -= UnlockGlider;
    }
    // Update is called once per frame
    void Update()
    {
        if (miscSo is null) return;
        if (Input.GetKeyUp(glideKey) && gliding)
        {
            timer = 1f; //CD
        }
        if (Input.GetKey(glideKey) && miscSo.unlocked && playerRb.velocity.y < 0 && timer < 0)
        {
            gliding = true;
            playerRb.velocity = new Vector3 (playerRb.velocity.x, playerRb.velocity.y/20, playerRb.velocity.z);
            miscSo.active = true;
            glider.SetActive(true);
        }
        else { miscSo.active = false; glider.SetActive(false); timer -= Time.deltaTime; gliding = false; ; }
    }

    public void UnlockGlider(string id)
    {
        if (miscSo.id == id) 
        { 
            miscSo.unlocked = true;
            //Modify some UI elements
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((LayerMask.GetMask("Ground") & 1 << collision.gameObject.layer) > 0 && gliding)
        {
            timer = 1f;
        }
    }
}
