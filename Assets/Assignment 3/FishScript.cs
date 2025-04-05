using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    //basic variables for the spawned fish
    public float size;
    public float speed = 2;
    public int direction;

    //components from fish
    SpriteRenderer sr;

    private void Start()
    {
        //get size from the transform
        size = transform.localScale.x;

        //set random direction (-1 or 1)
        direction = Random.Range(0, 2) * 2 - 1;

        //get component from fish
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //make the fish bounce between screen
        Vector2 pos = transform.position;
        pos.x += speed * direction * Time.deltaTime;

        //get the screen position of fish
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        //set value base on which side of edge
        if (screenPos.x < 0)
        {
            direction *= -1;
            Vector3 worldLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
            pos.x = worldLeft.x;
        }
        if (screenPos.x > Screen.width)
        {
            direction *= -1;
            Vector3 worldRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
            pos.x = worldRight.x;
        }

        //assign the position back to fish
        transform.position = pos;
    }

    //function:

    //flip the x according to direction
}
