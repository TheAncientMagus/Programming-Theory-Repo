using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject roundsUI;
    public GameObject round1Button;
    public GameObject round2Button;
    public GameObject round3Button;
    public GameObject mainMenuButton;
    private Vector3 enemy1Spawn = new(0, 1, -15);
    private Vector3 enemy2Spawn = new(0, 1, 15);
    private Vector3 enemy3Spawn = new(-15, 1, 0);
    [SerializeField] private bool isRound1 = false;
    [SerializeField] private bool isRound2 = false;
    [SerializeField] private bool isRound3 = false;
    [SerializeField] private bool allEnemiesSpawned = false;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Round1Check();
        Round2Check();
        Round3Check();

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void RoundVictory()
    {
        if (isRound1 == true && isRound2 == false)
        {
            roundsUI.SetActive(true);
            round1Button.SetActive(true);
            round2Button.SetActive(true);
            allEnemiesSpawned = false;
        }

        if (isRound2 == true && isRound3 == false)
        {
            roundsUI.SetActive(true);
            round1Button.SetActive(true);
            round2Button.SetActive(true);
            round3Button.SetActive(true);
            allEnemiesSpawned = false;
        }

        if (isRound3 == true)
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

    public void Round2()
    {
        roundsUI.SetActive(false);
        Invoke(nameof(Enemy1Spawner), 1);
        Invoke(nameof(Enemy2Spawner), 3);
        Invoke(nameof(Enemy3Spawner), 5);

        Invoke(nameof(Enemy1Spawner), 6);
        Invoke(nameof(Enemy2Spawner), 9);
        Invoke(nameof(Enemy3Spawner), 13);
        Invoke(nameof(RoundOngoing), 13.1f);
    }

    public void Round3()
    {
        roundsUI.SetActive(false);
        Invoke(nameof(Enemy1Spawner), 1);
        Invoke(nameof(Enemy2Spawner), 3);
        Invoke(nameof(Enemy3Spawner), 5);

        Invoke(nameof(Enemy1Spawner), 6);
        Invoke(nameof(Enemy2Spawner), 9);
        Invoke(nameof(Enemy3Spawner), 13);

        Invoke(nameof(Enemy1Spawner), 14);
        Invoke(nameof(Enemy2Spawner), 17);
        Invoke(nameof(Enemy3Spawner), 21);
        Invoke(nameof(RoundOngoing), 21.1f);
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

    private void Round1Check()
    {
        if (isRound1 == false && allEnemiesSpawned == true)
        {
            if (transform.childCount == 0)
            {
                isRound1 = true;
                RoundVictory();

            }
        }
        
    }

    private void Round2Check()
    {
        if (isRound2 == false && allEnemiesSpawned == true)
        {
            if (transform.childCount == 0)
            {
                isRound2 = true;
                RoundVictory();

            }
        }
        
    }

    private void Round3Check()
    {
        if (isRound3 == false && allEnemiesSpawned == true)
        {
            if (transform.childCount == 0)
            {
                isRound3 = true;
                RoundVictory();

            }
        }
        
    }
}
