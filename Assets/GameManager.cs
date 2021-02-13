using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // tracker for what level we are and what data to load
    int CurrentLevel = 0;

    // enemycount scriptable object
    public EnemyCount EnemyCount;


    //game Text Display
    public GameObject GameStatus;
    public EnemyTextDisplay enemyTextDisplay;

    [SerializeField]
    GameObject nextLevelDisplay;
    public GameObject playerRightRay;

    // pull Data from level scriptable object
    // variables for managing level difficulty
    // number of enmies
    public int amountToPoolEnemyOne;
    public int amountToPoolEnemyTwo;

    // time between enemy spawning
    public float enemyOneTimeBetweenSpawn;
    public float enemyTwoTimeBetweenSpawn;

    // EnemySpeed
    public float enemyOneSpeed;
    public float enemyTwoSpeed;

    // create list for scriptable objects and a current level data tracker
    public List<LevelSettings> levelSettingsList;
    public LevelSettings currentLevelData;

    private enum State
    {
        Idle,
        Playing,
    }

    private State state;

    void Awake()
    {
        state = State.Idle;
        nextLevelDisplay.SetActive(false);
    }

    private void Start()
    {
    }

    public void quitGame()
    {
        Application.Quit();
    }

    // Start Game Function
    public void GameStart()
    {
        state = State.Playing;
        GameStatus.SetActive(true);
        if (state == State.Playing)
        {
            // grab level data from scriptable object
            LoadLevelSettings(CurrentLevel);
            // start Spawn Manager
            SpawnManager.SharedInstance.StartSpawnPool();
            SpawnManager.SharedInstance.SpawnEnemies();

        }
        nextLevelDisplay.SetActive(false);
        playerRightRay.SetActive(false);
    }

    void LoadLevelSettings(int currentlevel)
    {
        resetEnemyCount();
        currentLevelData = levelSettingsList[currentlevel];
        amountToPoolEnemyOne = currentLevelData.amountToPoolEnemyOne;
        amountToPoolEnemyTwo = currentLevelData.amountToPoolEnemyTwo;

        enemyOneTimeBetweenSpawn = currentLevelData.enemyOneTimeBetweenSpawn;
        enemyTwoTimeBetweenSpawn = currentLevelData.enemyTwoTimeBetweenSpawn;

        enemyOneSpeed = currentLevelData.enemyOneSpeed;
        enemyTwoSpeed = currentLevelData.enemyTwoSpeed;
    }

    public void EnemyOneDied()
    {
        EnemyCount.EnemyOneCount -= 1;
        enemyTextDisplay.InsertText();
    }

    public void EnemyTwoDied()
    {
        EnemyCount.EnemyTwoCount -= 1;
        enemyTextDisplay.InsertText();
    }

    public void LoadNextLevel(int newLevel)
    {
        CurrentLevel++;
        GameStart();
    }

    void resetEnemyCount()
    {
        EnemyCount.EnemyOneCount = 0;
        EnemyCount.EnemyTwoCount = 0;
        EnemyCount.EnemyOneCountSpawned = 0;
        EnemyCount.EnemyTwoCountSpawned = 0;
    }

    private void Update()
    {
        if(state == State.Playing && CheckGameFinish())
        {
            nextLevelDisplay.SetActive(true);
            state = State.Idle;
            playerRightRay.SetActive(true);
        }
    }

    private bool CheckGameFinish()
    {
        if (EnemyCount.EnemyOneCountSpawned == currentLevelData.amountToPoolEnemyOne && EnemyCount.EnemyTwoCountSpawned == currentLevelData.amountToPoolEnemyTwo
            && EnemyCount.EnemyOneCount == 0 && EnemyCount.EnemyTwoCount == 00)
        {
            return true;
        }
        else
            return false;
    }
}
