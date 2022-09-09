using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 20;
    private float boundary = 34.5f;

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
        if (transform.position.x > boundary)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < -boundary)
        {
            Destroy(gameObject);
        }

        if (transform.position.z > boundary)
        {
            Destroy(gameObject);
        }

        if (transform.position.z < -boundary)
        {
            Destroy(gameObject);
        }
    }
}
