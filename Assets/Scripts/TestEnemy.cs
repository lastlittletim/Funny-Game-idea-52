using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public GameObject bullet;
    public float bulletInterval = 1;
    public float timer;

    //shoot
        //make bullet

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
            //shoot
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = this.transform.position;
            Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>(); //get bullet rb
            bulletRB.velocity = Vector2.right; //set velocity
            timer = 0;
        }
    }
}
