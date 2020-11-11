using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region playerVariables
    // the player for us to work with
    Player birdPlayer;
    Rigidbody birdBody;
    RigidbodyConstraints originalConstraints;
    #endregion

    #region movementVariables
    public float moveSpeed;

    float xInput;
    float yInput;
    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private float minHeight;
    [SerializeField]
    private float leftBorder;
    [SerializeField]
    private float rightBorder;
    #endregion

    #region attackVariables
    public float Damage;
    public float attackRange;
    private bool atCooldown;
    private bool isAttacking;
    float attackTimer;
    #endregion

    #region unityFunctions
    // Initialize the bird and relevant variables
    private void Awake()
    {
        birdPlayer = GetComponent<Player>();
        attackTimer = 0; // Player starts off ready to attack 
        birdBody = GetComponent<Rigidbody>();
        originalConstraints = birdBody.constraints;
        isAttacking = false;
    }

    // Might need an Update() function? Implement later for when bird does a certain action
    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        if (birdBody.position.y >= maxHeight && yInput > 0
            || birdBody.position.y <= minHeight && yInput < 0)
        {
            yInput = 0f;
        } 
        if (birdBody.position.x <= leftBorder && xInput < 0
            || birdBody.position.x >= rightBorder && xInput > 0)
        {
            xInput = 0f;
        } 
        
        Move();
        
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackTimer <= 0)
        {
            PeckAttack();
        } else if (Input.GetKeyDown(KeyCode.Mouse1) && attackTimer <= 0)
        {
            DashAttack();
        } else
        {
            attackTimer -= Time.deltaTime;
        }
        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            chooseAttack();
        }
        */
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }


    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (isAttacking)
            {
                Debug.Log(col.transform.name);
                if (col.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Attacking BasicEnemy");
                    col.transform.GetComponent<Enemy>().TakeDamage(Damage);
                }
            }
        }
        isAttacking = false;
    }


    /*
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            birdBody.isKinematic = true;
            birdBody.constraints = RigidbodyConstraints.FreezeAll;
            float timeInvincible = 2.5f;
            Invoke("InvincibleFrames", timeInvincible);
        }
    }

    private void InvincibleFrames()
    {
        birdBody.isKinematic = false;
        birdBody.constraints = originalConstraints;
    }
    */

    #endregion

    #region movementFunctions
    private void Move()
    {
        Vector3 movement_vector = new Vector3(xInput, yInput);
        movement_vector = movement_vector.normalized;
        birdBody.MovePosition(birdBody.position + movement_vector * Time.deltaTime * moveSpeed);
    }
    #endregion

    #region attackFunctions

    /*
    private void Attack()
    {
        // For now, we will set left click to attack and F to toggle attack method
        // Pressing F will go through element list

        // The following message will display when you left click
        Debug.Log("Left click initialized! Basic peck attack");
    }
    */
    private void PeckAttack()
    {
        Debug.Log("Left click initialized! Peck attack");
        isAttacking = true;
        // Play animation for pecking

        
        // Still need to figure out where transform.forward is pointing. Maybe I just want a Vector3.right when facing the enemy on the right.
        /*
        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.right, attackRange);

        foreach (RaycastHit hit in hits)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Attacking BasicEnemy");
                hit.transform.GetComponent<Enemy>().TakeDamage(Damage);
                // Basic Enemy should die here. Check to see if enemy object gets destroyed.
                // Notes: AttackRange = 2 in order to consistently detect basicEnemy.
                // Since BasicEnemy has 5 health, the player does 5 damage to oneshot the enemy.

                // Hitbox Inconsistencies: If enemy is aligned with the bird's beak, it doesn't hit the enemy. The bird's attack will hit if enemy touches bird's belly.
            }
        }
        */
    }

    private void DashAttack()
    {
        Debug.Log("Right click initialized! Dash attack");
        isAttacking = true;
        float dashDistance = 5f;



        // Below code is for teleporting. We can use this later in case we still want teleporting.
        // For now, this will be omitted.
        /*
        if (xInput > 0)
        {
            birdBody.position += Vector3.right * dashDistance;
        } else if (xInput < 0)
        {
            birdBody.position -= Vector3.right * dashDistance;
        }
        */

        // Actual dashing behavior
        // For later: Possibly consider a dash cooldown timer to prevent spamming

        // Fix by 11/5: Fix dashing so that if bird touches enemy while dashing, bird oneshots enemy and dashes through enemy.
        /*
        if (xInput > 0)
        {
            birdBody.AddForce(Vector3.right * dashDistance * moveSpeed, ForceMode.Impulse);
        }
        else if (xInput < 0)
        {
            birdBody.AddForce(Vector3.left * dashDistance * moveSpeed, ForceMode.Impulse);
        }
        */
        Vector3 movement_vector = new Vector3(xInput, yInput);
        movement_vector = movement_vector.normalized;
        birdBody.AddForce(movement_vector * dashDistance * moveSpeed, ForceMode.Impulse);

        /*
        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.right, attackRange);

        foreach (RaycastHit hit in hits)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Attacking BasicEnemy");
                hit.transform.GetComponent<Enemy>().TakeDamage(Damage);
            }

        }
        */
    }
    /*
    // chooseAttack might have parameters to set which elemental attack the bird will have equipped
    private void chooseAttack()
    {
        Debug.Log("Switching mode of attack!");
    }
    */
    #endregion

}
