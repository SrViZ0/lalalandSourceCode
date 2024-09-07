using UnityEngine;

public class GuideScript : MonoBehaviour
{
    Transform player;
    [SerializeField]float factor = 10;
    [SerializeField] float plyrYOffset = 0.015f;
    Material material;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        factor = 10; //Tested works. Dont question it
        material = transform.GetComponentInChildren<MeshRenderer>().material;
    }
    void Update()
    {
        float dist = Vector3.Distance(this.transform.position, player.position);
        this.transform.LookAt(player.position + new Vector3(0,plyrYOffset,0));
        this.transform.localScale = new Vector3 (1,1,  dist * factor);
        material.mainTextureScale = new Vector2 (1, dist * factor);
        material.mainTextureOffset -= new Vector2 (0, Time.deltaTime);
        if (material.mainTextureOffset.y < -30)
        {
            material.mainTextureOffset = Vector2.one;
        }
        
    }
}
