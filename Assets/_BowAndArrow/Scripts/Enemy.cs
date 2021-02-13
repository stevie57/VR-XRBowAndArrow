using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Enemy : MonoBehaviour
{
    //for scriptable object attachment
    public EnemyStats enemyStats;

    //access to game manager
    public GameManager gameManager;

    //stats
    public int health;
    public int strength;
    public float speed = 1;

    //for counting arrows that have hit this enemy
    public List<Arrow> arrows;

    //death particle system
    private ParticleSystem particles;

    //player targeting for movement
    float smoothing = 1f;
    public Transform playerTargetPos;
    bool isDead = false;



    public virtual void Start()
    {
        //update enemy stats
        updateStats();
        arrows = new List<Arrow>();
        particles = GetComponent<ParticleSystem>();

        //set target to player position and movement coroutine
        playerTargetPos = Camera.main.transform;
        Movement(playerTargetPos, speed);

    }

    public virtual void Movement(Transform playerTargetPos, float speed)
    {
        StartCoroutine(moveToTargetCoroutine(playerTargetPos, speed));
    }

    public virtual void updateStats()
    {
        health = enemyStats.health;
        strength = enemyStats.strength;

    }

    // enemy movement
    public virtual IEnumerator moveToTargetCoroutine (Transform target, float speed)
    {
        while (Vector3.Distance(transform.position, target.position) > 0.05f && !isDead)
        {
            Vector3 lookAtPos = target.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(lookAtPos, transform.up);
            Quaternion targetRotation = new Quaternion(0, newRotation.y, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

            //check direction rotation is facing before moving
            float dot = Vector3.Dot(transform.forward, (target.position - transform.position).normalized);
            if (dot > 0.8)
            {
                float step = speed * Time.deltaTime;
                Vector3 targetPos = new Vector3(target.position.x, 0, target.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            }
            yield return null;
        }
    }

    public void TakeDamage(Arrow arrow, int damage)
    {
        arrows.Add(arrow);
        health -= damage;
        if(health <= 0)
            Die();   
    }

    public virtual void Die()
    {
        isDead = true;
        MeshRenderer meshrend = GetComponent<MeshRenderer>();
        meshrend.material.color = Color.black;
        foreach (Arrow arrow in arrows)
        {
            arrow.GetComponent<XRGrabInteractable>().colliders.Clear();
            particles.transform.parent = null;
            particles.Play();
        }
        StartCoroutine(Death());
    }

    public void GameManagerEnemyOneDeath()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        gameManager.EnemyOneDied();
    }

    public void GameManagerEnemyTwoDeath()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        gameManager.EnemyTwoDied();
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        GameObject thisEnemy = this.gameObject;
        thisEnemy.SetActive(false);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player has been hit ! Gameover");
        }
    }


    private void Update()
    {

    }
}
