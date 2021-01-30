using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform target;
    Vector3 offset;
    Vector3 velocity = Vector3.zero;
    float spinY = 1.5f;
    float spinX = 3;
    private float rotationAmountX = 0;
    private float rotationAmountY = 0;

    public static float actualMoveX = 0;
    public static float actualMoveY = 0;
    
    private float targetPitch;
    private float targetYaw;
    private float pitch;
    private float yaw;

    float dollyDis = 10;
    float targetDollyDis = 10;

    public float mouseSensitivityY;
    public float mouseSensitivityX;

    public float mouseScrollMultiplier = 5;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (target.position + offset);
        
        transform.position = Vector3.SmoothDamp(transform.position, direction, ref velocity, 0);
        //transform.rotation = target.transform.rotation;

        //float h = spinX * Input.GetAxis("Mouse X") * Time.deltaTime;
        //float v = spinY * Input.GetAxis("Mouse Y") * Time.deltaTime;

        //rotationAmountX += h;
        //rotationAmountY += v;

        //float v = spinY * Input.GetAxis("Mouse Y");
        //actualMoveX = rotationAmountX / 2;
        //actualMoveY = rotationAmountY / 2;

        //if (actualMoveY < -89) actualMoveY = -88;
        //if (actualMoveY > 89) actualMoveY = 88;

        //transform.rotation = new Quaternion(-actualMoveY, actualMoveX, 0, transform.rotation.w);

        //rotationAmountX = actualMoveX;
        //rotationAmountY = actualMoveY;

        ///////////////

        //transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * spinX);
        //transform.RotateAround(target.transform.position, new Vector3(1,0,0), Input.GetAxis("Mouse Y") * spinY);
        //transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, 0, this.transform.rotation.w);


        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        targetPitch += mouseY * mouseSensitivityY;
        targetYaw += mouseX * mouseSensitivityX;

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        targetDollyDis += scroll * mouseScrollMultiplier;
        targetDollyDis = Mathf.Clamp(targetDollyDis, 2.5f, 15);


        dollyDis = AnimMath.Slide(dollyDis, targetDollyDis, .05f); // EASE
        //target.transform.position = new Vector3(0, 0, -dollyDis);


        // Changing the rotation to match the pitch variable

        targetPitch = Mathf.Clamp(targetPitch, -50, 25);

        pitch = AnimMath.Slide(pitch, targetPitch, .01f); //EASE
        yaw = AnimMath.Slide(yaw, targetYaw, .01f); //EASE

        // Quaternion targetRotation
        transform.rotation = Quaternion.Euler(-pitch, yaw, 0);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.01f); // Could use .Slerp



    }

}