using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float speed = 5f;
    float newSpeed;
    Rigidbody rb;
    public static int fireBallCharge = 0;
    public int lightningYOffset = 10;
    GameObject rotTarget;

    bool isGrounded = false;

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

    //Reset Booleans
    [Header("Reset Booleans")]
    public static bool resetFireToggle = false;
    public static bool resetShieldToggle = false;
    public static bool resetLightningToggle = false;
    public static bool resetSummonToggle = false;
    public static bool resetFlightToggle = false;


    // Start is called before the first frame update
    void Start()
    {

        rotTarget = GameObject.FindGameObjectWithTag("MainCamera");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
                spawnFireBall();
                fireCoolDown = false;
            }
        }



        //LaunchShield
        if (Input.GetKey(KeyCode.V) && hasShieldGem && !GameObject.FindGameObjectWithTag("Shield") && shieldCoolDown)
        {
            spawnShield();
            shieldCoolDown = false;
        }


        // launch lightning
        if (Input.GetKey(KeyCode.R))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
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

            //Incase you need this @Jacob to edit the times of cooldowns
            // Invoke("NameOfFunction", TimeInSeconds)

            //Check for Reset Toggles to be true, if so, "Invoke" command will reset them using functions below
            if (resetFireToggle) { Invoke("resetFire", 3); resetFireToggle = false; }
            if (resetShieldToggle) { Invoke("resetShield", 30); resetShieldToggle = false; }
            if (resetLightningToggle) { Invoke("resetLightning", 50); resetLightningToggle = false; }
            if (resetSummonToggle) { Invoke("resetSummon", 60); resetSummonToggle = false; }
            if (resetFlightToggle) { Invoke("resetFlight", 30); resetFlightToggle = false; }
    }

    private void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * 3f, ForceMode.Impulse);

        }

        //Input for movement
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.D))
            {
                newSpeed = speed / 2;
                RotateForward("wd");
            }
            else if (Input.GetKey(KeyCode.A))
            {
                newSpeed = speed / 2;
                RotateForward("wa");
            }
            else {
                newSpeed = speed;
                RotateForward("w");
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.W))
            {
                newSpeed = speed/2;
                RotateForward("wa");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                newSpeed = speed / 2;
                RotateForward("as");
            }
            else
            {
                newSpeed = speed;
                RotateForward("a");
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
            {
                newSpeed = speed / 2;
                RotateForward("as");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                newSpeed = speed / 2;
                RotateForward("sd");
            }
            else
            {
                newSpeed = speed;
                RotateForward("s");
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.W))
            {
                newSpeed = speed / 2;
                RotateForward("wd");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                newSpeed = speed / 2;
                RotateForward("sd");
            }
            else
            {
                newSpeed = speed;
                RotateForward("d");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            print("Touching");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            print("NotTouching");
        }
    }



    //Reset Abilities
    private void resetFire()
    {
        fireCoolDown = true;
    }
    private void resetShield()
    {
        shieldCoolDown = true;
    }
    private void resetLightning()
    {
        lightningCoolDown = true;
    }
    private void resetSummon()
    {
        summonCoolDown = true;
    }
    private void resetFlight()
    {
        flightCoolDown = true;
    }

    //Create Abilities
    private void spawnFireBall()
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

    private void RotateForward(string directionLooking)
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {

            newSpeed = newSpeed * 2f;
        }
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            newSpeed = newSpeed / 2f;
        }
        transform.position += (transform.forward * newSpeed * Time.deltaTime);

        Vector3 straitAhead = new Vector3(0, 0, 0);
        Vector3 pos90 = new Vector3(0,90,0);
        Vector3 neg90 = new Vector3(0, -90, 0);
        Vector3 even180 = new Vector3(0, 180, 0);

        Vector3 wa = new Vector3(0, 45, 0);
        Vector3 ass = new Vector3(0, 135, 0);
        Vector3 sd = new Vector3(0, 225, 0);
        Vector3 wd = new Vector3(0, -45, 0);


        Quaternion camDir = rotTarget.transform.rotation;
        int multiplier = 4;

        if(directionLooking == "w")
        {
            
            Quaternion angleSize = Quaternion.Lerp(transform.rotation, Quaternion.Euler(straitAhead) * camDir, Time.deltaTime * multiplier);

            transform.rotation = new Quaternion(0 , angleSize.y, 0, angleSize.w);

        }

        if (directionLooking == "a")
        {
            
            Quaternion angleSize = Quaternion.Lerp(transform.rotation, Quaternion.Euler(neg90) * camDir, Time.deltaTime * multiplier);
            transform.rotation = new Quaternion(0, angleSize.y, 0, angleSize.w);
        }

        if (directionLooking == "s")
        {
            
            Quaternion angleSize = Quaternion.Lerp(transform.rotation, Quaternion.Euler(even180) * camDir, Time.deltaTime * multiplier);
            transform.rotation = new Quaternion(0, angleSize.y, 0, angleSize.w);
        }

        if (directionLooking == "d")
        {
            
            Quaternion angleSize = Quaternion.Lerp(transform.rotation, Quaternion.Euler(pos90) * camDir, Time.deltaTime * multiplier);

            transform.rotation = new Quaternion(0, angleSize.y, 0, angleSize.w);
        }


        if (directionLooking == "wa")
        {
            
            Quaternion angleSize = Quaternion.Lerp(transform.rotation, Quaternion.Euler(wd) * camDir, Time.deltaTime * multiplier);
            transform.rotation = new Quaternion(0, angleSize.y, 0, angleSize.w);

        }

        if (directionLooking == "as")
        {
           
            Quaternion angleSize = Quaternion.Lerp(transform.rotation, Quaternion.Euler(sd) * camDir, Time.deltaTime * multiplier);
            transform.rotation = new Quaternion(0, angleSize.y, 0, angleSize.w);

        }

        if (directionLooking == "sd")
        {
           
            Quaternion angleSize = Quaternion.Lerp(transform.rotation, Quaternion.Euler(ass) * camDir, Time.deltaTime * multiplier);
            transform.rotation = new Quaternion(0, angleSize.y, 0, angleSize.w);

        }

        if (directionLooking == "wd")
        {
         
            Quaternion angleSize = Quaternion.Lerp(transform.rotation, Quaternion.Euler(wa) * camDir, Time.deltaTime * multiplier);

            transform.rotation = new Quaternion(0, angleSize.y, 0, angleSize.w);

        }
    }
}