using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MoveForward
{
    [SerializeField]private Enemy2 enemy2;

    private void Awake()
    {
        enemy2 = GetComponentInParent<Enemy2>();
        transform.parent = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        DestroyOutOfBounds();
    }

    

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy2.EnemyRangedDamage();
            Destroy(gameObject);
        }
    }
}
