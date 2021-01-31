using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AbilityUiController : MonoBehaviour
{

    public GameObject player;

    public Image fireAbility;
    public Image ShieldAbility;
    public Image LightningAbility;
    public Image SummonAbility;
    public Image FlightAbility;
    public Image TimeAbility;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image heart4;

    public bool gem = true;
    public bool cooldown = true;

    public float p = 2;

    Color activated;
    Color used;
    Color deactivated;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        activated = Color.white;
        used = Color.grey;
        deactivated = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        updateHearts();
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
        if (PlayerController.hasFireGem && PlayerController.fireCoolDown)
        {
            fireAbility.color = Color.Lerp(fireAbility.color, activated, 0 + Time.deltaTime * p);
        }
        else if (PlayerController.hasFireGem && !PlayerController.fireCoolDown)
        {
            fireAbility.color = Color.Lerp(fireAbility.color, used, 0 + Time.deltaTime * p);
        }
        else if(!PlayerController.hasFireGem && !PlayerController.fireCoolDown)
        {
            fireAbility.color = Color.Lerp(fireAbility.color, deactivated, 0 + Time.deltaTime * p);
        }







        if (PlayerController.hasLightningGem && PlayerController.lightningCoolDown)
        {
            LightningAbility.color = Color.Lerp(LightningAbility.color, activated, 0 + Time.deltaTime * p);
        }
        else if (PlayerController.hasLightningGem && !PlayerController.lightningCoolDown)
        {
            LightningAbility.color = Color.Lerp(LightningAbility.color, used, 0 + Time.deltaTime * p);
        }
        else if (!PlayerController.hasLightningGem && !PlayerController.lightningCoolDown)
        {
            LightningAbility.color = Color.Lerp(LightningAbility.color, deactivated, 0 + Time.deltaTime * p);
        }






        if (PlayerController.hasFlightGem && PlayerController.flightCoolDown)
        {
            FlightAbility.color = Color.Lerp(FlightAbility.color, activated, 0 + Time.deltaTime * p);
        }
        else if (PlayerController.hasFlightGem && !PlayerController.flightCoolDown)
        {
            FlightAbility.color = Color.Lerp(FlightAbility.color, used, 0 + Time.deltaTime * p);
        }
        else if (!PlayerController.hasFlightGem && !PlayerController.flightCoolDown)
        {
            FlightAbility.color = Color.Lerp(FlightAbility.color, deactivated, 0 + Time.deltaTime * p);
        }






        if (PlayerController.hasShieldGem && PlayerController.shieldCoolDown)
        {
            ShieldAbility.color = Color.Lerp(ShieldAbility.color, activated, 0 + Time.deltaTime * p);
        }
        else if (PlayerController.hasShieldGem && !PlayerController.shieldCoolDown)
        {
            ShieldAbility.color = Color.Lerp(ShieldAbility.color, used, 0 + Time.deltaTime * p);
        }
        else if (!PlayerController.hasShieldGem && !PlayerController.shieldCoolDown)
        {
            ShieldAbility.color = Color.Lerp(ShieldAbility.color, deactivated, 0 + Time.deltaTime * p);
        }






        if (PlayerController.hasSummonGem && PlayerController.summonCoolDown)
        {
            SummonAbility.color = Color.Lerp(SummonAbility.color, activated, 0 + Time.deltaTime * p);
        }
        else if (PlayerController.hasSummonGem && !PlayerController.summonCoolDown)
        {
            SummonAbility.color = Color.Lerp(SummonAbility.color, used, 0 + Time.deltaTime * p);
        }
        else if (!PlayerController.hasSummonGem && !PlayerController.summonCoolDown)
        {
            SummonAbility.color = Color.Lerp(SummonAbility.color, deactivated, 0 + Time.deltaTime * p);
        }






        if (PlayerController.timeCoolDown)
        {
            TimeAbility.color = Color.Lerp(TimeAbility.color, activated, 0 + Time.deltaTime * p);
        }
        else if (!PlayerController.timeCoolDown)
        {
            TimeAbility.color = Color.Lerp(TimeAbility.color, used, 0 + Time.deltaTime * p);
        }
        




    }

    private void updateHearts()
    {
        print(player.GetComponent<PlayerController>().health);
        if(player.GetComponent<PlayerController>().health >= 76)
        {
            heart1.enabled = true;
            heart2.enabled = true;
            heart3.enabled = true;
            heart4.enabled = true;
        }
        if (player.GetComponent<PlayerController>().health >= 51 && player.GetComponent<PlayerController>().health <= 76)
        {
            heart1.enabled = true;
            heart2.enabled = true;
            heart3.enabled = true;
            heart4.enabled = false;
        }
        if (player.GetComponent<PlayerController>().health >= 26 && player.GetComponent<PlayerController>().health <= 51)
        {
            heart1.enabled = true;
            heart2.enabled = true;
            heart3.enabled = false;
            heart4.enabled = false;
        }
        if (player.GetComponent<PlayerController>().health >= 0 && player.GetComponent<PlayerController>().health <= 26)
        {
            heart1.enabled = true;
            heart2.enabled = false;
            heart3.enabled = false;
            heart4.enabled = false;
        }
    }
}
