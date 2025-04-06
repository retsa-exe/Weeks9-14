using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //basic information on player
    public float health = 50;
    public float score;
    public float speed = 2;
    float size;
    public float speedOfGrowth = 1.05f;

    //the timer for coroutine
    float t;

    //get component from the player
    SpriteRenderer sr;

    //get the spawner to get the fish array
    public FishSpawner spawner;

    private void Start()
    {
        //initialize the variables
        health = 50;
        score = 0;
        size = 1;

        //set the scale to one
        transform.localScale = Vector3.one * size;

        //get sprite renderer from player
        sr = GetComponent<SpriteRenderer>();

        //start grow with time coroutine
        StartCoroutine(growWithTime());
    }

    //the coroutine that makes the player grow with time
    IEnumerator growWithTime()
    {
        while (health > 0)
        {
            //make the t grow with time
            t += Time.deltaTime;

            //make the player grow a little every 5 seconds
            if (t > 5)
            {
                t = 0;
                size *= speedOfGrowth;
                transform.localScale = Vector3.one * size;
            }
            yield return null;
        }
    }

    private void Update()
    {
        //make the player move by player inputs
        Vector2 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position = pos;

        //flip image when player goes right
        if (Input.GetAxis("Horizontal") > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        //check fish collisions
        checkFishCollisions();
    }

    void checkFishCollisions()
    {
        //get the fish list from the spawner
        List<GameObject> fishes = spawner.fishList;

        //detect the distance of the fishes
        for (int i = fishes.Count - 1; i >= 0; i--)
        {
            //get fish form the object
            GameObject fish = fishes[i];
            float fishSize = fish.transform.localScale.x;

            //calculate the collision distance between the player and fish
            float collisionDistance = size / 2 + fishSize / 2;

            //calculate the distance between player and fish
            float distance = Vector2.Distance(transform.position, fish.transform.position);

            //detect if the fish is near the player
            if (distance <= collisionDistance)
            {
                //call eat function if fish is smaller than player
                if (size > fishSize)
                {
                    Debug.Log("Eat fish!");
                    Eat(fish);
                }
            }
        }
    }

    void Eat(GameObject fish)
    {
        //variable changes
        score += 100;
        health += 10;
        size += 0.1f;

        //assign the size to the scale
        transform.localScale = Vector2.one * size;

        //destroy the fish being eaten
        spawner.fishList.Remove(fish);
        Destroy(fish);
    }
}
