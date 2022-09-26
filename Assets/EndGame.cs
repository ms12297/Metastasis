using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndGame : MonoBehaviour
{
    public GameObject Thelper;
    public GameObject Parent;
    int y = 1;

    // Update is called once per frame
    void Update()
    {
        if (!Parent.GetComponent<EnemyController>().enabled && y == 1)
        {
            Thelper.GetComponent<Thelper>().EndGame();
            y = 0;
        }
    }
}
