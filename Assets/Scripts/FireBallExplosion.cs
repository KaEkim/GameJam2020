using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallExplosion : MonoBehaviour
{

    private Vector3 scaleChange;
    private int charge;
    private float explodeTime = .5f;

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
        if (!PlayerController.timeFreeze)
        {
            this.transform.localScale += scaleChange;
        }
    }

    private void destroy()
    {
        if (!PlayerController.timeFreeze)
        {
            PlayerController.fireBallCharge = 0;
            Destroy(this.gameObject);
        }
    }    

}