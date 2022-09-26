using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayer;

    public float attackRange = 1.2f;
    public int attackDamage = 20; //Change to 10 later here and in Unity console
    public float attackRate = 5f;
    float nextAttackTime = 5f;


    public int maxHealth = 100;
    int currentHealth;

    public int y = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }
    void Update()
    {
        if ((GetComponent<EnemyController>().distance) <= 2)
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                //To make sure enemy cannot attack more than 2 times in 10 seconds
                nextAttackTime = Time.time + attackRate;
            }

        }
    }

    public void Attack()
    {
        //Play an attack animation
        animator.SetTrigger("Attack");

        //Detect if player in range of attack
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        //Deal damage
        foreach (Collider enemy in hitPlayer)
        {
            /*if (enemy.name == "User") enemy.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
            else enemy.GetComponent<CancerCell>().TakeDamage(attackDamage);*/
            enemy.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }

    }

    public void TakeDamage (int damage)
    {
        currentHealth -= damage;
        //Play Hit Reaction Animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        
        }

    }

    void Die()
    {
        Debug.Log("Enemy died!");
        //Die animation
        animator.SetBool("isDead", true);
        //Disable enemy
        this.enabled = false;
        //Making sure player does not run into the enemy after enemy is disabled
        GetComponent<Collider>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        y = 1;
    }

    //To visualize attack range
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
    

}
