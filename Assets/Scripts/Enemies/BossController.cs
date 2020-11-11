using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : Enemy
{
    #region Boss Variables
    //[SerializeField]
    //[Tooltip("The type of boss.")]
    //private Enemy bossEnemy;

    //[SerializeField]
    //[Tooltip("A list of this boss' attacks.")]
    //private Attack[] attacks;

    [SerializeField]
    [Tooltip("The amount of time to wait before attacking (must be less than Frozen Time).")]
    private float attackWaitTime;

    [SerializeField]
    [Tooltip("The amount of time this boss is frozen to attack (must be longer than the attack animation).")]
    private float frozenTime;

    [SerializeField]
    [Tooltip("The range of this boss' attack.")]
    private float attackRange;

    [SerializeField]
    [Tooltip("The number of times this boss can attack in one position.")]
    private int attacksPerPosition;

    [SerializeField]
    [Tooltip("Where to spawn the attack with respect to the boss.")]
    private Vector3 attackOffset;

    [SerializeField]
    [Tooltip("The positions this boss can move to.")]
    private Vector3[] movePositions;

    [SerializeField]
    [Tooltip("The health bar of this boss.")]
    private Slider HPBar;

    [SerializeField]
    [Tooltip("The bounds of this boss.")]
    private Vector3[] bounds;

    private float attackWaitTimer;

    private float frozenTimer;

    private int i;

    private Rigidbody bossRigidbody;

    //private Enemy bossEnemy;

    private float attackTime;

    private bool isAttacking;

    private int numAttackSoFar;

    private ParticleSystem attackEffect;
    #endregion

    #region Unity Functions
    // Initialize the boss and relevant variables
    public override void Awake()
    {
        base.Awake();
        //GetComponent<Renderer>().material.color = Color.white;
        bossRigidbody = GetComponent<Rigidbody>();
        //bossEnemy = GetComponent<Enemy>();
        frozenTimer = frozenTime;
        attackWaitTimer = attackWaitTime;
        numAttackSoFar = 0;
        attackTime = 0.6f;
        isAttacking = false;
        i = 0;
        HPBar.value = currHealth / totalHealth;
        //boss = Instantiate<Enemy>(bossEnemy, movePositions[i], Quaternion.identity);
        attackEffect = GetComponentInChildren<ParticleSystem>();
        attackEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //if (frozenTimer > 0)
        //{
        //    frozenTimer -= Time.deltaTime;
        //    return;
        //} else
        //{
        //    frozenTimer = 0;
        //}

        //if (attackWaitTimer > 0)
        //{
        //    attackWaitTimer -= Time.deltaTime;
        //    return;
        //} else
        //{
        //    attackWaitTimer = 0;
        //}
        if (frozenTimer > 0)
        {
            frozenTimer -= Time.deltaTime;
            if (numAttackSoFar < attacksPerPosition)
            {
                attackWaitTimer -= Time.deltaTime;
            }
        } else
        {
            frozenTimer = 0;
        }

        if (attackWaitTimer <= 0)
        {
            Attack();
            isAttacking = true;
            numAttackSoFar++;
            attackWaitTimer = attackWaitTime;
        }

        if (isAttacking)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                //GetComponent<Renderer>().material.color = Color.white;
                isAttacking = false;
                attackTime = 0.6f;
            }
        }

        if (transform.position == movePositions[i])
        {
            frozenTimer = frozenTime;
            numAttackSoFar = 0;
            i++;
            if (i >= movePositions.Length)
            {
                i = 0;
            }
        }

        if (frozenTimer <= 0)
        {
            Vector3 moveTo = movePositions[i];
            Move(moveTo);
        }

        //Debug.Log(bossRigidbody.position);
        //boss.transform.position = Vector3.MoveTowards(boss.transform.position, moveTo, boss.moveSpeed * Time.deltaTime);
        //GetComponent<Renderer>().material.color = Color.black;
        //GetComponent<Renderer>().material.color = Color.white;
    }
    #endregion

    #region Movement Function
    private void Move(Vector3 moveTo)
    {
        // Movement using transform
        transform.position = Vector3.MoveTowards(transform.position, moveTo, moveSpeed * Time.deltaTime);

        // Movement using rigidbody
        //Vector3 movementVector = (moveTo - bossRigidbody.position).normalized;
        //bossRigidbody.MovePosition(bossRigidbody.position + movementVector * Time.deltaTime * bossEnemy.moveSpeed);

        //Vector3 moveTo = new Vector3(Random.Range(bounds[0][0], bounds[1][0]), Random.Range(bounds[0][1], bounds[1][1]), 0f);
        //Vector3 moveTo = transform.position + Vector3.up;
        //Debug.Log("Next position this boss is moving to: " + moveTo);
        //Debug.Log("Current position of this boss: " + transform.position);
        //attackWaitTimer = attackWaitTime;
        //frozenTimer = frozenTime;
        //Attack();
        //GetComponent<Renderer>().material.color = Color.white;
    }
    #endregion

    #region Attack Function
    private void Attack()
    {
        //GetComponent<Renderer>().material.color = Color.black;
        Debug.Log("This boss is performing an attack now.");
        RaycastHit hit;
        attackEffect.Play();
        if (Physics.SphereCast(transform.position + attackOffset, 0.5f, Vector3.left, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<Player>().TakeDamage(damageDealt);
            }
        }
    }
    #endregion

    #region Health Functions
    public override void Heal(float amount)
    {
        currHealth += amount;
        if (currHealth >= totalHealth)
        {
            currHealth = totalHealth;
        }
        HPBar.value = currHealth / totalHealth;
        Debug.Log("Current health of boss: " + currHealth);
    }

    public override void TakeDamage(float amount)
    {
        currHealth -= amount;
        HPBar.value = currHealth / totalHealth;
        Debug.Log("Current health of boss: " + currHealth);
        if (currHealth <= 0)
        {
            Purify();
        }
    }
    #endregion
}
