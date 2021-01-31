using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float speed = 5f;
    float newSpeed;
    Rigidbody rb;
    public static int fireBallCharge = 0;
    public static int lightningYOffset = 10;
    GameObject rotTarget;
    bool isGrounded = false;
    public static bool timeFreeze = false;
    public float spawnRadius = 5f;
    public float dogRadius = 5f;

    bool usingAimAbilities = false;

    public Camera cam;
    public Transform target;
    private bool isFlying = false;

    [Header("Health")]
    public bool isDead = false;
    public float health = 100;
    public float iframes = 0;
    public float iframesReset = 5;

    [Header("Attacking Vars")]
    public float damage = 34;
    public float attackTimer = 0;
    public float attackRadius = 5;

    //Prefabs
    [Header("Prefabs")]
    public GameObject fireBallPrefab;
    public GameObject shieldPrefab;
    public GameObject lightningPrefab;
    public GameObject summonPrefab;

    //cooldowns
    [Header("CoolDowns")]
    public static bool fireCoolDown = true;
    public static bool shieldCoolDown = true;
    public static  bool lightningCoolDown = true;
    public static bool summonCoolDown = true;
    public static bool flightCoolDown = true;
    public static bool timeCoolDown = true;

    //Gem States
    [Header("Gem States")]
    public static bool hasFireGem = true;
    public static bool hasShieldGem = true;
    public static bool hasLightningGem = true;
    public static bool hasSummonGem = true;
    public static bool hasFlightGem = true;

    //Reset Booleans
    [Header("Reset Booleans")]
    public static bool resetFireToggle = false;
    public static bool resetShieldToggle = false;
    public static bool resetLightningToggle = false;
    public static bool resetSummonToggle = false;

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
        if (iframes <= 0) iframes = 0;
        iframes -= Time.deltaTime;
        if (attackTimer <= 0) attackTimer = 0;
        attackTimer -= Time.deltaTime;

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
        
        if (Input.GetKey(KeyCode.R) && hasLightningGem && lightningCoolDown)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                var spawnLocation = hit.point;
                Debug.DrawLine(cam.ScreenToWorldPoint(Input.mousePosition), spawnLocation, Color.green, 4);
                spawnLocation = new Vector3(spawnLocation.x, spawnLocation.y + lightningYOffset, spawnLocation.z);

                spawnLightning(spawnLocation);

                lightningCoolDown = false;
                
            }
        }

        // summon golem dog
        if (Input.GetKey(KeyCode.Q) && hasSummonGem && summonCoolDown)
        {
            summonCoolDown = false;
            spawnGolem();
        }

        // toggles the flying ability
        if (Input.GetKey(KeyCode.F) && hasFlightGem && flightCoolDown)
        {
            flightCoolDown = false;
            isFlying = true;
            Invoke("disableFlight", 1.2f);
            Invoke("resetFlight", 10);
        }

        // start TimeFreeze
        if (Input.GetMouseButton(1) && timeCoolDown)
        {
            timeFreeze = true;
            timeCoolDown = false;
            Invoke("disableTime", 10);
            Invoke("resetTime", 60);
        }


            //Incase you need this @Jacob to edit the times of cooldowns
            // Invoke("NameOfFunction", TimeInSeconds)

            //Check for Reset Toggles to be true, if so, "Invoke" command will reset them using functions below
            if (resetFireToggle) { Invoke("resetFire", 3); resetFireToggle = false; }
            if (resetShieldToggle) { Invoke("resetShield", 30); resetShieldToggle = false; }
            if (resetLightningToggle) { Invoke("resetLightning", 50); resetLightningToggle = false; }
            if (resetSummonToggle) { Invoke("resetSummon", 60); resetSummonToggle = false; }
    }

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * 200f, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.Space) && isFlying)
        {
            rb.AddForce(transform.up * 15f, ForceMode.Impulse);

        }

        //if making an attack where you would want to turn forward for
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.R) || Input.GetMouseButtonDown(0))
        {
            RotateForward("newW");
            usingAimAbilities = true;

        }
        else
        {
            usingAimAbilities = false;
        }

        //else { usingAimAbilities = false; }

        //Input for movement
        if (Input.GetKey(KeyCode.W) && !usingAimAbilities)
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

        if (Input.GetKey(KeyCode.A) && !usingAimAbilities)
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

        if (Input.GetKey(KeyCode.S) && !usingAimAbilities)
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

        if (Input.GetKey(KeyCode.D) && !usingAimAbilities)
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
        if (Input.GetMouseButtonDown(0) && attackTimer <= 0)
        {
            GameObject[] eList = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject e in eList)
            {      
                
                

                if(checkDistanceToPoint(e.transform.position) <= attackRadius)
                {
                    e.GetComponent<AIController>().takeDamage(100, 5);
                }
            }
            attackTimer = 2;
            
            
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;


        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

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
    public void resetSummon()
    {
        summonCoolDown = true;
    }
    private void disableFlight()
    {
        isFlying = false;
    }
    private void resetFlight()
    {
        print("CanFlyAgain");
        flightCoolDown = true;
    }
    private void disableTime()
    {
        timeFreeze = false;

    }
    private void resetTime()
    {
        timeCoolDown = true;
        print("CanFreezeAgain");
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
        bool hasPickedPoint = false;
        Vector3 spawn = transform.position;
        spawn = pickPointInRadius();
        RaycastHit hit;
        if (Physics.Raycast(spawn + new Vector3(0, 10f, 0), Vector3.down, out hit, 200f))
        {
            Instantiate(summonPrefab, new Vector3(spawn.x, hit.point.y + 1f, spawn.z), this.transform.rotation);
        }        
    }

    private void RotateForward(string directionLooking)
    {
        //These three if statements check if you are doing anything that would effect your speed, and then change it if so
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {

            newSpeed = newSpeed * 2f;
        }
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            newSpeed = newSpeed / 2f;
        }
        if (isFlying)
        {
            newSpeed = newSpeed * 2f;
        }

        if (!usingAimAbilities)
        {
            transform.position += (transform.forward * newSpeed * Time.deltaTime);
        }


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

        if (directionLooking == "newW")
        { 
            //this quaternion needs to be changed to make it face forward always
            Quaternion angleSize = new Quaternion(transform.rotation.x, cam.transform.rotation.y, transform.rotation.z, transform.rotation.w);

            GameObject camControl = GameObject.FindGameObjectWithTag("GameController");

            float camAngle = camControl.transform.rotation.w;

            Vector3 rotFor = new Vector3(0, camAngle, 0);
            Vector3 rotBack = new Vector3(0, camAngle * -1, 0);
            Vector3 rotRight = new Vector3(0, camAngle + 1.5708f, 0);
            Vector3 rotLeft = new Vector3(0, (camAngle + 1.5708f) * -1, 0);

            transform.rotation = angleSize;


            if (newSpeed == 0)
            {
                newSpeed = speed;
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.position += (transform.forward * newSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += (transform.forward * -1 * newSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += (transform.right * -1 * newSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += (transform.right * newSpeed * Time.deltaTime);
            }
        }

        print(directionLooking);

    }

    public void takeDamage(int damage, float IFRMS)
    {
        if(iframes <= 0)
        {
            health -= damage;
            iframes = IFRMS;
        }
    }

    public Vector3 pickPointInRadius()
    {
        Vector3 originPoint = transform.position;
        originPoint.x += Random.Range(-spawnRadius, spawnRadius);
        originPoint.y += 1f;
        originPoint.z += Random.Range(-spawnRadius, spawnRadius);
        return originPoint;
    }

    float checkDistanceToPoint(Vector3 point)
    {
        float dist = Vector3.Distance(point, transform.position);
        //print(dist);
        return dist;
    }

    public float checkProjection(Vector2 a, Vector2 b)
    {
        return Vector2.Dot(a.normalized, b.normalized);
    }

}