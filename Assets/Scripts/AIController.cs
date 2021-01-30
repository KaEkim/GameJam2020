using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    NavMeshAgent agent;

    // points of interest where the enemy will go
    [Header("POI")]
    public GameObject player;
    public GameObject centerOfMap;

    // speed and sight
    [Header("Speed & Sight")]
    public float speed;
    public float sightRange = 5;

    // State machine
    [Header("State Machine")]
    //general movement
    public bool isIdling = true;
    public bool isWalking = false;
    public bool isAttacking = false;
    //special states
    public bool isGoingTowardsCenter = false;
    public bool isUsingGem = false;
    private bool hasKilledPlayer = false;
    private bool hasGems = false;

    // health
    [Header("Health")]
    public bool isDead = false;
    public float health = 100;
    public float iframes = 0;
    public float iframesReset = 300;

    // attacking 
    [Header("Attacking Vars")]
    public float damage = 34;
    public float attackTimer = 0;
    public float attackRadius = 5;

    //idling
    [Header("Idle Vars")]
    public float idleTimer = 0;
    public float defaultIdleTimer = 300;

    //random point
    [Header("Random Point")]
    private Vector3 randomPoint = new Vector3();
    public float maxDistance = 70;
    private float walkTimer = 0;
    private float defaultWalkTimer = 2000;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent =  GetComponent<NavMeshAgent>();
        idleTimer = defaultIdleTimer;
    }

    // Update is called once per frame
    void Update()
    {
        stateManagement();
        stateActions();

        if (iframes <= 0) iframes = 0;
        iframes--;
    }         
    
    void stateManagement()
    {
        if (!hasGems)
        {
            // idle/walking to attacking
            if (checkDistanceToPlayer() < sightRange)
            {
                isAttacking = true;
                isIdling = false;
                isWalking = false;
            }
            // attacking to idle
            if (checkDistanceToPlayer() > sightRange && isAttacking)
            {
                isAttacking = false;
                isIdling = true;
                idleTimer = defaultIdleTimer;
            }
            // idle to walking
            if (idleTimer <= 0 && !isAttacking && isIdling)
            {
                isWalking = true;
                isIdling = false;
                randomPoint = Random.insideUnitSphere * maxDistance;
                agent.destination = randomPoint;
                walkTimer = defaultWalkTimer;
            }
            // walking to idle
            if (checkDistanceToPoint(randomPoint) <= 5 || walkTimer <= 0)
            {
                if (isWalking)
                {
                    agent.destination = transform.position;
                    isWalking = false;
                    isIdling = true;
                    idleTimer = defaultIdleTimer;
                }
            }
        }
        if (hasGems)
        {
            if (hasKilledPlayer)
            {
                isAttacking = false;
                isWalking = false;
                isGoingTowardsCenter = true;
            }
            if (checkDistanceToCenter() <= 5 && isGoingTowardsCenter)
            {
                isGoingTowardsCenter = false;
                hasKilledPlayer = false;
                isIdling = true;
            }
            if (checkDistanceToPlayer() <= 10)
            {
                isGoingTowardsCenter = false;
                isIdling = false;
                isUsingGem = true;
            }
        }
    }

    void stateActions()
    {
        if (isAttacking)
        {
            
            agent.destination = player.transform.position;
            if (checkDistanceToPlayer() < attackRadius && attackTimer <= 0)
            {
                attackTimer = 120;
                //player.health -= damage;
                //if (player.isDead) {
                    //hasGems = true;
                    //hasKilledPlayer = true;
                //}
            }
            if (attackTimer <= 0) attackTimer = 0;
            attackTimer--;
        }
        if (isIdling)
        {
            if (hasGems)
            {
                //endlessly idle at center until player gets near
            }
            if (!hasGems)
            {
                if (idleTimer <= 0) idleTimer = 0;
                idleTimer--;
            }
        }
        if (isWalking)
        {
            walkTimer--;
            if (walkTimer <= 0) walkTimer = 0;
        }
        if (isGoingTowardsCenter)
        {
            
            agent.destination = centerOfMap.transform.position;
        }
        if (isUsingGem)
        {
            //try to use gem
            //spawn effects
            //kill monke

            //drop gems
        }
    }
    

    float checkDistanceToPlayer()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        //print(dist);
        return dist;
    }

    float checkDistanceToCenter()
    {
        float dist = Vector3.Distance(centerOfMap.transform.position, transform.position);
        //print(dist);
        return dist;
    }

    float checkDistanceToPoint(Vector3 point)
    {
        float dist = Vector3.Distance(point, transform.position);
        //print(dist);
        return dist;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("hit");        
        if (collision.gameObject.CompareTag("explosion"))
        {
            //print("booom");
            takeDamage(50);
        }
    }

    public void takeDamage(int damage)
    {
        if (iframes <= 0)
        {
            health -= damage;
            iframes = iframesReset;
        }
    }
}
