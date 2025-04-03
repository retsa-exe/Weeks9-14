using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //basic information on player
    public float health = 50;
    public float score;
    float size;

    //the timer for coroutine
    float t;

    private void Start()
    {
        //initialize the variables
        health = 50;
        score = 0;
        size = 1;

        //set the scale to one
        transform.localScale = Vector3.one * size;

        //start grow with time coroutine
        StartCoroutine(growWithTime());
    }

    IEnumerator growWithTime()
    {
        while (health > 0)
        {
            //make the t grow with time
            t += Time.deltaTime;

            //make the player grow a little every 3 seconds
            if (t > 3)
            {
                t = 0;
                size *= 1.1f;
                transform.localScale = Vector3.one * size;
            }
        }
        yield return null;
    }
}
