using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject roundsUI;
    public Button round1Button;
    private Vector3 enemy1Spawn = new(0, 1, -15);
    private Vector3 enemy2Spawn = new(0, 1, 15);
    private Vector3 enemy3Spawn = new(-15, 1, 0);
    [SerializeField] private bool allEnemiesSpawned = false;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (allEnemiesSpawned == true)
        {
            RoundVictory();
        }
        
    }

    private void RoundVictory()
    {
        if (transform.childCount == 0)
        {
            roundsUI.SetActive(true);
            allEnemiesSpawned = false;
        }
    }

    public void Round1()
    {
        
        roundsUI.SetActive(false);
        Invoke(nameof(Enemy1Spawner), 1);
        Invoke(nameof(Enemy2Spawner), 3);
        Invoke(nameof(Enemy3Spawner), 5);
        Invoke(nameof(RoundOngoing), 5.1f);
        


    }

    private void RoundOngoing()
    {
        allEnemiesSpawned = true;
    }

    private void Enemy1Spawner()
    {
        Instantiate(enemy1, enemy1Spawn, enemy1.transform.rotation, transform);
        Instantiate(enemy1, enemy1Spawn + (Vector3.left * 2), enemy1.transform.rotation, transform);
        Instantiate(enemy1, enemy1Spawn + (Vector3.right * 2), enemy1.transform.rotation, transform);
    }

    private void Enemy2Spawner()
    {
        Instantiate(enemy2, enemy2Spawn, enemy2.transform.rotation, transform);
        Instantiate(enemy2, enemy1Spawn, enemy2.transform.rotation, transform);
    }

    private void Enemy3Spawner()
    {
        Instantiate(enemy3, enemy3Spawn, enemy3.transform.rotation, transform);
        Instantiate(enemy3, enemy3Spawn + (Vector3.right * 30), enemy3.transform.rotation, transform);
    }
}
