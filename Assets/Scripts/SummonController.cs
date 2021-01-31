using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonController : MonoBehaviour
{
    //TODO:
    //change the attack so it checks all enemies then attacks if close
    //have the random point finder be somewhere within the players radius
    //

    NavMeshAgent agent;

    // points of interest where the summon will go    
    private GameObject player;

    // speed and sight
    [Header("Speed & Sight")]
    public float speed;
    public float sightRange = 5;
    public float spawnTimer = 5;
    public float timer = 5;

    // State machine
    [Header("State Machine")]
    //general movement
    public bool isSpawning = true;
    public bool isIdling = false;
    public bool isWalking = false;
    public bool isAttacking = false;

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
    public float attackTimerReset = 5;
    public float attackRadius = 5;
    public static GameObject targetEnemy;
    private bool hasTarget = false;
    public GameObject attackPrefab;
    public GameObject rockFollow;
    public float rockFollowOffset = 5;
    public float spawnRadius = 5f;
    public GameObject[] rocks;
    public int totalRocks = 0;

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
        agent = GetComponent<NavMeshAgent>();
        //print("isworkign~!");
        player = GameObject.FindGameObjectWithTag("Player");
        
        idleTimer = defaultIdleTimer;
        
    }

    // Update is called once per frame
    void Update()
    {
        //print("isworkignsdafsdfasadfdsaa~!");
        stateManagement();
        stateActions();
        timer -= Time.deltaTime;
        //print(timer);
        rocks = GameObject.FindGameObjectsWithTag("Rock");

        if (iframes <= 0) iframes = 0;
        iframes -= Time.deltaTime;

        if (hasTarget && attackTimer >= 0)
        {            
            getClosestRock(GameObject.FindGameObjectsWithTag("Rock")).SendMessage("goTowardsEnemy", SendMessageOptions.DontRequireReceiver);
            totalRocks--;
        }

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0) attackTimer = 0;


        if (spawn) spawnRock();
    }

    void stateManagement()
    {
        // isSpawning to idle
        if (isSpawning && timer <= 0)
        {
            
            setToIdle();
        }


        // idle/walking to attacking
        //if (isIdling || isWalking)
        //{
            if (totalRocks <= 0)
            {
                isAttacking = true;
                isIdling = false;
                isWalking = false;
            }
        //}
        print(GameObject.FindGameObjectsWithTag("Rock"));

        // attacking to idle
        if (isAttacking && totalRocks > 0)
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

    void stateActions()
    {
        if (isAttacking)
        {
            Invoke("spawnRock", 0.25f);
            Invoke("spawnRock", 0.5f);
            Invoke("spawnRock", 0.75f);
            Invoke("spawnRock", 1.0f);
            totalRocks = 4;
            /*if (checkDistanceToPoint(targetEnemy.transform.position) < attackRadius && attackTimer <= 0)
            {
                attackTimer = 120;                
                Invoke("spawnRock", 0.25f);
                Invoke("spawnRock", 0.5f);
                Invoke("spawnRock", 0.75f);
                Invoke("spawnRock", 1.0f);
            }
            if (attackTimer <= 0) attackTimer = 0;
            attackTimer--;*/
        }
        if (isIdling)
        {
            idleTimer--;
        }
        if (isWalking)
        {
            walkTimer--;
            if (walkTimer <= 0) walkTimer = 0;
        }
    }


    float checkDistanceToPlayer()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        //print(dist);
        return dist;
    }

    void checkDistanceToEnemies()
    {
        var objects = GameObject.FindGameObjectsWithTag("Enemy");
        var objectCount = objects.Length;

        foreach (var obj in objects)
        {
            if (!hasTarget)
            {
                float dist = Vector3.Distance(transform.position, obj.transform.position);
                if (dist <= sightRange)
                {
                    targetEnemy = obj;                    
                    hasTarget = true;
                }
            }
        }           
    }


    float checkDistanceToPoint(Vector3 point)
    {
        float dist = Vector3.Distance(point, transform.position);
        //print(dist);
        return dist;
    }   

    public void takeDamage(int damage, float iFRMS)
    {
        if (iframes <= 0)
        {
            health -= damage;
            iframes = iFRMS;
        }
    }

    public void setToIdle()
    {
        //print("is Idling");
        isSpawning = false;
        isIdling = true;
        idleTimer = defaultIdleTimer;
    }

    public bool spawn = false;

    public void spawnRock()
    {
        spawn = false;
        Vector3 spawnPoint = pickPointInRadius();
        Instantiate(attackPrefab, spawnPoint, transform.rotation);
        Instantiate(rockFollow, spawnPoint + new Vector3(0, rockFollowOffset, 0), transform.rotation, transform);
    }

    private Vector3 pickPointInRadius()
    {
        Vector3 originPoint = transform.position;
        originPoint.x += Random.Range(-spawnRadius, spawnRadius);
        originPoint.y -= 1f;
        originPoint.z += Random.Range(-spawnRadius, spawnRadius);
        return originPoint;
    }

    private GameObject getClosestRock(GameObject[] points)
    {
        GameObject tMin = null;
        float minDist = 10f;
        Vector3 currentpos = transform.position;
        foreach (GameObject t in points)
        {
            float dist = Vector3.Distance(t.transform.position, currentpos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }

        return tMin;
    }
}
