using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    public GameObject enemyProjectile;
    [SerializeField]private Transform projectileSpawnPoint;
    private float minDistance = 20;
    [SerializeField] private float playerDistance;
    private int rotationSpeed = 720;

    // Start is called before the first frame update
    void Start()
    {
        projectileSpawnPoint = gameObject.transform.GetChild(0);
        SetEnemyStats();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovementAI();
        EnemyAttackAI();
        EnemyBoundary();
    }

    public override void SetEnemyStats()
    {
        moveSpeed = 10;
        repelSpeed = 10;
        attackDamage = 5;
        attackSpeed = 3;
        maxHealth = 5;
        currentHealth = maxHealth;
    }

    public override void DamageEnemy(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            playerController.LevelUp(5);
            Destroy(gameObject);
        }
    }

    public override void EnemyMovementAI()
    {
        Vector3 fleeDirection = (transform.position - player.transform.position).normalized;
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (isAttack == false && playerDistance < minDistance)
        {
            transform.Translate(moveSpeed * Time.deltaTime * fleeDirection, Space.World);
        }

        if (playerDistance >= minDistance && isAttack == false)
        {
            isAttack = true;
            StartCoroutine(EnemyRangedAttack());
        }
    }

    public override void EnemyAttackAI()
    {
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;
        if (playerDirection != Vector3.zero)
        {
            Quaternion lookDirection = Quaternion.LookRotation(playerDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookDirection, rotationSpeed * Time.deltaTime);
            
        }
    }

    public void EnemyRangedDamage()
    {
        playerHealth.DamagePlayer(attackDamage);
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
    }

    IEnumerator EnemyRangedAttack()
    {
        if (isAttack == true)
        {
            yield return new WaitForSeconds(attackSpeed);
            Instantiate(enemyProjectile, projectileSpawnPoint.transform.position, transform.rotation, transform);
            isAttack = false;
        }
        
    }
}
