using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{

    private bool following = true;
    private float shieldGrowRate = .2f;
    private float maxScale = 8f;
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
        //ChecksForAbilityPressToMoveShield
        if (Input.GetKey(KeyCode.V))
        {
            following = true;
        }
        else following = false;

        //scale until it hits the proper size
        if(this.transform.localScale.x < maxScale)
        {
            this.transform.localScale += new Vector3(shieldGrowRate, shieldGrowRate, shieldGrowRate);
        }

        if (health <= 0) DeathFunction();

    }

    private void DeathFunction()
    {
        //start the timer in playerController to be able to use the shield ability again

        //PlayAnimationShitHere, and dont forget you can use a delay in the destroy down below
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (following)
        {
            //calculates absolute value between player and shield, then moves it 
            Vector3 absoluteDistance = new Vector3(this.transform.position.x - player.transform.position.x, this.transform.position.y - player.transform.position.y, this.transform.position.z - player.transform.position.z);
            this.transform.position -= absoluteDistance / 40;
        }
    }
}