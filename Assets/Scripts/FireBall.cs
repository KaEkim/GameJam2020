using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    public GameObject fireBallExplosion;
    public float speed = 2;
    private Vector3 initialLocation;


    // Start is called before the first frame update
    void Start()
    {
        //set spawn location so we can track distance from that later
        initialLocation = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //Always fly forward
        transform.position += this.transform.forward * speed * Time.deltaTime;

        //If you get far enough from your spawn location, trigger explosion
        if (Mathf.Abs(initialLocation.x - this.transform.position.x) > 20 || Mathf.Abs(initialLocation.z - this.transform.position.z) > 20)
        {
            spawnBoom();
        }
    }

    private void OnCollisionEnter(Collision collisionObject)
    {
        //if you touch something, trigger explosion
        if (!collisionObject.gameObject.CompareTag("Player"))
        {
            spawnBoom();
        }
    }

    //spawn explosion and destroy the projectile
    private void spawnBoom(){
        Instantiate(fireBallExplosion, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}