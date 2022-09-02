using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 20;

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

    void Move()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime, Space.Self);
    }

    void DestroyOutOfBounds()
    {
        if (transform.position.x > 35)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < -35)
        {
            Destroy(gameObject);
        }

        if (transform.position.z > 35)
        {
            Destroy(gameObject);
        }

        if (transform.position.z < -35)
        {
            Destroy(gameObject);
        }
    }
}
