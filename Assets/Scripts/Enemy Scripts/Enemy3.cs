using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{
    public GameObject enemyProjectile;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float playerDistance;
    private float minDistance = 12;
    private float maxDistance = 18;
    private float circleSpeed = 90;
    private int rotationSpeed = 720;
    [SerializeField]private bool isFlee = false;


    private void Start()
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
        moveSpeed = 15;
        repelSpeed = 10;
        attackDamage = 15;
        attackSpeed = 4;
        maxHealth = 10;
        currentHealth = maxHealth;

    }

    // Enemy3 will move towards the player if outside minDistance
    // Once at minDistance Enemy 3 will cirlce around the player until they finish attacking
    // They will then run away from the player
    public override void EnemyMovementAI()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        Vector3 fleeDirection = (new Vector3(transform.position.x, 1, transform.position.z) - new Vector3(player.transform.position.x, 1,player.transform.position.z)).normalized;
        playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (playerDistance > minDistance && isAttack == false && isFlee == false)
        {
            transform.Translate(moveSpeed * Time.deltaTime * lookDirection, Space.World);
        }

        if (playerDistance <= minDistance && isAttack == false && isFlee == false)
        {
            isAttack = true;
            StartCoroutine(EnemyMidRangedAttack());
        }

        if (isAttack == true && isFlee == false)
        {
            if (playerDistance > maxDistance)
            {
                transform.Translate((moveSpeed * 2) * Time.deltaTime * lookDirection, Space.World);
            }

            else if (playerDistance < minDistance)
            {
                transform.Translate(moveSpeed * Time.deltaTime * fleeDirection, Space.World);
            }

            else
            {
                transform.RotateAround(player.transform.position, Vector3.up, circleSpeed * Time.deltaTime);
            }
            
        }

        if (isFlee == true)
        {
            transform.Translate(moveSpeed/3 * Time.deltaTime * fleeDirection, Space.World);
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

    public void EnemyMidRangedDamage()
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

    IEnumerator EnemyMidRangedAttack()
    {
        if (isAttack == true)
        {
            yield return new WaitForSeconds(attackSpeed);
            Instantiate(enemyProjectile, projectileSpawnPoint.transform.position, transform.rotation, transform);
            isAttack = false;
            isFlee = true;
            yield return new WaitForSeconds(attackSpeed);
            isFlee = false;

        }
    }
}
