using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FlashSecretFound : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public GameObject don;
    public int childCount;
    private void Awake()
    {
        don.SetActive(false);
    }
    void Update()
    {
        counter.text = $"{7-this.transform.childCount}/7";

        CheckForCollection();

        childCount = transform.childCount;
    }

    private void CheckForCollection()
    {
        if (childCount != this.transform.childCount && this.transform.childCount != 7) 
        { 
            FlashThePlayer();
        }
    }

    private void FlashThePlayer()
    {
        don.SetActive(true);
        Invoke("UnFlashThePlayer", 2f);
    }
    private void UnFlashThePlayer()
    {
        don.SetActive(false);
    }
}
