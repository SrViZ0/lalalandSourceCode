using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float flashTime, rundownTimer;
    private string lastText;
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("PlayerParent");
    }
    private void Update()
    {
        if (player is null) return; //early return if cant find the player

        if (this.GetComponent<TextMeshProUGUI>().text != lastText)
        {
            rundownTimer = flashTime;
        }
        if (rundownTimer > 0)
        {
            rundownTimer -= Time.deltaTime;
        }
        if (rundownTimer <= 0)
        {
            this.GetComponent<TextMeshProUGUI>().text = null;
        }
        lastText = this.GetComponent<TextMeshProUGUI>().text;
    }
}
