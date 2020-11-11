using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicEnemyController : Enemy
{
    #region enemy_variables
    // the type of enemy we want to spawn
    //public Enemy basicEnemy;
    // number of enemies we want to spawn
    public int numberOfEnemies;
    // Use empty GameObjects as positions to move to
    // this way you can control the positions more specifically
    public Vector3[] moveSpots;
    // a random spot
    private Vector3[] randomSpots;
    // array of enemies
    private Enemy[] enemyList;
    #endregion

    #region Unity_functions
    // Start is called before the first frame update
    void Start()
    {

        // make list of 
        enemyList = new Enemy[numberOfEnemies];
        for (int i = 0; i < enemyList.Length; i++)
        {
            //enemyList[i] = basicEnemy;
        }

        for (int i = 0; i < enemyList.Length; i++)
        {
            enemyList[i] = Instantiate<Enemy>(enemyList[i], moveSpots[Random.Range(0, moveSpots.Length)], Quaternion.identity);
        }

        // pick random spots
        randomSpots = new Vector3[numberOfEnemies];
        for (int i = 0; i < randomSpots.Length; i++)
        {
            randomSpots[i] = moveSpots[Random.Range(0, moveSpots.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            Move(enemyList[i], randomSpots[i]);

            if (enemyList[i].transform.position == randomSpots[i])
            {
                randomSpots[i] = moveSpots[Random.Range(0, moveSpots.Length)];
            }
        }
    }
    #endregion

    #region movement_functions
    private void Move(Enemy mob, Vector3 spot)
    {
        //mob.transform.position = Vector3.MoveTowards(mob.transform.position, 
            //spot, mob.moveSpeed * Time.deltaTime);
    }
    #endregion
}
