using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CancerCell : MonoBehaviour
{

    public float lookRadius = 30f;
    Transform target;
    NavMeshAgent agent;
    Animator anim;
    public float distance;

    public Transform attackPoint;
    public LayerMask enemyLayer;

    public float attackRange = 1.2f;
    public int attackDamage = 15; //Change to 10 later here and in Unity console
    public float attackRate = 5f;
    float nextAttackTime = 5f;

    public int maxHealth = 40;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        target = EnemyManager.instance.enemy.transform; 
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
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

            if (Time.time >= nextAttackTime)
            {
                Attack();
                //To make sure enemy cannot attack more than 2 times in 10 seconds
                nextAttackTime = Time.time + attackRate;
            }
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Attack()
    {
        //Play an attack animation
        anim.SetTrigger("Attack");

        //Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        //Deal damage
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Play Hit Reaction Animation
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        Debug.Log("Enemy died!");
        //Die animation
        anim.SetBool("isDead", true);
        //Disable enemy
        this.enabled = false;
        //Making sure player does not run into the cancer after it is disabled
        GetComponent<Collider>().enabled = false;
        GetComponent<CancerCell>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
    }

    //To visualize attack range
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }

}
