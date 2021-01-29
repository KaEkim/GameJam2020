using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float speed = 300f;
    Rigidbody rb;
    float maxSpeed = 500f;
    int spinX = 30;
    int spinY = 1;
    float rotationAmount = 0;
    public static int fireBallCharge = 0;
    public int lightningYOffset = 10;

    public Camera cam;

    public Transform target;

    //Prefabs
    [Header("Prefabs")]
    public GameObject fireBallPrefab;
    public GameObject shieldPrefab;
    public GameObject lightningPrefab;
    public GameObject summonPrefab;
    public GameObject flightPrefab;


    //cooldowns
    [Header("CoolDowns")]
    public bool fireCoolDown = true;
    public bool shieldCoolDown = true;
    public bool lightningCoolDown = true;
    public bool summonCoolDown = true;
    public bool flightCoolDown = true;


    //Gem States
    [Header("Gem States")]
    public bool hasFireGem = true;
    public bool hasShieldGem = true;
    public bool hasLightningGem = true;
    public bool hasSummonGem = true;
    public bool hasFlightGem = true;


    // Start is called before the first frame update
    void Start()
    {

        Screen.lockCursor = true;
        Cursor.visible = true;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {


        ////SpawnsFireBall    
        if (hasFireGem && fireCoolDown)
        {
            //ChargesFireBall
            if (Input.GetKey(KeyCode.E))
            {
                fireBallCharge++;
            }
            //LaunchFireballFunction
            if (Input.GetKeyUp(KeyCode.E))
            {
                spawnFireBall(fireBallCharge);
            }
        }



        //LaunchShield
        if (Input.GetKey(KeyCode.V))
        {
            spawnShield();
        }


        // launch lightning
        if (Input.GetKey(KeyCode.R))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                
                var spawnLocation = hit.point;
                Debug.DrawLine(cam.ScreenToWorldPoint(Input.mousePosition), spawnLocation, Color.green, 4);
                spawnLocation = new Vector3(spawnLocation.x, spawnLocation.y + lightningYOffset, spawnLocation.z);      

                spawnLightning(spawnLocation);
                
            }
        }


        // summon golem dog
        if (Input.GetKey(KeyCode.Q))
        {

        }



        //Input for movement
        if (Input.GetKey(KeyCode.A))
        {
            //RotateTowardCamera();
            if (rb.velocity.magnitude < maxSpeed)
            {
                if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(transform.right * -1 * speed * 2 * Time.deltaTime);
                }
                else rb.AddForce(transform.right * -1 * speed * Time.deltaTime);
            }

        }


        if (Input.GetKey(KeyCode.D))
        {
            //RotateTowardCamera();
            if (rb.velocity.magnitude < maxSpeed)
            {
                if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(transform.right * speed * 2 * Time.deltaTime);
                }
                else rb.AddForce(transform.right * speed * Time.deltaTime);
            }
        }


        if (Input.GetKey(KeyCode.S))
        {
            RotateTowardCamera();
            if (rb.velocity.magnitude < maxSpeed)
            {
                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                {
                    rb.AddForce(transform.forward * -1 * speed * 2 * Time.deltaTime);
                }
                else rb.AddForce(transform.forward * -1 * speed * Time.deltaTime);
            }
        }


        if (Input.GetKey(KeyCode.W))
        {
            RotateTowardCamera();
            if (rb.velocity.magnitude < maxSpeed)
            {
                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                {
                    rb.AddForce(transform.forward * speed * 2 * Time.deltaTime);
                }
                else rb.AddForce(transform.forward * speed * Time.deltaTime);
            }
        }
    }

    private void spawnFireBall(int multiplier)
    {
        Instantiate(fireBallPrefab, this.transform.position, this.transform.rotation);
    }

    private void spawnShield()
    {
        Instantiate(shieldPrefab, this.transform.position, this.transform.rotation);
    }

    private void spawnLightning(Vector3 spawn)
    {
        Instantiate(lightningPrefab, spawn, new Quaternion());
    }

    private void spawnGolem()
    {
        //Instantiate(summonPrefab, summonposition, this.transform.rotation);
    }

    private void RotateTowardCamera()
    {

        Vector3 targetDir = target.position - transform.position;
        float singleStep = 5 * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);

        //Vector3 newestDirection = new Vector3(.x, this.transform.rotation.y, this.transform.rotation.z);

        //transform.rotation = Quaternion.LookRotation(newDirection);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}