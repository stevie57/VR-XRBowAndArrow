using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlue : Enemy
{
    public float CurrentSpeed;
    public EnemyCount enemyCount;
    public LevelSettings levelSettings;

    private void Awake()
    {

    }

    public override void Movement(Transform playerTargetPos, float speed)
    {
        CurrentSpeed = levelSettings.enemyOneSpeed;
        base.Movement(playerTargetPos, CurrentSpeed);
    }

    public override void Die()
    {
        GameManagerEnemyOneDeath();
        base.Die();
    }

}
