using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubTutorialPanelScript : MonoBehaviour
{
    public void ClosePanel()
    {
        this.enabled = false;
        Destroy(gameObject);
    }
}
