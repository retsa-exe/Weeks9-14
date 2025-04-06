using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    //basic variables for the spawned fish
    public float size;
    public float speed = 2;
    public int direction;
    float initialY;

    //place that exceed the screen
    public float exceedScreen = 150;

    //animation curve for y movement
    public AnimationCurve curve;

    //components from fish
    SpriteRenderer sr;

    //remember the spawner
    public FishSpawner spawner;

    //random value add to fish Y movement
    public float randomNum;

    private void Start()
    {
        //get size from the transform
        size = transform.localScale.x;

        //set random direction (-1 or 1)
        direction = Random.Range(0, 2) * 2 - 1;

        //get component from fish
        sr = GetComponent<SpriteRenderer>();

        //record the initial Y position
        initialY = transform.position.y;

        //generate a value that randomlize the animation
        randomNum = Random.Range(0f, 1f);
    }

    private void Update()
    {
        //make the fish bounce between screen
        Vector2 pos = transform.position;

        //calculate the new X and Y
        pos.x += speed * direction * Time.deltaTime;
        pos.y = initialY + curve.Evaluate((Time.time + randomNum) / 3 % 1) * 0.3f;

        //get the screen position of fish
        Vector2 screenPos = Camera.main.WorldToScreenPoint(pos);

        //set value base on which side of edge
        if (screenPos.x < (0 - exceedScreen))
        {
            direction *= -1;
            Vector3 worldLeft = Camera.main.ScreenToWorldPoint(new Vector3(0 - exceedScreen, 0, 0));
            pos.x = worldLeft.x;
        }
        if (screenPos.x > (Screen.width + exceedScreen))
        {
            direction *= -1;
            Vector3 worldRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + exceedScreen, 0, 0));
            pos.x = worldRight.x;
        }

        //assign the position back to fish
        transform.position = pos;

        //flip the image based on direction
        if (direction > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
