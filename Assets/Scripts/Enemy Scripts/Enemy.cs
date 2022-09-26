using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // INHERITANCE
    [SerializeField] protected GameObject player;
    [SerializeField] protected Rigidbody enemyRb;
    [SerializeField] protected Health playerHealth;
    [SerializeField] protected PlayerController playerController;
    protected float xBoundary = 34.5f;
    protected float zBoundary = 34.5f;
    [SerializeField] protected int currentHealth;
    protected int maxHealth;
    protected int moveSpeed;
    protected int repelSpeed;
    protected int attackDamage;
    protected int attackSpeed;
    [SerializeField]protected bool isAttack = false;

    // INHERITANCE
    void Awake()
    {
        player = GameObject.Find("Player");
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        enemyRb = gameObject.GetComponent<Rigidbody>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        SetEnemyStats();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovementAI();
        EnemyBoundary();
    }

    // ABSTRACTION
    public virtual void SetEnemyStats()
    {
        moveSpeed = 5;
        repelSpeed = 10;
        attackDamage = 10;
        attackSpeed = 2;
        maxHealth = 15;
        currentHealth = maxHealth;
    }

    // ABSTRACTION
    public virtual void DamageEnemy(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            playerController.LevelUp(10);
            Destroy(gameObject);
        }
    }

    // ABSTRACTION
    public virtual void EnemyMovementAI()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        if (isAttack == false)
        {
            transform.Translate(moveSpeed * Time.deltaTime * lookDirection);
        }
        
    }

    // ABSTRACTION
    // INHERITANCE
    public void EnemyBoundary()
    {
        if (transform.position.x > xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        }

        if (transform.position.z > zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);
        }

        if (transform.position.z < -zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundary);
        }
    }

    // ABSTRACTION
    public virtual void EnemyAttackAI()
    {
        Vector3 repelDirection = (transform.position - player.transform.position).normalized;
        if (isAttack == true)
        {
            enemyRb.AddForce(repelDirection * repelSpeed, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player Melee Weapon"))
        {
            DamageEnemy(playerController.meleeDamage);
        }

        if (collision.gameObject.CompareTag("Player Ranged Weapon"))
        {
            DamageEnemy(playerController.rangedDamage);
        }

        if ( collision.gameObject.CompareTag("Player") && isAttack == false)
        {
            isAttack = true;
            StartCoroutine(EnemyDamage());
            EnemyAttackAI();
            
        }
    }

    IEnumerator EnemyDamage()
    {
        playerHealth.DamagePlayer(attackDamage);
        yield return new WaitForSeconds(attackSpeed);
        isAttack = false;
    }
}
