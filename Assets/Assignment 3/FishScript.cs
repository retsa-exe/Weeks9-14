using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    //basic variables for the spawned fish
    public float size = 1;

    private void Update()
    {
        //update size
        transform.localScale = Vector3.one * size;
    }
}
