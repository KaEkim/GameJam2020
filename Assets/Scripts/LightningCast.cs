using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCast : MonoBehaviour
{
    
    public int spawnTimer = 5;
    public int lifeTimer = 5;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimer += spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    // spawn thundercloud
    // wait a few seconds
    //spawn lightning
    // wait longer
    // destroy this and lightning
}
