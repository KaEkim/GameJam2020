using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private float SmoothTime = .13f;
    private Vector3 offset;
    private GameObject target;
    private Vector3 prevTransform;
    private float rotAmount;



    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - target.transform.position;
        prevTransform = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = (target.transform.position + offset);
        transform.position = Vector3.SmoothDamp(transform.position, direction, ref velocity, SmoothTime);
        
        if (prevTransform != this.transform.position || Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.V))
        {
            //This makes the rotation of the playerMesh the
            //same as the playerController but with some smoothing
            rotAmount = Mathf.LerpAngle(target.transform.rotation.y, transform.rotation.y, 5);
            
            transform.Rotate(0,rotAmount, 0);
        }

        prevTransform = transform.position;
    }

}
