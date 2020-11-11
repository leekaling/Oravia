using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class BasicEnemyMovement : Enemy
{
    #region enemy_variables
    // the enemy
    //public Enemy basicEnemy;
    // body of the attached enemy
    private Rigidbody EnemyRB;
    // where to move
    // if you want continuously in one direction I think you can set one to infinity
    private Vector3 pos1;
    public Vector3 pos2;
    private Vector3 originalPos2;
    private Vector3 originalPos1;
    private Vector3 whereToGo;
    #endregion

    #region Unity_functions
    // Start is called before the first frame update
    void Start()
    {
        EnemyRB = GetComponent<Rigidbody>();
        //basicEnemy = GetComponent<Enemy>();
        pos1 = transform.position;
        pos2.y = pos1.y;
        originalPos2 = pos2;
        originalPos1 = pos1;
    }

    // Update is called once per frame
    void Update()
    {
        pos1 = transform.position;
        if (transform.position == originalPos2)
        {
            pos2 = originalPos1;
        }
        if (transform.position == originalPos1)
        {
            pos2 = originalPos2;
        }
        Move();
    }
    #endregion

    #region movement_functions
    private void Move()
    {
        transform.position = Vector3.MoveTowards(pos1, pos2, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionStay(Collision col)
    {
        whereToGo = pos2;
        pos2 = transform.position;

        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().TakeDamage(damageDealt * Time.deltaTime);
        }
    }

    private void OnCollisionExit(Collision coll)
    {
        pos2 = whereToGo;
    }
    #endregion
}

