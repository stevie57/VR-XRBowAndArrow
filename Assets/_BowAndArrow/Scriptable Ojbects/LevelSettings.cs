using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Level Settings")]
public class LevelSettings : ScriptableObject
{
    //variables for managing level difficulty
    // number of enmies
    public int amountToPoolEnemyOne;
    public int amountToPoolEnemyTwo;

    // time between enemy spawning
    public float enemyOneTimeBetweenSpawn;
    public float enemyTwoTimeBetweenSpawn;

    // enemy movement speed
    public float enemyOneSpeed;
    public float enemyTwoSpeed;
}
