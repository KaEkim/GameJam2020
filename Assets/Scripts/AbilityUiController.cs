using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AbilityUiController : MonoBehaviour
{

    public Image fireAbility;
    public Image ShieldAbility;
    public Image LightningAbility;
    public Image SummonAbility;
    public Image FlightAbility;
    public Image TimeAbility;

    public bool gem = true;
    public bool cooldown = true;

    public float p = 2;

    Color activated;
    Color used;
    Color deactivated;

    // Start is called before the first frame update
    void Start()
    {
        activated = Color.white;
        used = Color.grey;
        deactivated = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (PlayerController.hasFireGem && PlayerController.fireCoolDown)
        {
            fireAbility.color = Color.white;
        }
        else if (PlayerController.hasFireGem && !PlayerController.fireCoolDown)
        {
            fireAbility.color = Color.grey;
        }
        else
        {
            fireAbility.color = Color.black;
        }*/
        if (gem && cooldown)
        {
            fireAbility.color = Color.Lerp(fireAbility.color, activated, 0 + Time.deltaTime * p);
        }
        else if (gem && !cooldown)
        {
            fireAbility.color = Color.Lerp(fireAbility.color, used, 0 + Time.deltaTime * p);
        }
        else
        {
            fireAbility.color = Color.Lerp(fireAbility.color, deactivated, 0 + Time.deltaTime * p);
        }
        
    }
}
