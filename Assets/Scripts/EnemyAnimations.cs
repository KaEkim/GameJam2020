using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{

    public Animator Animation;

    // Start is called before the first frame update
    void Start()
    {
        Animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AIController.isIdling)
        {
            Animation.SetBool("animIdle", true);
        }
        else {  Animation.SetBool("animIdle", false); }

        if (AIController.isWalking)
        {
            Animation.SetBool("animWalking", true);
        }
        else {  Animation.SetBool("animWalking", false); }

        if (AIController.isAttacking)
        {
            Animation.SetBool("animAttacking", true);
        }
        else
        {   Animation.SetBool("animAttacking", false);  }

    }
}
