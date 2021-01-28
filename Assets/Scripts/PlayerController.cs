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

    public Transform target;
    

    // Start is called before the first frame update
    void Start()
    {

        Screen.lockCursor = true;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        //Shooting Input

        //Setup for bullet rotation



        //Input for movement
        if (Input.GetKey(KeyCode.A))
        {
            
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

    private void RotateTowardCamera()
    {
        Vector3 targetDir = target.position - transform.position;
        float singleStep = 20 * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);

        //Vector3 newestDirection = new Vector3(.x, this.transform.rotation.y, this.transform.rotation.z);

        //transform.rotation = Quaternion.LookRotation(newDirection);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}