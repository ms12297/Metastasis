using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Thelper : MonoBehaviour
{
    public float lookRadius = 30f;
    Transform target;
    NavMeshAgent agent;
    public Animator anim;
    public float distance;
    public GameObject spawnpoint;
    public GameObject Enemy;
    [SerializeField] private GameObject Tkiller;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            //Animations
            anim.SetInteger("Run", 1);
        }

        if (distance <= agent.stoppingDistance)
        {
            anim.SetInteger("Run", 0);
            FaceTarget();
        }

       
       if (Enemy.GetComponent<Enemy>().y == 1) CallHelp();
        Enemy.GetComponent<Enemy>().y = 2;
   
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void CallHelp()
    {
        anim.SetTrigger("CallHelp");
        Instantiate(Tkiller, transform.position, transform.rotation);
    }

    public void EndGame()
    {
        Debug.Log("You Win!"); //For now
        anim.SetTrigger("CallHelp");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
