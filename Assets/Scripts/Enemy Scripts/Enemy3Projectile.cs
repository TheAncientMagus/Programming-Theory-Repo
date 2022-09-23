using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Projectile : MoveForward
{
    [SerializeField]private Enemy3 enemy3;

    private void Awake()
    {
        enemy3 = GetComponentInParent<Enemy3>();
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
            enemy3.EnemyMidRangedDamage();
            Destroy(gameObject);
        }
    }
}
