using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{

    public float velocityDown = .02f;
    public float p = 0f;
    public bool hasHitTarget = false;
    private Vector3 spawn;
    public float growRate = .2f;
    public int maxScale = 7;


    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.timeFreeze)
        {
            if (!hasHitTarget)
            {
                p += velocityDown;
                //transform.position = new Vector3(transform.position.x, transform.position.y - (velocityDown * Time.deltaTime), transform.position.z);
                transform.position = Vector3.Lerp(spawn, new Vector3(spawn.x, spawn.y - PlayerController.lightningYOffset, spawn.z), p);
                if (p >= 1.1f) hasHitTarget = true;
            }

            if (hasHitTarget)
            {
                if (this.transform.localScale.x < maxScale)
                {
                    this.transform.localScale += new Vector3(growRate, growRate, growRate);
                }
            }

            if (this.transform.localScale.x >= maxScale)
            {
                LightningCast.isAttackOver = true;
                Destroy(this.gameObject);
            }
        }

    }
    
}
