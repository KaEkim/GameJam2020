using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{

    private bool following = true;
    private float growRate = .2f;
    private int maxScale = 7;
    private GameObject player;
    private float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.timeFreeze)
        {
            //ChecksForAbilityPressToMoveShield
            if (Input.GetKey(KeyCode.V))
            {
                following = true;
            }
            else following = false;

            //Kills shield for testing
            //if (Input.GetKey(KeyCode.K))
            //{
            //    health = 0;
            //}

            //scale until it hits the proper size
            if (this.transform.localScale.x < maxScale)
            {
                this.transform.localScale += new Vector3(growRate, growRate, growRate);
            }

            if (health <= 0)
            {
                //PlayDestructionAnimationHere

                //Starts cooldown inside PlayerController
                PlayerController.resetShieldToggle = true;

                //  #KYS
                Destroy(this.gameObject);

            }
        }

    }

    private void FixedUpdate()
    {
        if (!PlayerController.timeFreeze)
        {
            if (following)
            {
                Vector3 absoluteDistance = new Vector3(this.transform.position.x - player.transform.position.x, this.transform.position.y - player.transform.position.y, this.transform.position.z - player.transform.position.z);
                this.transform.position -= absoluteDistance / 5;
            }
        }
    }
}
