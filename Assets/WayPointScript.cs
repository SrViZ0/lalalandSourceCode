using UnityEditor;
using UnityEngine;

public enum WayPointParts
{
    MARKER,GUIDE
}
public class WayPointScript : MonoBehaviour
{
    //Acces child using this script
    [HideInInspector] public GameObject marker;
    [HideInInspector] public GameObject guide;

    private void Awake()
    {
        marker = this.transform.GetChild(0).gameObject;
        guide = this.transform.GetChild(1).gameObject;
    }

    public void ToggleMarker(bool input, WayPointParts target)
    {
        switch (target)
        {
            case WayPointParts.MARKER:marker.SetActive(input);
                break;
            case WayPointParts.GUIDE: guide.SetActive(input);
                break;
        } 
        
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(WayPointScript))]
public class WPEdt : Editor
{
    public override void OnInspectorGUI()
    {
        //TBA
        WayPointParts wpp;
        bool tf = false;
    }
}
#endif
