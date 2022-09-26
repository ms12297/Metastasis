using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.incredigeek.com/home/unity-script-for-enemy-to-follow-player/


public class EnemyFollow : MonoBehaviour
{

    public float attackSpeed = 4; // How fast the game object moves
    public float attackDistance; //  How close does the player need to be to start moving
    public float bufferDistance; //How far away from the player should the game object stop
    public GameObject player; //Game player to target

    Transform playerTransform;

    void GetPlayerTransform()
    {
        if (player != null)
        {playerTransform = player.transform; }
        else
        {Debug.Log("Player not specified in Inspector");}
    }

    // Start is called before the first frame update
    void Start()
    {GetPlayerTransform();}

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(playerTransform.position, transform.position);
        // Debug.Log("Distance to Player" + distance);
        if (distance <= attackDistance)
        {
            if (distance > bufferDistance)
            {
                transform.position += transform.forward * attackSpeed * Time.deltaTime;
            }
            else if (distance<= bufferDistance)
                transform.position = this.transform.position;
            //attack
            //playerfalls
        }
    }
}