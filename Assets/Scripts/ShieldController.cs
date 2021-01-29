using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{

    private bool following = true;
    private float growRate = .2f;
    private int maxScale = 7;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //ChecksForAbilityPressToMoveShield
        if (Input.GetKeyDown(KeyCode.V))
        {
            following = true;
        }
        else following = false;


        //scale until it hits the proper size
        if(this.transform.localScale.x < maxScale)
        {
            this.transform.localScale += new Vector3(growRate, growRate, growRate);
        }
    }
    private void FixedUpdate()
    {
        if (following)
        {
            Vector3 absoluteDistance = new Vector3(Mathf.Abs(this.transform.position.x - player.transform.position.x), Mathf.Abs(this.transform.position.y - player.transform.position.y), Mathf.Abs(this.transform.position.z - player.transform.position.z));
            this.transform.position -= absoluteDistance / 5;
        }
    }
}
