using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckerR : MonoBehaviour
{
    public ToasterActivator Right;

    public bool Present = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Present)
        //{
        //    Right.enemyPresentR = true;

        //}
        //else if (!Present)
        //{
        //    Right.enemyPresentR = false;

        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        

    }

}
