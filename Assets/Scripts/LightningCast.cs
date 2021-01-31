using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCast : MonoBehaviour
{
    
    public float spawnTimer = 5;
    public static bool isAttackOver = false;
    public GameObject lightningBoltPrefab;
    public bool spawnOnce = true;
    public float removeTimer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        isAttackOver = false;
        spawnTimer = 5;
        spawnOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.timeFreeze)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0 && spawnOnce)
            {
                spawnOnce = false;
                Instantiate(lightningBoltPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            }

            if (isAttackOver)
            {

                Invoke("destroy", removeTimer);
            }
        }
    }

    private void destroy()
    {
        PlayerController.lightningCoolDown = true;
        Destroy(this.gameObject);
    }

    // spawn thundercloud
    // wait a few seconds
    //spawn lightning
    // wait longer
    // destroy this and lightning
}
