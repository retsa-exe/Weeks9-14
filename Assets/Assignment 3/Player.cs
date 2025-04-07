using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    //basic information on player
    public float health = 50;
    public float score;
    public float speed = 2;
    float size;
    public float speedOfGrowth = 1.05f;
    public float maxSize = 3.5f;

    //timers
    float t;
    float damageTimer;

    //get component from the player
    SpriteRenderer sr;

    //get the spawner to get the fish array
    public FishSpawner spawner;

    //events for value changed
    public UnityEvent<float> onHealthChanged = new UnityEvent<float>();
    public UnityEvent<float> onScoreChanged = new UnityEvent<float>();
    public UnityEvent onGameOver;

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

        //reset the size to max size if player gets bigger than it
        if (size > maxSize)
        {
            size = maxSize;
            transform.localScale = Vector3.one * size;
        }
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

                //check if exceed the maxium value
                if (size > maxSize)
                {
                    size = maxSize;
                }
                transform.localScale = Vector3.one * size;
            }
            yield return null;
        }
    }

    private void Update()
    {
        //update damage timer
        damageTimer += Time.deltaTime;

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
                else
                {
                    //check the cool down timer
                    if (damageTimer > 1.5f)
                    {
                        takeDamage(fishSize - size);

                        //reset the cool down timer
                        damageTimer = 0;
                    }
                }
            }
        }
    }

    void Eat(GameObject fish)
    {
        //variable changes
        score += 100;
        health += 3;
        size += 0.1f;

        //invoke unity events
        onScoreChanged.Invoke(score);
        onHealthChanged.Invoke(health);

        //check if exceed the maxium value
        if (size > maxSize)
        {
            size = maxSize;
        }

        //assign the size to the scale
        transform.localScale = Vector2.one * size;

        //destroy the fish being eaten
        spawner.fishList.Remove(fish);
        Destroy(fish);
    }

    void takeDamage (float damage)
    {
        //convert the damage to whole number
        damage = Mathf.RoundToInt(damage * 5);
        health -= damage;
        Debug.Log("Take " + damage + " damage!");

        //invoke unity events
        onHealthChanged.Invoke(health);
    }
}
