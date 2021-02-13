using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SpawnManager : MonoBehaviour
{
    //access to the game manager
    private GameManager gameManager;

    //creating shared instance of the spawn manager
    public static SpawnManager SharedInstance;

    //list of enemy spawn points
    public Transform[] SpawnPoint;

    //game status screen
    public EnemyTextDisplay enemyTextDisplay;
    public List<GameObject> pooledEnemyOne;
    public List<GameObject> pooledEnemyTwo;
    public GameObject EnemyOnePrefab;
    public GameObject EnemyTwoPrefab;
    //enemy count scriptable object
    public EnemyCount enemycount;

    //check if enemytextdisplay has something assigned to it
    //void checkTextNull()
    //{
    //    if (enemyTextDisplay != null)
    //    {
    //        Debug.Log("enemy text display does not equal null + " + enemyTextDisplay.gameObject.name);
    //    }
    //    else
    //    {
    //        Debug.Log("enemy text display is null");
    //    }
    //}

    private void Awake()
    {
        SharedInstance = this;
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
    }

    // Gamestarts when this function is implemented
   
    public void StartSpawnPool()
    {
        pooledEnemyOne = new List<GameObject>();
        GameObject EnemyOne;
        for (int i = 0; i < gameManager.amountToPoolEnemyOne; i++)
        {
            EnemyOne = Instantiate(EnemyOnePrefab);
            EnemyOne.SetActive(false);
            pooledEnemyOne.Add(EnemyOne);
        }

        pooledEnemyTwo = new List<GameObject>();
        GameObject EnemyTwo;
        for (int k = 0; k < gameManager.amountToPoolEnemyTwo; k++)
        {
            EnemyTwo = Instantiate(EnemyTwoPrefab);
            EnemyTwo.SetActive(false);
            pooledEnemyTwo.Add(EnemyTwo);
        }   
    }

    public void SpawnEnemies()
    {
        // starts the cube spawning
        StartCoroutine(SpawnEnemyOne());
        StartCoroutine(SpawnEnemyTwo());
        Debug.Log("spawn enmies is done");
    }


    IEnumerator SpawnEnemyOne()
    {
        while (enemycount.EnemyOneCountSpawned < gameManager.amountToPoolEnemyOne)
        {
            GameObject EnemyOne = GetPooledEnemy(pooledEnemyOne, gameManager.amountToPoolEnemyOne);
            if (EnemyOne != null)
            {
                pooledEnemyOne.Remove(EnemyOne);
                int RandomSpawnPosition = UnityEngine.Random.Range(0, SpawnPoint.Length);
                Debug.Log("random spawn position is " + RandomSpawnPosition);
                EnemyOne.transform.position = SpawnPoint[RandomSpawnPosition].transform.position;
                EnemyOne.SetActive(true);

                int random = (int)System.DateTime.Now.Ticks;
                UnityEngine.Random.InitState(random);
                enemycount.EnemyOneCount++;
                enemycount.EnemyOneCountSpawned++;
                

                enemyTextDisplay.InsertText();
            }
            yield return new WaitForSeconds(gameManager.enemyOneTimeBetweenSpawn);
        }
    }



    IEnumerator SpawnEnemyTwo()
    {
        while (enemycount.EnemyTwoCountSpawned < gameManager.amountToPoolEnemyTwo)
        {
            GameObject EnemyTwo = GetPooledEnemy(pooledEnemyTwo, gameManager.amountToPoolEnemyTwo);
            if (EnemyTwo != null)
            {
                pooledEnemyTwo.Remove(EnemyTwo);
                int RandomSpawnPosition = UnityEngine.Random.Range(0, SpawnPoint.Length);
                Debug.Log("random spawn position is " + RandomSpawnPosition);
                EnemyTwo.transform.position = SpawnPoint[RandomSpawnPosition].transform.position;
                EnemyTwo.SetActive(true);

                int random = (int)System.DateTime.Now.Ticks;
                UnityEngine.Random.InitState(random);
                enemycount.EnemyTwoCount++;
                enemycount.EnemyTwoCountSpawned++;

                enemyTextDisplay.InsertText();
            }
            yield return new WaitForSeconds(gameManager.enemyTwoTimeBetweenSpawn);
        }
    }

    public GameObject GetPooledEnemy(List<GameObject> enemylist, int amountToPool)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!enemylist[i].activeInHierarchy)
            {
                return enemylist[i];
            }
        }
        return null;
    }

}
