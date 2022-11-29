using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
    public float bulletSpeed;
    public float bulletInterval = 1;
    public float timer;

    //find player

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; //update timer
        if(timer > bulletInterval)
        {
            Vector2 toPlayer = player.transform.position - transform.position;

            //shoot
            GameObject newBullet = Instantiate(bullet); //create copy of bullet
            newBullet.transform.position = transform.position; //move bullet to enemy
            Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>(); //get bullet rb
            bulletRB.velocity = toPlayer.normalized * bulletSpeed; //set velocity
            timer = 0;
            Destroy(newBullet, 5);
        }
    }
}