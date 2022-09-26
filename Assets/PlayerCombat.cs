using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 1f;
    public int attackDamage = 20; //Change to 10 later here and in Unity console
    public float attackRate = 2f;
    float nextAttackTime = 0f;


    private float nextspawntime;
    [SerializeField] private GameObject cancerCellW;
    [SerializeField] private GameObject cancerCellT;

    [SerializeField] private float spawndelay = 10;

    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextspawntime)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Spawn();
            }
            
        }


        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                //To make sure player cannot attack more than 2 times in a second
                nextAttackTime = Time.time + (1f / attackRate);
            }
        }

    }

    void Attack()
    {
        //Play an attack animation
        animator.SetTrigger("Attack");

        //Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        //Deal damage
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("You hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Play Hit Reaction Animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    //To visualize attack range
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }

    void Die()
    {
        Debug.Log("You died!");
        //Die animation
        animator.SetBool("isDead", true);
        //Disable enemy
        this.enabled = false;
        //Making sure player does not run into the enemy after enemy is disabled
        GetComponent<Collider>().enabled = false;
        GetComponent<Movement>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;
    }

  
    private void Spawn()
    {

        animator.SetTrigger("Summon");
        nextspawntime = Time.time + spawndelay;
        Instantiate(cancerCellW, transform.position, transform.rotation);
        Instantiate(cancerCellT, transform.position, transform.rotation);
    }

}

