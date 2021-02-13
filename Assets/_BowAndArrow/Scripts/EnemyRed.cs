using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : Enemy
{
    public float CurrentSpeed;
    public EnemyCount enemyCount;
    public LevelSettings levelSettings;


    public override void Movement(Transform playerTargetPos, float speed)
    {
        CurrentSpeed = levelSettings.enemyTwoSpeed;
        base.Movement(playerTargetPos, CurrentSpeed);
    }

    public override void Die()
    {
        GameManagerEnemyTwoDeath();
        base.Die();
    }
}
