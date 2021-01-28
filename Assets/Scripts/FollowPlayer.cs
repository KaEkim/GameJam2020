using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform target;
    Vector3 offset;
    float SmoothTime = .13f;
    Vector3 velocity = Vector3.zero;
    float spinY = 50;
    private float rotationAmount = 0;
    public static float actualMove = 0;




    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = (target.position + offset);
        //transform.position = Vector3.SmoothDamp(transform.position, direction, ref velocity, SmoothTime);
        //transform.rotation = target.transform.rotation;
    }

    private void FixedUpdate()
    {

        Vector3 direction = (target.position + offset);
        transform.position = Vector3.SmoothDamp(transform.position, direction, ref velocity, SmoothTime);
        //transform.rotation = target.transform.rotation;

        float h = spinY * Input.GetAxis("Mouse X") * Time.deltaTime;

        rotationAmount += h;

        //float v = spinY * Input.GetAxis("Mouse Y");
        actualMove = rotationAmount / 2;

        transform.Rotate(0, actualMove, 0);

        rotationAmount = actualMove;

    }

}
