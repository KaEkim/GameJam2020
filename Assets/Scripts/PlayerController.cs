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
    int spinX = 2;
    int spinY = 1;

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
        if (Input.GetKey(KeyCode.W))
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
                {
                    rb.AddForce(transform.right * -1 * speed * 2 * Time.deltaTime); ;
                }
                else rb.AddForce(transform.right * -1 * speed * Time.deltaTime);
            }

        }


        if (Input.GetKey(KeyCode.S))
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
                {
                    rb.AddForce(transform.right * speed * 2 * Time.deltaTime);
                }
                else rb.AddForce(transform.right * speed * Time.deltaTime);
            }
        }


        if (Input.GetKey(KeyCode.A))
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(transform.forward * -1 * speed * 2 * Time.deltaTime);
                }
                else rb.AddForce(transform.forward * -1 * speed * Time.deltaTime);
            }
        }


        if (Input.GetKey(KeyCode.D))
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(transform.forward * speed * 2 * Time.deltaTime);
                }
                else rb.AddForce(transform.forward * speed * Time.deltaTime);
            }
        }

        //Mouse controls rotation of player
        float h = spinX * Input.GetAxis("Mouse X");
        float v = spinY * Input.GetAxis("Mouse Y");

        transform.Rotate(0, h, 0);

    }
}
