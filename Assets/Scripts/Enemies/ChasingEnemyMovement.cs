using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemyMovement : Enemy
{
    #region Tracking_variables
    // the player we want to chase
    Transform player;
    // body of the attached enemy
    Rigidbody EnemyRB;
    // the enemy
    //Enemy basicEnemy;
    // the minimum distance we want for the enemy to start chasing
    [SerializeField]
    int minDist;
    #endregion

    #region Unity_functions
    // Start is called before the first frame update
    void Start()
    {
        EnemyRB = GetComponent<Rigidbody>();
        //basicEnemy = GetComponent<Enemy>();
        //find the player in the scene
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        if (Vector3.Distance(transform.position, player.position) <= minDist)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().TakeDamage(damageDealt * Time.deltaTime);
        }
    }
    #endregion
}
