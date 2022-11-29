﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (!collision.transform.CompareTag("Enemy"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}