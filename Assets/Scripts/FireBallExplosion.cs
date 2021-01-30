using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallExplosion : MonoBehaviour
{

    private Vector3 scaleChange;
    private int charge;
    private float explodeTime = 1.5f;

    AIController aiScript;

    // Start is called before the first frame update
    void Start()
    {
        charge = PlayerController.fireBallCharge;
        if (charge >= 90) Invoke("destroy", explodeTime);
        if (charge > 30 && charge < 90) Invoke("destroy", explodeTime/2);
        if (charge <= 30) Invoke("destroy", explodeTime/4);
      

        
        scaleChange = new Vector3(.2f, .2f, .2f) ;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale += scaleChange;
        
    }

    private void destroy()
    {
        PlayerController.fireBallCharge = 0;
        Destroy(this.gameObject);
    }    

    public bool checkDistToEnemies()
    {
        var objects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var obj in objects)
        {
            float dist = Vector3.Distance(transform.position, obj.transform.position);
            if (dist <= -20)
            {
                aiScript = obj.transform.GetComponent<AIController>();
                return true;
            }            
        }
        return false;
    }
}