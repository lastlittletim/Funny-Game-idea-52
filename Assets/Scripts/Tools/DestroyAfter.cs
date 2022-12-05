using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float time; //script just destroys the gameObject that it's attached to after a certain amount of time
    void Start() { Destroy(gameObject, time); }
}
