using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour
{

    public float velocityUp = .02f;
    public float p = 0f;
    public bool hasGoneUp = false;
    public  bool isTimeToAttack = false;
    private Vector3 spawn;
    public float heightOffset = 5;
    public float rotSpeed = .1f;
    public float rotationTimer = 2;
    private bool getNewRotation = true;
    private Quaternion targetRotation;
    private GameObject followPoint;
    private GameObject dog;
    public GameObject target;
    public float attackSpeed = .02f;
    public float s = 0f;
    private Vector3 currLoc;

    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
        followPoint = getClosestRockFollowPoint(GameObject.FindGameObjectsWithTag("RockFollow"));
        dog = GameObject.FindGameObjectWithTag("Dog");
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasGoneUp)
        {
            p += velocityUp;
            transform.position = Vector3.Lerp(spawn, new Vector3(spawn.x, spawn.y + heightOffset, spawn.z), p);
            if (p >= 1.1f) hasGoneUp = true;
        }

        if (getNewRotation)
        {
            targetRotation = Random.rotation;
            getNewRotation = false;
            Invoke("NewRotation", rotationTimer);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);

        if (!isTimeToAttack && followPoint != null)
        {
            transform.position = AnimMath.Slide(transform.position, followPoint.transform.position, .01f);
        }

        if (isTimeToAttack)
        {
            s += attackSpeed;
            transform.position = Vector3.Lerp(currLoc, target.transform.position, s);
        }

        if (isTimeToAttack && target == null)
        {
            Invoke("destroy", 0);
            if(followPoint != null)
            {
                Destroy(followPoint);
            }
        }

        if (!isTimeToAttack && followPoint == null)
        {
            dog.gameObject.SendMessage("subtractARock", SendMessageOptions.DontRequireReceiver);
            
            Invoke("destroy", 0);
        }  


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(followPoint != null)
            {
                Destroy(followPoint);
            }
            Invoke("destroy", .5f);
        }
    }

    private void destroy()
    {
        Destroy(this.gameObject);
    }

    private void goTowardsEnemy(GameObject enemy)
    {
        target = enemy;
        currLoc = transform.position;
        isTimeToAttack = true;
    }

    private void NewRotation()
    {
        getNewRotation = true;
    }

    private GameObject getClosestRockFollowPoint(GameObject[] points)
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
