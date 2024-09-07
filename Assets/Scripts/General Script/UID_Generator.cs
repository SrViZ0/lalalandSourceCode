using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class UID_Generator : MonoBehaviour
{
    //Note: Drag to prefab from scene to Asset menu to update to prefab with scene values

    public string id;
    [SerializeField] int idLenght = 8;
    const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*_+-=~?/";

    private void Awake() 
    {   //Test Currently not working just make sure everything matches up manually

        //if (id == null) Debug.LogWarning($"GameObject {this.name} dosen't have a ID, please manually generate one in the inspector menu");
        //DuplicateCheck();

        //string prefabId = EditorUtility.FindPrefabRoot(this.gameObject).gameObject.GetComponent<UID_Generator>().id;

        //if (prefabId != id) Debug.LogError($"GameObject {this.name} have mismatched ID, please manually update the prefab");

    }
    public void GenerateRandomID()
    {
        id = null;
        for (int i = 0; i < idLenght; i++)
        {
            id += glyphs[Random.Range(0, glyphs.Length)];
        }
    }


    //private void DuplicateCheck() //Debugging use (not working???)
    //{
    //    UID_Generator[] ids = MonoBehaviour.FindObjectsOfType<UID_Generator>();
    //    foreach (UID_Generator id in ids)
    //    {
    //        string uid = id.id;
    //        if (uid == this.id && EditorUtility.GetPrefabParent(id.gameObject)  == this.gameObject) Debug.LogError($"Duplicate UID found Please regenerate UID for the following asset: {id.gameObject.name}");
    //    }
    //}
//#if UNITY_EDITOR
//    private void OnValidate()
//    {
//        UnityEditor.EditorUtility.SetDirty(this);
//    }
//#endif
}


#if UNITY_EDITOR
[CustomEditor(typeof(UID_Generator))]
public class UidInspctor : Editor
{
    public override void OnInspectorGUI()
    {

        UID_Generator script = (UID_Generator)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Random ID"))
        {
            script.GenerateRandomID();
        }
    }
}
#endif
