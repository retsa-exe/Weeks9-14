using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Collapse : MonoBehaviour
{
    public float t;
    private void Start()
    {
        StartCoroutine(collapse());
    }

    public IEnumerator collapse()
    {
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.one * (1 - t);
            yield return null;
        }
    }
}
